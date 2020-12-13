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

namespace AoC2020_Day13
{
    public partial class Form1 : Form
    {
        string[] lines;
        long[] buses;
        long start;

        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();

            start = Convert.ToInt32(lines[0]);
            string[] busList = lines[1].Split(new string[] { ",", "x" }, StringSplitOptions.RemoveEmptyEntries);
            buses = new long[busList.Length];
            for (int i = 0; i < buses.Length; i++)
                buses[i] = Convert.ToInt32(busList[i]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long nextBus = 0;
            long timestamp = 999999;

            for (int i = 0; i < buses.Length; i++)
            {
                long temp = start - (start % buses[i]) + buses[i];
                long stamp = temp - start;
                if (stamp < timestamp)
                {
                    nextBus = buses[i];
                    timestamp = stamp;
                }
            }

            MessageBox.Show("" + (nextBus * timestamp));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int64 start = 0;
            Int64 timestamp = 0;
            Bus[] Fleet = new Bus[] { new Bus(17, 0), new Bus(41, 7), new Bus(643, 17), new Bus(23, 25), new Bus(13, 30), new Bus(29, 46), new Bus(433, 48), new Bus(37, 54), new Bus(19, 67)};
            Int64 increment = Fleet[0].busID;

            bool hasNotFailed = false;
            while (!hasNotFailed)
            {
                hasNotFailed = true;
                timestamp += increment;
                for(int i = 1; i < Fleet.Length; i++)
                {
                    if (!Fleet[i].CheckEquals(timestamp + Fleet[i].Offset))
                    {
                        hasNotFailed = false;
                    }
                    else
                    {
                        if (Fleet[i].FoundIncrement)
                        {
                            increment = Fleet[i].Increment;
                            for (int j = 1; j < Fleet.Length; j++)
                                Fleet[j].Clear();
                        }
                    }
                }
            }

            MessageBox.Show("" + (timestamp - start));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int64 start = 0;
            //Int64 timestamp = 3400;
            //Bus[] Fleet = new Bus[] { new Bus(17, 0), new Bus(13, 2), new Bus(19, 3)};

            //Int64 timestamp = 753951;
            //Bus[] Fleet = new Bus[] { new Bus(67, 0), new Bus(7, 1), new Bus(59, 2), new Bus(61, 3) };

            //Int64 timestamp = 1202159600;
            Int64 timestamp = 0;
            Bus[] Fleet = new Bus[] { new Bus(1789, 0), new Bus(37, 1), new Bus(47, 2), new Bus(1889, 3), };
            Int64 increment = Fleet[0].busID;

            bool hasNotFailed = false;
            while (!hasNotFailed)
            {
                hasNotFailed = true;
                timestamp += increment;
                for (int i = 1; i < Fleet.Length; i++)
                {
                    if (!Fleet[i].CheckEquals(timestamp + Fleet[i].Offset))
                    {
                        hasNotFailed = false;
                    }
                    else
                    {
                        if(Fleet[i].FoundIncrement)
                        {
                            increment = Fleet[i].Increment;
                            for (int j = 1; j < Fleet.Length; j++)
                                Fleet[j].Clear();
                        }
                    }
                }
            }

            MessageBox.Show("" + (timestamp));
        }
    }

    public class Bus
    {
        public Int64 busID { get; set; }

        public Int64 Offset { get; set; }

        public Int64 Mark_One { get; set; }
        public Int64 Mark_Two { get; set; }

        public Int64 Increment { get; set; }

        bool pInc = false;
        public bool FoundIncrement
        {
            get
            {
                bool val = pInc;
                pInc = false;
                return val;
            }

            set { pInc = value; }
        }


        public Bus(Int64 id, Int64 offset)
        {
            Offset = offset;
            busID = id;
            Mark_One = 0;
            Mark_Two = 0;
            Increment = 0;
            FoundIncrement = false;
        }

        public bool CheckEquals(Int64 timestamp)
        {
            bool ret = ((timestamp % busID) == 0);
            if(ret && Increment == 0)
            {
                if (Mark_One == 0)
                    Mark_One = timestamp;
                else if (Mark_Two == 0)
                {
                    Mark_Two = timestamp;
                    FoundIncrement = true;
                    Increment = Mark_Two - Mark_One;
                }
            }
            return ret;
        }

        public void Clear()
        {
            if(Increment == 0)
            {
                Mark_One = 0;
                Mark_Two = 0;
                FoundIncrement = false;
            }
        }

        public Int64 NextValue(Int64 timestamp)
        {
            return (timestamp + busID);
        }
    }
}
