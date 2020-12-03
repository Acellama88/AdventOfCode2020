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

namespace AoC2020_Day3
{
    public partial class Form1 : Form
    {
        string[] lines;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = lines[0].Length;
            int length = lines.Length;

            int locW = 0;
            int locL = 0;

            int totalHit = 0;

            while(locL < length)
            {
                if (lines[locL][locW] == '#')
                    totalHit++;
                locL++;
                locW += 3;
                if (locW >= width)
                    locW = locW % width;
            }
            MessageBox.Show("" + totalHit);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[][] slopes = new int[][] { new int[] { 1, 1 }, new int[] { 3, 1 }, new int[] { 5, 1 }, new int[] { 7, 1 }, new int[] { 1, 2 } };
            int width = lines[0].Length;
            int length = lines.Length;

            int grandTotal = 1;
            for (int i = 0; i < slopes.Length; i++)
            {
                int locW = 0;
                int locL = 0;
                int totalHit = 0;
                while (locL < length)
                {
                    if (lines[locL][locW] == '#')
                        totalHit++;
                    locW += slopes[i][0];
                    locL += slopes[i][1];
                    if (locW >= width)
                        locW = locW % width;
                }
                grandTotal *= totalHit;
            }
            MessageBox.Show("" + grandTotal);
        }
    }
}
