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
using System.IO;

namespace JournalProgram
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
            //Arrange panels for running
            tagPanel.SendToBack();
            displayPanel.SendToBack();
            entryPanel.BringToFront();

            //Initialize displayPanel labels
            //dispBodyText.Text = entryList[0];
            dispEntryCountLbl.Text = "-/-";

            //Initialize new entry panel for startup
            updateEntryPanel();

            //Test to see if file "journalEntries.csv" exists and whether proper headers are written in
            if (!File.Exists(Manager.csvPath))
            {
                Manager.initCsv();
                Debug.WriteLine("File does not exist, initializing");
            }
            else
            {
                Debug.WriteLine("File exists");
                try
                {
                    Manager.entryCSV = Manager.getCsvEntries();
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString() + "\n");
                    Manager.initCsv();
                    Debug.WriteLine("Emergency file init");
                }
            }

            //Get all tags from CSV and populate dropdown menus
            updateTags();
            dispTagBox.SelectedIndex = 0;

            this.Activated += Form1_Activated;
            
        }

        public static void activateMain()
        {
            // Manager.entryScreen.Activate();
            Manager.f.Activate();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            updateDisplayPanel();
            Debug.WriteLine("Updated display panel on new window focus");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Debug.WriteLine();
            //Manager.f = Form.ActiveForm;
            //Debug.WriteLine("Active form: " + Manager.f.Name);

        }

        private void bodyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        #region navigation-btnFunctions
        //Switch panel being displayed
        private void entryBtn2_Click(object sender, EventArgs e)
        {
            displayPanel.SendToBack();
            updateEntryPanel();
            entryPanel.BringToFront();
        }

        private void dispBtn1_Click(object sender, EventArgs e)
        {
            //Prepare entry display label & page count
            if (Manager.entryCSV.Count > 0)
            {
                updateDisplayPanel();
            } else
            {
                dispBodyText.Text = "No entries saved";
                dispEntryCountLbl.Text = "-/-";
            }
            //Update tag dropdown
            updateTags();
            //show entry display panel
            entryPanel.SendToBack();
            displayPanel.BringToFront();
        }

         //Program close button handlers
        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        //Open tags menu
        private void tagBtn1_Click(object sender, EventArgs e)
        {
            tagPanel.BringToFront();
            updateTagPanel();
            tagListBox.BringToFront();

            /*foreach (string str in Manager.tagList)
            {
                tagListBox.Items.Add(str);
                Debug.WriteLine("Added " + str);
            }
            this.Controls.Add(tagListBox);
            tagListBox.BringToFront();*/
        }

        #endregion

        #region newEntry-btnFunctions
        //Create new entry using text in bodyTextBox
        private void button1_Click(object sender, EventArgs e)
        {

            Debug.WriteLine("Submit Button Clicked");
            if (bodyTextBox.Text != String.Empty)
            {
                //Create new entry, add to local entry list, and write to .csv file
                Entry newEntry = new Entry { Id = Manager.entryCSV.Count + 1, Title = titleTextBox.Text, Date = System.DateTime.Now, Body = bodyTextBox.Text, Tags = listToString(Manager.currentEntryTags)};
                Manager.entryCSV.Add(newEntry);
                Manager.writeEntryToCsv(newEntry);

                //Update entryCSV list
                try
                {
                    Manager.entryCSV = Manager.getCsvEntries();
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString() + "\n");
                    Manager.initCsv();
                    Debug.WriteLine("Emergency file init");
                }
                for (int i = 0; i < Manager.entryCSV.Count; i++)
                {
                    Debug.WriteLine("From csv: " + Manager.entryCSV[i].Body);
                }
            }
            updateEntryPanel();
            updateDisplayPanel();
            outputLbl.Text = System.DateTime.Now.ToString() + ": Entry added!";
            Debug.WriteLine(System.DateTime.Now.ToString() + ": Entry added!");
        }
        //Add & remove tags to current entry
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
            //Removes all tags
            Manager.currentEntryTags.Clear();
            selectedTagsLbl.Text = String.Empty;
        }

        #endregion

        #region display-btnFunctions
        //Switch entry being displayed
        private void nextEntryBtn_Click(object sender, EventArgs e)
        {
            if (Manager.dispInc + 2 <= Manager.entryCSV.Count())
            {
                Manager.dispInc++;
                updateDisplayPanel();
            }
        }
        private void prevEntryBtn_Click(object sender, EventArgs e)
        {
            if (Manager.dispInc > 0)
            {
                Manager.dispInc--;
                updateDisplayPanel();
            }
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //Show next entry available
            int tempDispInc = Manager.dispInc;
            if (Manager.dispInc + 2 <= Manager.entryCSV.Count())
            {
                //Manager.dispInc++;
                Debug.WriteLine("dispInc = " + Manager.dispInc.ToString());
                //updateDisplayPanel();
            } else if (Manager.dispInc > 0)
            {
                Manager.dispInc--;
                Debug.WriteLine("dispInc = " + Manager.dispInc.ToString());
                //updateDisplayPanel();
            } else
            {
                dispBodyText.Text = "No entries saved";
                dispTitleLbl.Text = String.Empty;
                dispEntryCountLbl.Text = "-/-";
            }

            //Delete previously showing entry
            Manager.entryCSV.RemoveAt(tempDispInc);
            Debug.WriteLine("Entry Removed");
            Manager.rewriteCsv();
            updateDisplayPanel();

        }
        private void dispTagBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Manager.dispInc = 0;
            if (dispTagBox.SelectedIndex == 0)
            {
                Manager.displayMode = "default";
                Debug.WriteLine("DisplayMode = default");
            }
            else
            {
                Manager.displayMode = "id";
                Debug.WriteLine("DisplayMode = id"); 
            }
            updateDisplayPanel();
        }
        void dispSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Debug.WriteLine("Search function entered");
                Manager.searchEntries(dispSearchBox.Text);
                //updateDisplayPanel();
                e.Handled = true;
            }
        }

        #endregion

        #region tag-btnFunctions
        private void tagEditBtn_Click(object sender, EventArgs e)
        {
            if (tagEditTextBox.Text == String.Empty)
            {
                tagEditErrLbl.Text = "Please enter a valid tag name in the text box.";
            }
            else
            {
                editTag(tagEditTextBox.Text, tagListBox.SelectedIndex);
                updateTags();
                updateTagPanel();
                tagEditErrLbl.Text = String.Empty;
            }
        }

        private void tagDeleteBtn_Click(object sender, EventArgs e)
        {
            if (Manager.confirmDeleteTag)
            {
                deleteTag(tagListBox.SelectedIndex);
                tagDeleteBtn.Text = "Delete Tag";
                Manager.confirmDeleteTag = false;
                tagEditErrLbl.Text = String.Empty;
                updateTags();
                updateTagPanel();
            }
            else
            {
                tagEditErrLbl.Text = "Please press 'delete' again to confirm. Select another tag to cancel.";
                tagDeleteBtn.Text = "Confirm";
                System.Threading.Thread.Sleep(250);
                Manager.confirmDeleteTag = true;
            }
            
        }
        private void tagListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Manager.confirmDeleteTag = false;
            tagDeleteBtn.Text = "Delete Tag";
            tagEditErrLbl.Text = String.Empty;
        }


        #endregion

        #region update-functions
        //Functions to update labels on panel switch
        private void updateDisplayPanel()
        {
            try
            {
                Manager.entryCSV = Manager.getCsvEntries();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString() + "\n");
                //Manager.initCsv();
                Manager.rewriteCsv();
                Debug.WriteLine("Emergency file rewrite");
            }
            Entry currentEntry = new Entry();
            switch (Manager.displayMode)
            {
                default:
                    dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryCSV.Count();
                    currentEntry = Manager.entryCSV[Manager.dispInc];
                    dispIdLbl.Text = "Displaying: all entries";
                    break;

                case "id":
                    Manager.entryResults.Clear();
                    Manager.entryResults = Manager.getCsvEntriesWithTag(dispTagBox.Text);
                    dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryResults.Count();
                    currentEntry = Manager.entryResults[Manager.dispInc];
                    dispIdLbl.Text = "Displaying: [id]  " + dispTagBox.Text;
                    break;

                case "search":
                    Manager.entryResults.Clear();
                    switch (Manager.searchMode)
                    {
                        default:
                            foreach (Result r in Manager.bodyResults)
                            {
                                Manager.entryResults.Add(r.Entry);
                            }
                            break;
                        case 1:
                            foreach (Result r in Manager.titleResults)
                            {
                                Manager.entryResults.Add(r.Entry);
                            }
                            break;
                        case 2:
                            foreach (Result r in Manager.tagResults)
                            {
                                Manager.entryResults.Add(r.Entry);
                            }
                            break;
                    }
                    dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryResults.Count();
                    currentEntry = Manager.entryResults[Manager.dispInc];
                    dispIdLbl.Text = "Displaying: [search results] " + dispSearchBox.Text;
                    break;
            }
            
            dispBodyText.Text = currentEntry.Body;
            dispTitleLbl.Text = currentEntry.Title + "  |  " + currentEntry.Date.DayOfWeek + ", " + currentEntry.Date.ToString("MMMM dd, h:mm");
            dispEntryTags.Text = "  |   Tags:  " + currentEntry.Tags;
        }
        private void updateEntryPanel()
        {
            titleTextBox.Text = "New Entry";
            bodyTextBox.Text = String.Empty;
            selectedTagsLbl.Text = String.Empty;
            updateTags();
            Manager.currentEntryTags.Clear();
        }
        public void updateTagPanel()
        {
            tagListBox.Items.Clear();
            foreach (string str in Manager.tagList)
            {
                tagListBox.Items.Add(str);
            }
        }

        //Update tag dropdowns on both entry and display screens
        public void updateTags()
        {
            Debug.WriteLine("Retrieving tags");
            List<string> tags = new List<string>();
            List<Entry> entries = Manager.getCsvEntries();

            List<string> entryTags = new List<string>();   //All tags on selected entry
            foreach (Entry e in entries)
            {
                entryTags.Clear();
                if (e.Tags == String.Empty)
                {
                    Debug.WriteLine("No tags, skipping");
                    continue;
                }
                string[] eTags = e.Tags.Split(' ');
                foreach (string str in eTags)
                {
                    entryTags.Add(str);
                }
                foreach (string str in entryTags)
                {
                    if (!tags.Contains(str))
                    {
                        tags.Add(str);
                        Debug.WriteLine(str);
                    }
                }
            }
            Manager.tagList = tags;
            entryTagSelect.Items.Clear();
            dispTagBox.Items.Clear();
            dispTagBox.Items.Add(String.Empty);
            foreach (string tag in tags)
            {
                entryTagSelect.Items.Add(tag);
                dispTagBox.Items.Add(tag);
            }
        }

        #endregion

        private void homeBtn2_Click(object sender, EventArgs e)
        {
            
        }

        
        public string listToString (List<string> list)
        {
            string retStr = String.Empty;
            foreach (string i in list)
            {
                //retStr = i + " ";
                if (retStr == String.Empty) retStr = i;
                else retStr += " " + i;
            }
            return retStr;
        }

        public void editTag(string newTag, int index)
        {
            if (newTag.Contains(' ')) newTag.Replace(' ', '-');
            string oldTag = Manager.tagList[index];
            Manager.tagList[index] = newTag;
            Debug.WriteLine("Replaced old " + oldTag + " with " + Manager.tagList[index]);
            foreach (Entry e in Manager.entryCSV)
            {
                if (e.Tags.Contains(oldTag))
                {
                    e.Tags = e.Tags.Replace(oldTag, newTag);
                    
                }
            }
            Manager.rewriteCsv();
        }
        public void deleteTag(int index)
        {
            string oldTag = Manager.tagList[index];
            Manager.tagList.RemoveAt(index);
            Debug.WriteLine("Deleted tag " + oldTag + " at index " + index.ToString());
            int i = 0;
            foreach (string str in Manager.tagList)
            {
                Debug.WriteLine("tagList[" + i.ToString() + "]: " + str);
                i++;
            }
            foreach (Entry e in Manager.entryCSV)
            {
                if (e.Tags.Contains(oldTag))
                {
                    if (e.Tags.Contains(' ')) e.Tags = e.Tags.Replace(oldTag + " ", String.Empty);
                    else e.Tags = null;
                    Debug.WriteLine("New tags: " + e.Tags);
                }
            }
            Manager.rewriteCsv();
        }
        //Repeat btn_click functions for other layout panels
        private void tagBtn2_Click(object sender, EventArgs e)
        {
            tagBtn1_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            entryBtn2_Click(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dispBtn1_Click(sender, e);
        }

       
    }
}
