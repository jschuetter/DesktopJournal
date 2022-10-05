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
        public Form2()
        {
            InitializeComponent();
            populateResults(0);
            this.FormClosing += Form2_FormClosing;
        }

        //Update main window when search window is closed
        //Focuses on updating display panel as default screen to return to
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form 2 closing");
            Entry currentResult = Manager.entryResults[Manager.dispInc];
            if (Manager.entryResults[0].Title == "No results found")
            {
                Manager.displayMode = "default";
                Manager.dispInc = 0;
            }

            //Find entry currently selected in overall list of entries and display on main window
            for (int i = 0; i < Manager.entryCSV.Count(); i++)
            {
                if (Manager.entryCSV[i].Body == currentResult.Body && Manager.entryCSV[i].Title == currentResult.Title)
                {
                    //Debug.WriteLine("Entry match found");
                    Manager.displayMode = "default";
                    Manager.dispInc = i;
                    break;
                }
            }
        }

        //Shows search results for title, body, and tags, respectively
        private void searchTitleBtn_Click(object sender, EventArgs e)
        {
            Manager.searchMode = 1;
            populateResults(1);
        }

        private void searchBodyBtn_Click(object sender, EventArgs e)
        {
            Manager.searchMode = 0;
            populateResults(0);
        }

        private void searchTagsBtn_Click(object sender, EventArgs e)
        {
            Manager.searchMode = 2;
            populateResults(2);
        }

        //Populates search results window with buttons for each result
        //Type 0 is body results, 1 for title results, 2 for tag results
        void populateResults (int type)
        {
            searchTitle.Text = "Search Results | " + "\"" + Manager.currentQuery + "\"";    //Set label at top of window

            resultsPanel.Controls.Clear();  //Clear any preexisting buttons

            //Get list of results to display
            List<Result> resultsToDisplay = new List<Result>();
            resultsToDisplay.Clear();
            switch (type)
            {
                default:
                    resultsToDisplay = Manager.bodyResults;
                    break;

                case 1:
                    resultsToDisplay = Manager.titleResults;
                    break;

                case 2:
                    resultsToDisplay = Manager.tagResults;
                    break;
            }

            //Create a new button for each result in resultsToDisplay
            //Button name corresponds to position in resultsToDisplay to indicate what result to display when clicked
            //(see resultClick function)
            for (int i = 0; i < resultsToDisplay.Count(); i++)
            {
                Button newBtn = new Button();
                switch (type)
                {
                    default:
                        newBtn.Text = resultsToDisplay[i].Context + " | " + resultsToDisplay[i].Entry.Title;
                        break;
                    case 1:
                        newBtn.Text = resultsToDisplay[i].Entry.Title;
                        break;
                    case 2:
                        newBtn.Text = resultsToDisplay[i].Entry.Title + " | " + resultsToDisplay[i].Entry.Tags;
                        break;
                }
                newBtn.Name = i.ToString();
                newBtn.Location = new Point(70, 70);
                newBtn.Size = new Size(400, 100);
                newBtn.TextAlign = ContentAlignment.MiddleLeft;
                newBtn.Padding = new Padding(24, 0, 0, 0);
                newBtn.Font = new Font("Avenir LT Std 55 Roman", 8);
                newBtn.BackColor = Color.LightSlateGray;
                newBtn.ForeColor = Color.AliceBlue;
                newBtn.FlatStyle = FlatStyle.Flat;
                newBtn.Click += resultClick;
                resultsPanel.Controls.Add(newBtn);
                Debug.WriteLine("Button added");
            }
        }

        //Function to run after a certain result button is clicked
        private void resultClick (object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Manager.dispInc = Int32.Parse(btn.Name);    //Display selected entry
            Manager.f.Activate();   //Activate main window
        }
    }
}
