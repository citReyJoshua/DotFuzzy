using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using DotFuzzy;

namespace Fuzzy
{
    public partial class Form1 : Form
    {
        private DotFuzzy.FuzzyEngine roadSelection;

        private LinguisticVariable road1;
        private LinguisticVariable road2;
        private LinguisticVariable road3;
        private LinguisticVariable road4;
        private LinguisticVariable nextGreen;
        private Random random;

        private MembershipFunctionCollection roadpopulation;
        private MembershipFunctionCollection nextGreenSelection;

        int state = 1;
        private Thread t;
        private Boolean shouldStart = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            
            
            
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roadSelection = new FuzzyEngine();
            random = new Random();
            setEngines();
            setTLights(state);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void runSimulation()
        {
            
            road1.InputValue = Convert.ToDouble(road1Cars.Text);
            road2.InputValue = Convert.ToDouble(road2Cars.Text);
            road3.InputValue = Convert.ToDouble(road3Cars.Text);
            road4.InputValue = Convert.ToDouble(road4Cars.Text);
            
            
            road1.Fuzzify("High");
            road4.Fuzzify("High");
            road2.Fuzzify("High");
            road3.Fuzzify("High");
            nextGreen.Fuzzify("Uncertain");
            
            double result = roadSelection.Defuzzify();
            Console.WriteLine("Starting...");
            Console.WriteLine(result);
            if (result >= 3 && result < 7) state = 1;
            else if (result >= 8 && result < 9) state = 2;
            else if (result >= 9.5 && result < 10) state = 3;
            else if (result >= 11 && result < 12) state = 4;
            else setLights();
            Console.WriteLine(state);
            setTLights(state);
            switch (state)
            {
                case 1:
                    while (!(road1Cars.Text.Equals("0")))
                    {
                        road1Cars.Text = Convert.ToString(Convert.ToInt32(road1Cars.Text) - random.Next(6, 10));
                        if (Convert.ToInt32(road1Cars.Text) < 0)
                            road1Cars.Text = "0";
                        road2Cars.Text = Convert.ToString(Convert.ToInt32(road2Cars.Text) + random.Next(0, 5));
                        road3Cars.Text = Convert.ToString(Convert.ToInt32(road3Cars.Text) + random.Next(0, 5));
                        road4Cars.Text = Convert.ToString(Convert.ToInt32(road4Cars.Text) + random.Next(0, 5));
                        Thread.Sleep(100);
                    }
                    break;
                case 2:
                    while (!(road2Cars.Text.Equals("0")))
                    {
                        road1Cars.Text = Convert.ToString(Convert.ToInt32(road1Cars.Text) + random.Next(0, 5));
                        road2Cars.Text = Convert.ToString(Convert.ToInt32(road2Cars.Text) - random.Next(6, 10));
                        if (Convert.ToInt32(road2Cars.Text) < 0)
                            road2Cars.Text = "0";
                        road3Cars.Text = Convert.ToString(Convert.ToInt32(road3Cars.Text) + random.Next(0, 5));
                        road4Cars.Text = Convert.ToString(Convert.ToInt32(road4Cars.Text) + random.Next(0, 5));
                        Thread.Sleep(100);
                    }
                        
                    break;
                case 3:
                    while (!(road3Cars.Text.Equals("0")))
                    {
                        road1Cars.Text = Convert.ToString(Convert.ToInt32(road1Cars.Text) + random.Next(0, 5));
                        road2Cars.Text = Convert.ToString(Convert.ToInt32(road2Cars.Text) + random.Next(0, 5));
                        road3Cars.Text = Convert.ToString(Convert.ToInt32(road3Cars.Text) - random.Next(6, 7));
                        if (Convert.ToInt32(road3Cars.Text) < 0)
                            road3Cars.Text = "0";
                        road4Cars.Text = Convert.ToString(Convert.ToInt32(road4Cars.Text) + random.Next(0, 5));
                        Thread.Sleep(100);
                    }
                    break;
                case 4:
                    while (!(road4Cars.Text.Equals("0")))
                    {
                        road1Cars.Text = Convert.ToString(Convert.ToInt32(road1Cars.Text) + random.Next(0, 5));
                        road2Cars.Text = Convert.ToString(Convert.ToInt32(road2Cars.Text) + random.Next(0, 5));
                        road3Cars.Text = Convert.ToString(Convert.ToInt32(road3Cars.Text) + random.Next(0, 5));
                        road4Cars.Text = Convert.ToString(Convert.ToInt32(road4Cars.Text) - random.Next(6, 7));
                        if (Convert.ToInt32(road4Cars.Text) < 0)
                            road4Cars.Text = "0";
                        Thread.Sleep(100);
                    }
                    break;
            }
            Console.WriteLine(road1Cars.Text);
            Console.WriteLine(road2Cars.Text);
            Console.WriteLine(road3Cars.Text);
            Console.WriteLine(road4Cars.Text);
             
            
            
        }

