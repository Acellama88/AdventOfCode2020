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

namespace AoC2020_Day6
{
    public partial class Form1 : Form
    {
        string[] lines;
        int offsetASCII = 97;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' });
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = 0;
            bool[] answers = new bool[26];
            foreach(string line in lines)
            {
                if(line.Equals(""))
                {
                    int temptotal = 0;
                    foreach(bool val in answers)
                    {
                        if (val)
                            temptotal++;
                    }
                    total += temptotal;
                    answers = new bool[26];
                    continue;
                }
                foreach(char answer in line)
                { 
                    answers[((int) answer) - offsetASCII] = true;
                }
            }
            MessageBox.Show("" + total);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int total = 0;
            bool[] answers = new bool[26];
            for(int i = 0; i < answers.Length; i++)
            {
                answers[i] = true;
            }
            foreach (string line in lines)
            {
                if (line.Equals(""))
                {
                    int temptotal = 0;
                    foreach (bool val in answers)
                    {
                        if (val)
                            temptotal++;
                    }
                    total += temptotal;
                    for (int i = 0; i < answers.Length; i++)
                    {
                        answers[i] = true;
                    }
                    continue;
                }
                bool[] personAnswer = new bool[26];
                foreach (char answer in line)
                {
                    personAnswer[((int)answer) - offsetASCII] = true;
                }
                for (int i = 0; i < answers.Length; i++)
                    answers[i] = answers[i] & personAnswer[i];
            }
            MessageBox.Show("" + total);
        }
    }
}
