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

namespace AoC2020_Day4
{
    public partial class Form1 : Form
    {
        string[] lines;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' });
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Passport scanPassport = new Passport();
            int valid = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                    continue;
                string value = "";
                while(lines[i].Length > 0)
                {
                    value += " " + lines[i++];
                }
                scanPassport.ClearPassport();
                scanPassport.Parse(value);
                if (scanPassport.isValid())
                    valid++;
            }

            MessageBox.Show("" + valid);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Passport scanPassport = new Passport();
            int valid = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                    continue;
                string value = "";
                while (lines[i].Length > 0)
                {
                    value += " " + lines[i++];
                }
                scanPassport.ClearPassport();
                scanPassport.Parse(value);
                if (scanPassport.isAlsoValid())
                    valid++;
            }

            MessageBox.Show("" + valid);
        }
    }

    public class Passport
    {
        string byr, iyr, eyr, hgt, hcl, ecl, pid, cid;
        public Passport()
        {
            ClearPassport();
        }
        public void Parse(string input)
        {
            string[] tokens = input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string val in tokens)
            {
                string[] keyPair = val.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                switch(keyPair[0])
                {
                    case "byr":
                        byr = keyPair[1];
                        break;
                    case "iyr":
                        iyr = keyPair[1];
                        break;
                    case "eyr":
                        eyr = keyPair[1];
                        break;
                    case "hgt":
                        hgt = keyPair[1];
                        break;
                    case "hcl":
                        hcl = keyPair[1];
                        break;
                    case "ecl":
                        ecl = keyPair[1];
                        break;
                    case "pid":
                        pid = keyPair[1];
                        break;
                    case "cid":
                        cid = keyPair[1];
                        break;
                    default:
                        MessageBox.Show("Oops, unparsable");
                        break;
                }
            }
        }

        public void ClearPassport()
        {
            byr = null;
            iyr = null;
            eyr = null;
            hgt = null;
            hcl = null;
            ecl = null;
            pid = null;
            cid = null;
        }

        public bool isValid()
        {
            if ((byr != null) && (iyr != null) && (eyr != null) && (hgt != null) && (hcl != null) && (ecl != null) && (pid != null))
                return true;
            else
                return false;
        }

        public bool isAlsoValid()
        {
            try
            {
                //If anything is missing?
                if ((byr == null) || (iyr == null) || (eyr == null) || (hgt == null) || (hcl == null) || (ecl == null) || (pid == null))
                    return false;
                //Check the Birth Year
                int year = Convert.ToInt32(byr);
                if (year > 2002 || year < 1920)
                    return false;
                //Check the Issue Year
                year = Convert.ToInt32(iyr);
                if (year < 2010 || year > 2020)
                    return false;
                //Check Expiration Year
                year = Convert.ToInt32(eyr);
                if (year < 2020 || year > 2030)
                    return false;
                //Check if the height format is correct
                if (!(hgt.Contains("cm") || hgt.Contains("in")))
                    return false;
                if(hgt.Contains("cm"))
                { //If Centimeters, parse out number and compare
                    int size = Convert.ToInt32(hgt.Split(new string[] { "cm" },StringSplitOptions.RemoveEmptyEntries)[0]);
                    if (size > 193 || size < 150)
                        return false;
                }
                else
                {//If Inches, parse out number and compare
                    int size = Convert.ToInt32(hgt.Split(new string[] { "in" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    if (size > 76 || size < 59) 
                        return false;
                }
                //Check Hair Color is #[0-9,a-f]
                if (!checkColor(hcl))
                    return false;
                //Check all hair color options
                string[] colors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                int i;
                for (i = 0; i < colors.Length; i++)
                {
                    if (ecl.Equals(colors[i]))
                        break;
                }
                if (i >= colors.Length)
                    return false;
                //Check PID is 9 long
                if (!checkNumber(pid, 9))
                    return false;
                //Skip CID

                //Everything else is good, pass!
                return true;
            }
            catch (Exception e)
            { //Just in case something is so wrong it blows something up, it was likely wrong to begin with!
                return false;
            }
        }

        public bool checkColor(string color)
        {
            try
            {
                if (color[0] != '#')
                    return false;
                if (color.Length > 7)
                    return false;
                for(int i = 1; i < color.Length; i++)
                {
                    int check = (int)color[i];
                    if ((check < 48) || ((check > 57) && (check < 97)) || (check > 102))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public bool checkNumber(string number, int length)
        {
            try
            {
                if (number.Length != length)
                    return false;
                for (int i = 1; i < number.Length; i++)
                {
                    int check = (int)number[i];
                    if ((check < 48) || (check > 57))
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
