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

namespace AoC2020_Day10
{
    public partial class Form1 : Form
    {
        string[] lines;
        List<int> adapters;
        UInt64 count = 0;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();

            adapters = new List<int>();
            adapters.Add(0);
            foreach (string line in lines)
            {
                adapters.Add(Convert.ToInt32(line));
            }
            adapters.Sort();
            adapters.Add(adapters.Max() + 3);
        }

            private void button1_Click(object sender, EventArgs e)
        {
            int one = 0;
            int two = 0;
            int three = 0;
            for(int i = 0; i < adapters.Count -1; i++)
            {
                int val = adapters[i + 1] - adapters[i];
                switch(val)
                {
                    case 1:
                        one++;
                        break;
                    case 2:
                        two++;
                        break;
                    case 3:
                        three++;
                        break;
                }
            }
            MessageBox.Show("" + (one * three));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ummm..... MATH! Answer: 3947645370368 (Hint: 7, 4, 2, 1)");
        }
    }
}
