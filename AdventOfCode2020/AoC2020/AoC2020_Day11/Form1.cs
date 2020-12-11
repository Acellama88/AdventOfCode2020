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

namespace AoC2020_Day11
{
    public partial class Form1 : Form
    {
        string[] lines;
        char[][] previous;
        char[][] current;
        bool first = true;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();

            current = new char[lines.Length][];
            previous = new char[lines.Length][];

            for(int i = 0; i < lines.Length; i++)
            {
                char[] temp = lines[i].ToCharArray();
                current[i] = temp;
            }
            Clone();
        }

        private void Clone()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                previous[i] = new char[current[i].Length];
                for (int j = 0; j < lines[0].Length; j++)
                {
                    previous[i][j] = current[i][j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int count;
            for (count = 0; !Same(); count++)
            {
                Clone();
                for (int i = 0; i < previous.Length; i++)
                {
                    for (int j = 0; j < previous[0].Length; j++)
                    {
                        checkArround(i, j);
                    }
                }
                Print();
            }

            count = 0;
            for (int i = 0; i < previous.Length; i++)
            {
                for (int j = 0; j < previous[0].Length; j++)
                {
                    if (occupied(current[i][j]))
                        count++;
                }
            }

            MessageBox.Show("" + count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count;
            for (count = 0; !Same(); count++)
            {
                Clone();
                for (int i = 0; i < previous.Length; i++)
                {
                    for (int j = 0; j < previous[0].Length; j++)
                    {
                        checkArround2(i, j);
                    }
                }
                Print();
            }

            count = 0;
            for (int i = 0; i < previous.Length; i++)
            {
                for (int j = 0; j < previous[0].Length; j++)
                {
                    if (occupied(current[i][j]))
                        count++;
                }
            }

            MessageBox.Show("" + count);
        }

        private void Print()
        {
            for (int i = 0; i < previous.Length; i++)
            {
                for (int j = 0; j < previous[0].Length; j++)
                {
                    Console.Write(previous[i][j]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("");

        }

        private bool Same()
        {
            if(first)
            {
                first = false;
                return false;
            }    
            for(int i = 0; i < previous.Length; i++)
            {
                for(int j = 0; j < previous[0].Length;j++)
                {
                    if (previous[i][j] != current[i][j])
                        return false;
                }
            }
            return true;

        }

        private bool empty(char val)
        {
            if (val == '.' || val == 'L')
                return true;
            else
                return false;
        }

        private bool occupied(char val)
        {
            if (val == '#')
                return true;
            else
                return false;
        }

        private void checkArround(int x, int y)
        {
            char TL, TP, TR, LF, RG, BL, BT, BR;
            if (previous[x][y] == '.')
                return;
            if (previous[x][y] == 'L')
            {
                if (x > 0 && y > 0)
                {
                    TL = previous[x - 1][y - 1];
                }
                else
                    TL = '.';

                if (x > 0)
                {
                    TP = previous[x - 1][y];
                }
                else
                    TP = '.';

                if (x > 0 && y < previous[0].Length - 1)
                {
                    TR = previous[x - 1][y + 1];
                }
                else
                    TR = '.';

                if (y > 0)
                {
                    LF = previous[x][y - 1];
                }
                else
                    LF = '.';

                if (y < previous[0].Length - 1)
                {
                    RG = previous[x][y + 1];
                }
                else
                    RG = '.';

                if (x < previous.Length - 1 && y > 0)
                {
                    BL = previous[x + 1][y - 1];
                }
                else
                    BL = '.';

                if (x < previous.Length - 1)
                {
                    BT = previous[x + 1][y];
                }
                else
                    BT = '.';

                if (x < previous.Length - 1 && y < previous[0].Length - 1)
                {
                    BR = previous[x + 1][y + 1];
                }
                else
                    BR = '.';

                if (empty(TL) && empty(TP) && empty(TR) && empty(LF) && empty(RG) && empty(BL) && empty(BT) && empty(BR))
                    current[x][y] = '#';
                return;
            }
            if (previous[x][y] == '#')
            {
                if (x > 0 && y > 0)
                {
                    TL = previous[x - 1][y - 1];
                }
                else
                    TL = '.';

                if (x > 0)
                {
                    TP = previous[x - 1][y];
                }
                else
                    TP = '.';

                if (x > 0 && y < previous[0].Length - 1)
                {
                    TR = previous[x - 1][y + 1];
                }
                else
                    TR = '.';

                if (y > 0)
                {
                    LF = previous[x][y - 1];
                }
                else
                    LF = '.';

                if (y < previous[0].Length - 1)
                {
                    RG = previous[x][y + 1];
                }
                else
                    RG = '.';

                if (x < previous.Length - 1 && y > 0)
                {
                    BL = previous[x + 1][y - 1];
                }
                else
                    BL = '.';

                if (x < previous.Length - 1)
                {
                    BT = previous[x + 1][y];
                }
                else
                    BT = '.';

                if (x < previous.Length - 1 && y < previous[0].Length - 1)
                {
                    BR = previous[x + 1][y + 1];
                }
                else
                    BR = '.';

                int count = 0;
                if (occupied(TL))
                    count++;
                if (occupied(TP))
                    count++;
                if (occupied(TR))
                    count++;
                if (occupied(LF))
                    count++;
                if (occupied(RG))
                    count++;
                if (occupied(BL))
                    count++;
                if (occupied(BT))
                    count++;
                if (occupied(BR))
                    count++;
                if (count >= 4)
                    current[x][y] = 'L';
                return;
            }
        }

        private void checkArround2(int x, int y)
        {
            char TL = '.';
            char TP = '.';
            char TR = '.';
            char LF = '.';
            char RG = '.';
            char BL = '.';
            char BT = '.';
            char BR = '.';
            
            if (previous[x][y] == '.')
                return;
            if (previous[x][y] == 'L')
            {
                int count = 1;
                while (x - count >= 0 && y - count >= 0 && TL == '.')
                {
                    TL = previous[x - count][y - count++];
                }

                count = 1;
                while (x - count >= 0 && TP == '.')
                {
                    TP = previous[x - count++][y];
                }

                count = 1;

                while (x - count >= 0 && y + count <= previous[0].Length - 1 && TR == '.')
                {
                    TR = previous[x - count][y + count++];
                }

                count = 1;

                while (y - count >= 0 && LF == '.')
                {
                    LF = previous[x][y - count++];
                }

                count = 1;

                while (y + count <= previous[0].Length - 1 && RG == '.')
                {
                    RG = previous[x][y + count++];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && y - count >= 0 && BL == '.')
                {
                    BL = previous[x+count][y - count++];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && BT == '.')
                {
                    BT = previous[x + count++][y];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && y + count <= previous[0].Length - 1 && BR == '.')
                {
                    BR = previous[x + count][y + count++];
                }

                count = 1;

                if (empty(TL) && empty(TP) && empty(TR) && empty(LF) && empty(RG) && empty(BL) && empty(BT) && empty(BR))
                    current[x][y] = '#';
                return;
            }
            if (previous[x][y] == '#')
            {
                int count = 1;
                while (x - count >= 0 && y - count >= 0 && TL == '.')
                {
                    TL = previous[x - count][y - count++];
                }

                count = 1;

                while (x - count >= 0 && TP == '.')
                {
                    TP = previous[x - count++][y];
                }

                count = 1;

                while (x - count >= 0 && y + count <= previous[0].Length - 1 && TR == '.')
                {
                    TR = previous[x - count][y + count++];
                }

                count = 1;

                while (y - count >= 0 && LF == '.')
                {
                    LF = previous[x][y - count++];
                }

                count = 1;

                while (y + count <= previous[0].Length - 1 && RG == '.')
                {
                    RG = previous[x][y + count++];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && y - count >= 0 && BL == '.')
                {
                    BL = previous[x + count][y - count++];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && BT == '.')
                {
                    BT = previous[x + count++][y];
                }

                count = 1;

                while (x + count <= previous.Length - 1 && y + count <= previous[0].Length - 1 && BR == '.')
                {
                    BR = previous[x + count][y + count++];
                }

                count = 0;
                if (occupied(TL))
                    count++;
                if (occupied(TP))
                    count++;
                if (occupied(TR))
                    count++;
                if (occupied(LF))
                    count++;
                if (occupied(RG))
                    count++;
                if (occupied(BL))
                    count++;
                if (occupied(BT))
                    count++;
                if (occupied(BR))
                    count++;
                if (count >= 5)
                    current[x][y] = 'L';
                return;
            }
        }
    }
}
