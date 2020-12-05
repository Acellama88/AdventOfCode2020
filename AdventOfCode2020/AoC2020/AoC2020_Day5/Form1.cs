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

namespace AoC2020_Day5
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
            int highestID = 0;
            foreach(string boardingPass in lines)
            {
                string row = boardingPass.Substring(0, 7).Replace('B', '1').Replace('F', '0');
                string column = boardingPass.Substring(7, 3).Replace('R', '1').Replace('L', '0');
                int iRow = Convert.ToInt32(row, 2);
                int iColumn = Convert.ToInt32(column, 2);

                int seatID = iRow * 8 + iColumn;
                if (seatID > highestID)
                    highestID = seatID;
            }
            MessageBox.Show("" + highestID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> IDList = new List<int>();
            foreach (string boardingPass in lines)
            {
                string row = boardingPass.Substring(0, 7).Replace('B', '1').Replace('F', '0');
                string column = boardingPass.Substring(7, 3).Replace('R', '1').Replace('L', '0');
                int iRow = Convert.ToInt32(row, 2);
                int iColumn = Convert.ToInt32(column, 2);

                int seatID = iRow * 8 + iColumn;
                IDList.Add(seatID);
            }
            IDList.Sort();
            int lastID = IDList[0];
            for(int i = 1; i < IDList.Count; i++)
            {
                if (IDList[i] != lastID + 1)
                {
                    MessageBox.Show("" + lastID + 1);
                    break;
                }
                lastID = IDList[i];
            }
        }
    }
}
