using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AOC2020_Day8
{
    public partial class Form1 : Form
    {
        string[] lines;
        List<int> cmd;
        int acc;
        public Form1()
        {
            StreamReader input = new StreamReader("programinput.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();

            acc = 0;
            cmd = new List<int>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (cmd.Contains(i))
                    break;
                cmd.Add(i);
                string[] tokens = lines[i].Split(' ');
                if (tokens[0].Equals("nop"))
                    continue;
                if (tokens[0].Equals("acc"))
                    acc += Convert.ToInt32(tokens[1]);
                if (tokens[0].Equals("jmp"))
                    i += Convert.ToInt32(tokens[1]) - 1;
            }
            MessageBox.Show("" + acc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] newLines;
            bool worked = false;
            for(int i = 0; i < lines.Length && !worked; i++)
            {
                newLines = (string[]) lines.Clone();
                acc = 0;
                string[] tokens = lines[i].Split(' ');
                if (tokens[0].Equals("nop") && !tokens[1].Equals("0"))
                {
                    newLines[i] = "jmp " + tokens[1];
                }
                else if (tokens[0].Equals("jmp"))
                {
                    newLines[i] = "nop " + tokens[1];
                }
                else
                    continue;
                worked = runBootloader(newLines);
            }
            MessageBox.Show("" + acc);
        }

        private bool runBootloader(string[] newLines)
        {
            cmd = new List<int>();
            bool worked = true;
            for (int i = 0; i < newLines.Length; i++)
            {
                if (cmd.Contains(i))
                {
                    worked = false;
                    break;
                }
                if (newLines[i].Equals(""))
                    break;
                cmd.Add(i);
                string[] tokens = newLines[i].Split(' ');
                if (tokens[0].Equals("nop"))
                    continue;
                if (tokens[0].Equals("acc"))
                    acc += Convert.ToInt32(tokens[1]);
                if (tokens[0].Equals("jmp"))
                    i += Convert.ToInt32(tokens[1]) - 1;
            }
            return worked;

        }
    }
}
