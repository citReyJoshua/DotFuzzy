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
        private DotFuzzy.FuzzyEngine greenLightDuration;

        private LinguisticVariable road1;
        private LinguisticVariable road2;
        private LinguisticVariable road3;
        private LinguisticVariable road4;
        private LinguisticVariable nextGreen;

        private MembershipFunctionCollection roadpopulation;
        private MembershipFunctionCollection nextGreenSelection;


        private Thread t;
        private Boolean shouldStart = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            roadSelection = new FuzzyEngine();
            greenLightDuration = new FuzzyEngine();
            setEngines();

            road1.InputValue = Convert.ToDouble(road1Cars.Text);
            Console.WriteLine(road1.InputValue);
            road1.Fuzzify("High");
            road2.InputValue = Convert.ToDouble(road2Cars.Text);
            Console.WriteLine(road2.InputValue);
            road2.Fuzzify("High");
            road3.InputValue = Convert.ToDouble(road3Cars.Text);
            Console.WriteLine(road3.InputValue);
            road3.Fuzzify("High");
            road4.InputValue = Convert.ToDouble(road4Cars.Text); 
            Console.WriteLine(road4.InputValue);
            road4.Fuzzify("High");
            nextGreen.Fuzzify("Uncertain");
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shouldStart = true;
            double result = roadSelection.Defuzzify();
            Console.WriteLine("Starting...");
            Console.WriteLine(result);
            //t = new Thread(runSimulation);
            //t.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shouldStart = false;
        }

        private void runSimulation()
        {
            double result = roadSelection.Defuzzify();
          Console.WriteLine("Starting...");
          Console.WriteLine(result);
          //Thread.Sleep(1000);
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
            nextGreenSelection.Add(new MembershipFunction("TrafficLight1", 0, 0, 1, 2));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight2", 3, 4, 4, 5));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight3", 6, 7, 7, 8));
            nextGreenSelection.Add(new MembershipFunction("TrafficLight4", 9, 10, 10, 11));
            nextGreenSelection.Add(new MembershipFunction("Uncertain", 12, 13, 14, 14));
            nextGreen = new LinguisticVariable("NextGreen", nextGreenSelection);

            roadSelection = new FuzzyEngine();

            roadSelection.LinguisticVariableCollection.Add(road1);
            roadSelection.LinguisticVariableCollection.Add(road2);
            roadSelection.LinguisticVariableCollection.Add(road3);
            roadSelection.LinguisticVariableCollection.Add(road4);
            roadSelection.LinguisticVariableCollection.Add(nextGreen);

            

            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Low) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS TrafficLight4"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight3"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight2"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS Mid) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Low) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS TrafficLight1"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS Mid) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Low) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS Mid) OR (Road4 IS High) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Low) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS Mid) THEN NextGreen IS Uncertain"));
            roadSelection.FuzzyRuleCollection.Add(new FuzzyRule("IF (Road1 IS High) OR (Road2 IS High) OR (Road3 IS High) OR (Road4 IS High) THEN NextGreen IS Uncertain"));

            roadSelection.Consequent = "NextGreen";
        }
    }
}
