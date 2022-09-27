using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace JournalProgram
{
    public partial class Form2 : Form
    {
        //List<Result> resultsToDisplay = new List<Result>();

        public Form2()
        {
            InitializeComponent();
            populateResults(0);
        }

        private void searchTitleBtn_Click(object sender, EventArgs e)
        {
            populateResults(1);
        }

        private void searchBodyBtn_Click(object sender, EventArgs e)
        {
            populateResults(0);
        }

        private void searchTagsBtn_Click(object sender, EventArgs e)
        {
            populateResults(2);
        }

        //Type 0 is body results, 1 for title results, 2 for tag results
        void populateResults (int type)
        {
            resultsPanel.Controls.Clear();
            List<Result> resultsToDisplay = new List<Result>();
            resultsToDisplay.Clear();
            string nameVar;
            switch (type)
            {
                default:
                    resultsToDisplay = Manager.bodyResults;
                    nameVar = "bodyResults";
                    break;

                case 1:
                    resultsToDisplay = Manager.titleResults;
                    nameVar = "titleResults";
                    break;

                case 2:
                    resultsToDisplay = Manager.tagResults;
                    nameVar = "tagResults";
                    break;
            }

            for (int i = 0; i < resultsToDisplay.Count(); i++)
            {
                Button newBtn = new Button();
                newBtn.Text = resultsToDisplay[i].Context + " | " + resultsToDisplay[i].Entry.Title;
                newBtn.Name = i.ToString();
                //newBtn.Name = "resultBtn" + resultsToDisplay[i].Entry.Id;
                newBtn.Location = new Point(70, 70);
                newBtn.Size = new Size(400, 100);
                newBtn.TextAlign = ContentAlignment.MiddleLeft;
                newBtn.Padding = new Padding(24, 0, 0, 0);
                resultsPanel.Controls.Add(newBtn);
            }
        }

        private void resultClick (object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Manager.dispInc = Int32.Parse(btn.Name);
            Debug.WriteLine("Button name = " + Manager.dispInc.ToString());
            Form1.activateMain();
        }
    }
}
