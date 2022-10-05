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
    public partial class Form3 : Form
    {
        public Entry currentResult = new Entry();
        public int entryIndex;
        public Form3()
        {
            InitializeComponent();
            populateEntryData();
            this.Activated += Form3_Activated;
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //populateEntryData();
        }

        private void encySubmitBtn_Click(object sender, EventArgs e)
        {
            if(bodyTextBox.Text != String.Empty)
            {
                Entry editedEntry = new Entry { Title = titleTextBox.Text, Body = bodyTextBox.Text, Date = DateTime.Now, CreatedDate = currentResult.CreatedDate, 
                    Tags = Form1.listToString(Manager.currentEntryTags), Book =  Manager.notebookList[entryNbSelect.SelectedIndex] };
                Manager.currentNb = entryNbSelect.SelectedIndex;
                Manager.entryCSV[entryIndex] = editedEntry;
                Debug.WriteLine("Entry index " + entryIndex.ToString());    
                Manager.rewriteCsv();
                Debug.WriteLine("CSV rewritten");
                Manager.f.Activate();
                this.Close();
            }            
        }

        private void populateEntryData() 
        {
            //Entry currentResult = new Entry();
            //int entryIndex;
            /*if (Manager.displayMode == "default")
            {
                currentResult = Manager.entryResults[Manager.dispInc];
                entryIndex = Manager.dispInc;
            }
            else
            {
                currentResult = Manager.entryResults[Manager.dispInc];
                //Find entry currently selected in overall list of entries and display on main window
                for (int i = 0; i < Manager.entryCSV.Count(); i++)
                {
                    if (Manager.entryCSV[i].Body == currentResult.Body && Manager.entryCSV[i].Title == currentResult.Title)
                    {
                        //Debug.WriteLine("Entry match found");
                        //Manager.displayMode = "default";
                        //Manager.dispInc = i;
                        entryIndex = i;
                        break;
                    }
                }
            }*/
            currentResult = Manager.entryResults[Manager.dispInc];
            //Find entry currently selected in overall list of entries and display on main window
            for (int i = 0; i < Manager.entryCSV.Count(); i++)
            {
                if (Manager.entryCSV[i].Body == currentResult.Body && Manager.entryCSV[i].Title == currentResult.Title)
                {
                    //Debug.WriteLine("Entry match found");
                    //Manager.displayMode = "default";
                    //Manager.dispInc = i;
                    entryIndex = i;
                    break;
                }
            }

            titleTextBox.Text = currentResult.Title;
            bodyTextBox.Text = currentResult.Body;
            selectedTagsLbl.Text = currentResult.Tags;

            //Add all tags currently on entry to global tagList for current entry
            string[] tagStr = currentResult.Tags.Split(' ');
            foreach (string str in tagStr)
            {
                Debug.WriteLine("Added " + str + " to tags");
                Manager.currentEntryTags.Add(str);
            }
            
            //Get all tags currently in use to populate add tag dropdown
            foreach (string tag in Manager.tagList)
            {
                entryTagSelect.Items.Add(tag);
            }

            //Get all notebooks & add to dropdown menu
            Debug.WriteLine("Retrieving notebooks");
            List<Entry> entries = Manager.getCsvEntries();
            if (entries.Count == 0) Manager.notebookList.Add("Default");
            foreach (Entry e in entries)
            {
                if (!Manager.notebookList.Contains(e.Book))
                {
                    Manager.notebookList.Add(e.Book);
                    Debug.WriteLine("Added notebook " + e.Book);
                }
            }
            //Update notebook dropdown on new entry panel
            entryNbSelect.Items.Clear();
            foreach (string nb in Manager.notebookList)
            {
                entryNbSelect.Items.Add(nb);
            }
            entryNbSelect.Text = currentResult.Book;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addTagBtn_Click(object sender, EventArgs e)
        {
            //Ensure there are no spaces in tag name to keep tag sorting functional
            if (entryTagSelect.Text.Contains(' '))
            {
                entryTagSelect.Text.Replace(' ', '-');
            }
            //Check for duplicate tags
            if (!Manager.currentEntryTags.Contains(entryTagSelect.Text)) Manager.currentEntryTags.Add(entryTagSelect.Text);
            //Display current tags
            selectedTagsLbl.Text = String.Empty;
            foreach (string i in Manager.currentEntryTags)
            {
                if (selectedTagsLbl.Text == String.Empty) selectedTagsLbl.Text = i;
                else selectedTagsLbl.Text += ", " + i;
            }
            entryTagSelect.Text = String.Empty;
        }

        private void deleteTagsBtn_Click(object sender, EventArgs e)
        {
            Manager.currentEntryTags.Clear();
            selectedTagsLbl.Text = String.Empty;
        }
    }
}
