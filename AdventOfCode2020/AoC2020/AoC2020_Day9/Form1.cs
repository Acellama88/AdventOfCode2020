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

namespace AoC2020_Day9
{
    public partial class Form1 : Form
    {
        string[] lines;
        List<Int64> grouping;
        int Goal = 542529149;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();
            grouping = new List<Int64>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 25; i++)
            {
                grouping.Add(Convert.ToInt32(lines[i]));
            }
            Int64 testVal = 0;
            for (int i = 25; i < lines.Length; i++)
            {
                testVal = Convert.ToInt32(lines[i]);
                if (testValue(testVal))
                {
                    grouping.Add(testVal);
                    grouping.RemoveAt(0);
                }
                else
                    break;
            }
            MessageBox.Show("" + testVal);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool foundIt = false;
            for (int count = 2; !foundIt && count < lines.Length; count++)
            {
                grouping.Clear();
                for (int i = 0; i < count; i++)
                {
                    grouping.Add(Convert.ToInt32(lines[i]));
                }
                for (int i = 25; !foundIt && i < lines.Length; i++)
                {
                    if (!addAll())
                    {
                        grouping.Add(Convert.ToInt64(lines[i]));
                        grouping.RemoveAt(0);
                    }
                    else
                    {
                        foundIt = true;
                        break;
                    }
                }
            }
            grouping.Sort();
            MessageBox.Show("" + (grouping.First() + grouping.Last()));
        }

        private bool testValue(Int64 result)
        {
            for(int i = 0; i < grouping.Count - 1; i++)
            {
                for(int j = i + 1; j < grouping.Count; j++)
                {
                    if ((grouping[i] + grouping[j]) == result)
                        return true;
                }
            }
            return false;
        }

        private bool addAll()
        {
            Int64 total = 0;
            for (int i = 0; i < grouping.Count; i++)
            {
                total += grouping[i];
            }
            if (total == Goal)
                return true;
            else
                return false;
        }
    }
}
