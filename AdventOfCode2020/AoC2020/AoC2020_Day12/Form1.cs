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

namespace AoC2020_Day12
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
            Ship Artemis = new Ship();
            foreach(string line in lines)
            {
                string[] tokens = new string[2];
                tokens[0] = line.Substring(0, 1);
                tokens[1] = line.Substring(1);

                if (tokens[0][0] == 'R' || tokens[0][0] == 'L')
                    Artemis.rotate(tokens[0][0], Convert.ToInt32(tokens[1]));
                else if(tokens[0][0] == 'F')
                    Artemis.forward(Convert.ToInt32(tokens[1]));
                else
                    Artemis.Move(tokens[0][0], Convert.ToInt32(tokens[1]));
            }

            MessageBox.Show("" + (Math.Abs(Artemis.x) + Math.Abs(Artemis.y)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vessel Enterprise = new Vessel();
            foreach (string line in lines)
            {
                string[] tokens = new string[2];
                tokens[0] = line.Substring(0, 1);
                tokens[1] = line.Substring(1);

                if (tokens[0][0] == 'R' || tokens[0][0] == 'L')
                    Enterprise.rotate(tokens[0][0], Convert.ToInt32(tokens[1]));
                else if (tokens[0][0] == 'F')
                    Enterprise.forward(Convert.ToInt32(tokens[1]));
                else
                    Enterprise.Move(tokens[0][0], Convert.ToInt32(tokens[1]));
            }

            MessageBox.Show("" + (Math.Abs(Enterprise.Shipx) + Math.Abs(Enterprise.Shipy)));
        }


    }

    public class Ship
    {
        public int x { get; set; }
        public int y { get; set; }

        public int dir { get; set; }

        public Ship()
        {
            dir = 180;
            x = 0;
            y = 0;
        }

        public void rotate(char direction, int degrees)
        {
            if(direction == 'R')
                dir += degrees;
            if (direction == 'L')
                dir -= degrees;

            if (dir >= 360)
                dir -= 360;
            else if (dir < 0)
                dir += 360;    
        }

        public void forward(int spaces)
        {
            if (dir == 0)
                x -= spaces;
            else if (dir == 180)
                x += spaces;
            else if(dir == 90)
                y -= spaces;
            else if (dir == 270)
                y += spaces;
        }
        public void Move(char direction, int spaces)
        {
            if (direction == 'E')
                x += spaces;
            else if (direction == 'W')
                x -= spaces;
            else if(direction == 'N')
                y -= spaces;
            else if (direction == 'S')
                y += spaces;
        }
    }
    
    public class Vessel //Vessel
    {
        public int Shipx { get; set; }
        public int Shipy { get; set; }
        public int WayPx { get; set; }
        public int WayPy { get; set; }

        public Vessel()
        {
            Shipx = 0;
            Shipy = 0;
            WayPx = 10;
            WayPy = -1;
        }

        public void rotate(char direction, int degrees)
        {
            int[][] Signs = new int[][] { new int[]{ -1, -1 }, new int[] { 1, -1 }, new int[] { 1, 1 }, new int[] { -1, 1 } };
            if (degrees == 180)
            {
                WayPx = -1 * WayPx;
                WayPy = -1 * WayPy;
                return;
            }

            int x, y;
            if (WayPx > 0)
                x = 1;
            else
                x = -1;
            if (WayPy > 0)
                y = 1;
            else
                y = -1;

            int i = 0;
            for(i = 0; i < 4; i++)
            {
                if (Signs[i][0] == x && Signs[i][1] == y)
                    break;
            }

            if (direction == 'R')
            {
                if (degrees == 90)
                    i = (i + 1) % 4;
                if (degrees == 270)
                    i = (i + 3) % 4;
            }
            else if (direction == 'L')
            {
                if (degrees == 90)
                    i = (i + 3) % 4;
                if (degrees == 270)
                    i = (i + 1) % 4;
            }
            int temp = WayPx;
            WayPx = Math.Abs(WayPy) * Signs[i][0];
            WayPy = Math.Abs(temp) * Signs[i][1];
        }

        public void forward(int spaces)
        {
            for(int i = 0; i < spaces; i++)
            {
                Shipx += WayPx;
                Shipy += WayPy;
            }
        }
        public void Move(char direction, int spaces)
        {
            if (direction == 'E')
                WayPx += spaces;
            else if (direction == 'W')
                WayPx -= spaces;
            else if (direction == 'N')
                WayPy -= spaces;
            else if (direction == 'S')
                WayPy += spaces;
        }
    }
}
