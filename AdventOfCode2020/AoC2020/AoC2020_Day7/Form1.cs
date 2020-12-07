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

namespace AoC2020_Day7
{
    public partial class Form1 : Form
    {
        string[] lines;
        List<Bag> allBags;

        List<Bag> finalBags;
        List<Bag> finalContainers;
        public Form1()
        {
            StreamReader input = new StreamReader("input.txt");
            lines = input.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            InitializeComponent();
            allBags = new List<Bag>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(string line in lines)
            {
                string[] tokens = line.Split(new string[] { "contain", "," }, StringSplitOptions.RemoveEmptyEntries);
                string bagName = tokens[0].Split(' ')[0] + " " + tokens[0].Split(' ')[1];

                Bag mainBag = allBags.Find(x => x.Name.Equals(bagName));
                if(mainBag == null)
                {
                    mainBag = new Bag(bagName);
                    allBags.Add(mainBag);
                }

                for(int i = 1; i < tokens.Length; i++)
                {
                    if (tokens[i].Contains("no other bag"))
                        break;
                    string[] bagTokens = tokens[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    int num = Convert.ToInt32(bagTokens[0]);
                    string inBag = bagTokens[1] + " " + bagTokens[2];
                    Bag getBag = allBags.Find(x => x.Name.Equals(inBag));
                    if(getBag == null)
                    {
                        getBag = new Bag(inBag);
                        allBags.Add(getBag);
                    }

                    mainBag.addBag(num, getBag);
                    getBag.addContainer(mainBag);
                }
                //Parse All Containers
            }

            finalBags = new List<Bag>();
            Bag shiny = allBags.Find(x => x.Name.Equals("shiny gold"));
            MessageBox.Show("" + countBags(shiny));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string line in lines)
            {
                string[] tokens = line.Split(new string[] { "contain", "," }, StringSplitOptions.RemoveEmptyEntries);
                string bagName = tokens[0].Split(' ')[0] + " " + tokens[0].Split(' ')[1];

                Bag mainBag = allBags.Find(x => x.Name.Equals(bagName));
                if (mainBag == null)
                {
                    mainBag = new Bag(bagName);
                    allBags.Add(mainBag);
                }

                for (int i = 1; i < tokens.Length; i++)
                {
                    if (tokens[i].Contains("no other bag"))
                        break;
                    string[] bagTokens = tokens[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    int num = Convert.ToInt32(bagTokens[0]);
                    string inBag = bagTokens[1] + " " + bagTokens[2];
                    Bag getBag = allBags.Find(x => x.Name.Equals(inBag));
                    if (getBag == null)
                    {
                        getBag = new Bag(inBag);
                        allBags.Add(getBag);
                    }

                    mainBag.addBag(num, getBag);
                    getBag.addContainer(mainBag);
                }
                //Parse All Containers
            }

            finalContainers = new List<Bag>();
            Bag shiny = allBags.Find(x => x.Name.Equals("shiny gold"));
            MessageBox.Show("" + (countTotalBags(shiny) - 1));
        }

        private int countBags(Bag checkBag)
        {
            Bag dBag = finalBags.Find(x => x.Name.Equals(checkBag.Name));
            if (dBag != null)
                return -1;
            finalBags.Add(checkBag);
            int total = 0;
            foreach (Bag aBag in checkBag.ExternalContainers)
            {
                total += countBags(aBag) + 1;
            }
            return total;
        }

        private int countTotalBags(Bag checkBag)
        {
            int total = 0;
            foreach (Container aBag in checkBag.InternalContainers)
            {
                total += aBag.Number * countTotalBags(aBag.Bag);
            }
            return total + 1;
        }
    }

    public class Bag
    { 
        public string Name { get; }

        public List<Bag> ExternalContainers { get; }

        public List<Container> InternalContainers { get; }
        public Bag(string name)
        {
            Name = name;
            ExternalContainers = new List<Bag>();
            InternalContainers = new List<Container>();

        }

        public void addContainer(Bag container)
        {
            ExternalContainers.Add(container);
        }

        public void addBag(int number, Bag internalBag)
        {
            InternalContainers.Add(new Container(number, internalBag));
        }
    }

    public class Container
    {
        public int Number { get; set; }

        public Bag Bag { get; set; }

        public Container(int num, Bag bag)
        {
            Number = num;
            Bag = bag;
        }
    }
}