        private void setTLights(int state)
        {
            road4Up.BackColor = Color.Red;
            road3Up.BackColor = Color.Red;
            road2Up.BackColor = Color.Red;
            road1Up.BackColor = Color.Red;

            if (state == 1)
            {
                road1Up.BackColor = Color.Gray;
                road1Down.BackColor = Color.Green;
                road2Down.BackColor = Color.Gray;
                road3Down.BackColor = Color.Gray;
                road4Down.BackColor = Color.Gray;
            }
            else if (state == 2)
            {
                road2Up.BackColor = Color.Gray;
                road2Down.BackColor = Color.Green;
                road1Down.BackColor = Color.Gray;
                road3Down.BackColor = Color.Gray;
                road4Down.BackColor = Color.Gray;
            }
            else if (state == 3)
            {
                road3Up.BackColor = Color.Gray;
                road3Down.BackColor = Color.Green;
                road1Down.BackColor = Color.Gray;
                road2Down.BackColor = Color.Gray;
                road4Down.BackColor = Color.Gray;
            }
            else if (state == 4)
            {
                road4Up.BackColor = Color.Gray;
                road4Down.BackColor = Color.Green;
                road1Down.BackColor = Color.Gray;
                road2Down.BackColor = Color.Gray;
                road3Down.BackColor = Color.Gray;
            }
        }
        private void setEngines()
        {
            roadpopulation = new MembershipFunctionCollection();

            roadpopulation.Add(new MembershipFunction("Low", -25, -25, 0, 20));
            roadpopulation.Add(new MembershipFunction("Mid", 10, 30, 30, 50));
            roadpopulation.Add(new MembershipFunction("High", 40, 60, 85, 85));

            road1 = new LinguisticVariable("Road1", roadpopulation);
            road2 = new LinguisticVariable("Road2", roadpopulation);
            road3 = new LinguisticVariable("Road3", roadpopulation);
            road4 = new LinguisticVariable("Road4", roadpopulation);

            nextGreenSelection = new MembershipFunctionCollection();
            nextGreenSelection.Add(new MembershipFunction("TrafficLight1", 0, 0, 1, 3));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight2", 3, 4, 4, 6));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight3", 6, 7, 7, 9));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight4", 9, 10, 10, 12));
            nextGreenSelection.Add(new MembershipFunction("Uncertain", 12, 13, 14, 14));
            nextGreen = new LinguisticVariable("NextGreen", nextGreenSelection);

            roadSelection = new FuzzyEngine();

            roadSelection.LinguisticVariableCollection.Add(road1);
            roadSelection.LinguisticVariableCollection.Add(road2);
            roadSelection.LinguisticVariableCollection.Add(road3);
            roadSelection.LinguisticVariableCollection.Add(road4);
            roadSelection.LinguisticVariableCollection.Add(nextGreen);

            

            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Low) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS Mid) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Low) AND (Road4 IS High) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS Mid) AND (Road4 IS High) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) AND (Road2 IS High) AND (Road3 IS High) AND (Road4 IS High) THEN NextGreen IS TrafficLight2"));

            roadSelection.Consequent = "NextGreen";
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runSimulation();
        }

        private void setLights()
        {
            int max = 0;
            if (Convert.ToInt32(road1Cars.Text) > max)
            {
                state = 1;
                max = Convert.ToInt32(road1Cars.Text);
            }
            if (Convert.ToInt32(road2Cars.Text) > max)
            {
                state = 2;
                max = Convert.ToInt32(road2Cars.Text);
            }
            if (Convert.ToInt32(road3Cars.Text) > max)
            {
                state = 3;
                max = Convert.ToInt32(road3Cars.Text);
            }
            if (Convert.ToInt32(road4Cars.Text) > max)
            {
                state = 4;
                max = Convert.ToInt32(road4Cars.Text);
            }
            if ((state == 2) && (road2Cars.Text.Equals("0"))){
                state = 3;
            }
        }
    }
}
