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
            
            //Arrange panels for startup
            tagPanel.SendToBack();
            displayPanel.SendToBack();
            entryPanel.BringToFront();
            homeScrnTableLayoutPanel.BringToFront();
            
            dispEntryCountLbl.Text = "-/-"; //Initialize displayPanel labels

            //Update notebook lists from saved entries
            entryNbSelect.Items.Clear();
            entryNbSelect.Items.Add("Default");
            initNotebooks();
            updateNotebooks();

            updateEntryPanel(); //Initialize new entry panel for startup

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

            //Get all tags from CSV and populate dropdown menu on display screen
            updateTags();
            dispTagBox.SelectedIndex = 0;

            //Initialize Manager.f as main window and create function for when main window has focus
            Manager.f = this;
            this.Activated += Form1_Activated;

            Manager.currentQuery = String.Empty;    //Ensure query variable is clear

        }

        //Update panels whenever focus changes to main window
        private void Form1_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                updateDisplayPanel();
                updateNotebooks();
                Debug.WriteLine("Updated notebooks and display panel on new window focus");
            } catch (Exception ex)
            {
                Debug.Write(ex.Message + "\n");
                Debug.WriteLine("Could not update display panel or notebooks on new window focus");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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

        //Show home panel
        private void homeBtn2_Click(object sender, EventArgs e)
        {
            updateNotebooks();
            homeScrnTableLayoutPanel.BringToFront();
        }

        //Show notebook editing window
        private void nbEditBtn_Click(object sender, EventArgs e)
        {
            Form4 nbEditWindow = new Form4();
            nbEditWindow.Show();
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
                Entry newEntry = new Entry { Id = Manager.entryCSV.Count + 1, Title = titleTextBox.Text, CreatedDate = System.DateTime.Now, Date = System.DateTime.Now, 
                    Body = bodyTextBox.Text, Tags = listToString(Manager.currentEntryTags), Book = entryNbSelect.Text };
                Manager.entryCSV.Add(newEntry);
                Manager.writeEntryToCsv(newEntry);
                Manager.currentNb = entryNbSelect.SelectedIndex;

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
            if (Manager.dispInc + 2 <= Manager.entryResults.Count())
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

        //Open new window to edit currently showing entry
        private void dispEditEntryBtn_Click(object sender, EventArgs e)
        {
            updateTags();
            Form3 entryEditScreen = new Form3();
            entryEditScreen.Show();
        }

        //Delete currently showing entry
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //Show next entry available
            int tempDispInc = Manager.dispInc;
            if (Manager.dispInc + 2 <= Manager.entryResults.Count())
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

        //Show entries with selected tag
        private void dispTagBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Manager.dispInc = 0;
            if (dispTagBox.SelectedIndex == 0) //If no tag is selected, reset to default view
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

        //Get search results when enter key is pressed in search box
        void dispSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Debug.WriteLine("Search function entered");
                Manager.currentQuery = dispSearchBox.Text;
                Manager.searchEntries(dispSearchBox.Text);
                //updateDisplayPanel();
                e.Handled = true;
            }
        }
        
        //Use "Notebooks" button on display screen to reset to default view
        private void dispBtn2_Click(object sender, EventArgs e)
        {
            Manager.displayMode = "default";
            dispSearchBox.Text = String.Empty;
            Manager.currentQuery = String.Empty;
            updateDisplayPanel();
        }

        #endregion

        #region tag-btnFunctions
        //Edit currently selected tag and replace in all entries containing said tag
        private void tagEditBtn_Click(object sender, EventArgs e)
        {
            if (tagEditTextBox.Text == String.Empty)    //Does not allow tag name to be replaced with nothing
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

        //Deletes currently selected tag and removes from all entries to which it was applied
        private void tagDeleteBtn_Click(object sender, EventArgs e)
        {
            if (Manager.confirmDeleteTag)   //Deletes tag if delete button is clicked a second time
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
                //Prevents tag deletion until delete button is pressed twice
                tagEditErrLbl.Text = "Please press 'delete' again to confirm. Select another tag to cancel.";
                tagDeleteBtn.Text = "Confirm";
                System.Threading.Thread.Sleep(250);
                Manager.confirmDeleteTag = true;
            }
            
        }
        private void tagListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Manager.confirmDeleteTag = false;   //Cancels tag deletion if started
            tagDeleteBtn.Text = "Delete Tag";
            tagEditErrLbl.Text = String.Empty;
        }


        #endregion

        #region home-btnFunctions

        private void NewNbBtn_Click(object sender, EventArgs e)
        {
            Manager.notebookList.Add("New Notebook " + (Manager.notebookList.Count() - 1).ToString());
            Manager.nbSelected = Manager.notebookList.Count() - 1;
            Form4 nbEditWindow = new Form4();
            nbEditWindow.Show();
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Manager.currentNb = Int32.Parse(btn.Name);
            Manager.dispInc = 0;
            displayPanel.BringToFront();
            updateDisplayPanel();
        }

        #endregion

        #region update-functions
        //Functions to update labels on panel switch
        private void updateDisplayPanel()
        {
            if (Manager.entryCSV.Count > 0)
            {
                try
                {
                    Manager.entryCSV = Manager.getCsvEntries();
                }
                catch (Exception ex)
                {
                    //Rewrite CSV file if there was an error retrieving entries due to formatting
                    Debug.Write(ex.Message + "\n");
                    //Manager.initCsv();
                    Manager.rewriteCsv();
                    Debug.WriteLine("Emergency file rewrite");
                }
                Entry currentEntry = new Entry();
                switch (Manager.displayMode)   //Changes list of entries to display for different display modes
                {
                    default:
                        Manager.entryResults.Clear();
                        //Manager.entryResults = Manager.entryCSV;    //Displays all entries
                        Debug.WriteLine("Current Notebook: " + Manager.notebookList[Manager.currentNb]);
                        foreach (Entry e in Manager.entryCSV)
                        {
                            if (e.Book == Manager.notebookList[Manager.currentNb]) Manager.entryResults.Add(e);
                        }
                        //Show default notebook if current notebook is empty
                        /*if (Manager.entryResults.Count == 0)
                        {
                            Debug.WriteLine("Notebook was empty, showing default");
                            Manager.currentNb = 0;
                            foreach (Entry e in Manager.entryCSV)
                            {
                                if (e.Book == Manager.notebookList[Manager.currentNb]) Manager.entryResults.Add(e);
                            }
                        }*/
                        //Show notebook is empty if no entries return
                        if (Manager.entryResults.Count == 0)
                        {
                            Manager.entryResults.Add(new Entry { Body = "Notebook was empty" });
                        }
                        dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryResults.Count();
                        currentEntry = Manager.entryResults[Manager.dispInc];
                        dispIdLbl.Text = "Displaying: all entries in " + Manager.notebookList[Manager.currentNb];
                        dispSearchBox.Text = String.Empty;
                        Manager.currentQuery = String.Empty;
                        break;

                    case "id":
                        Manager.entryResults.Clear();
                        Manager.entryResults = Manager.getCsvEntriesWithTag(dispTagBox.Text);
                        dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryResults.Count();
                        currentEntry = Manager.entryResults[Manager.dispInc];
                        dispIdLbl.Text = "Displaying: [id]  " + dispTagBox.Text + "  |  Deselect tag to return";
                        break;

                    case "search":
                        Manager.entryResults.Clear();
                        switch (Manager.searchMode)   //Changes list of search results to display based on selected filter (changed in Form2)
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
                        dispEntryCountLbl.Text = (Manager.dispInc + 1).ToString() + "/" + Manager.entryResults.Count();   //Update page number label
                        currentEntry = Manager.entryResults[Manager.dispInc];
                        dispIdLbl.Text = "Displaying: [search results] " + dispSearchBox.Text + "  |  Close search results to return";
                        break;
                }
                //Update currently displaying entry title, body, and tags
                dispBodyText.Text = currentEntry.Body;
                dispTitleLbl.Text = currentEntry.Title;
                dispEntryData.Text = currentEntry.Date.DayOfWeek + ", " + currentEntry.Date.ToString("MMMM dd, h:mm") + "\n" +
                    "Created " + currentEntry.CreatedDate.ToString("dd MMM yyyy") + "\n" + "\n" + 
                    "Tags:  " + currentEntry.Tags;
            } else
            {
                dispBodyText.Text = "No entries saved";
                dispEntryCountLbl.Text = "-/-";
            }
        }
        
        //Reset text boxes and tag selections for new entry
        private void updateEntryPanel()
        {
            titleTextBox.Text = "New Entry";
            bodyTextBox.Text = String.Empty;
            selectedTagsLbl.Text = String.Empty;
            entryDateLbl.Text = DateTime.Now.ToString("dddd,\nd MMMM yyyy");
            entryNbSelect.SelectedIndex = Manager.currentNb;
            updateTags();
            Manager.currentEntryTags.Clear();
        }
        
        //Reset list of tags being displayed
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

        //Update notebook dropdowns
        public void initNotebooks()
        {
            Debug.WriteLine("Retrieving notebooks");
            Manager.notebookList.Clear();
            List<Entry> entries = Manager.getCsvEntries();
            Manager.notebookList.Add("Default");
            foreach (Entry e in entries)
            {
                if (!Manager.notebookList.Contains(e.Book))
                {
                    Manager.notebookList.Add(e.Book);
                    Debug.WriteLine("Added notebook " + e.Book);
                }
            }
        }
        public void updateNotebooks()
        {
            Debug.WriteLine("Updating notebook dropdowns");
            //initNotebooks();

            //Update notebook dropdown on new entry panel
            entryNbSelect.Items.Clear();
            foreach (string nb in Manager.notebookList)
            {
                entryNbSelect.Items.Add(nb);
            }

            //Add notebooks to home menu
            homeMenuNotebookFlow.Controls.Clear();
            //Debug.WriteLine("Adding notebooks");
            for (int i = 0; i < Manager.notebookList.Count; i++)
            {
                Button newBtn = new Button();
                newBtn.Name = i.ToString();
                newBtn.TextAlign = ContentAlignment.TopLeft;
                string btnName = "              " + Manager.notebookList[i];

                //Wrap text to fit background image
                for (int j = 1; j * 28 <= btnName.Length+1; j++)
                {
                    int n = j * 28;
                    while (true)
                    {
                        try
                        {
                            if (btnName[n] == ' ')
                            {
                                Debug.WriteLine("Space found at index " + n.ToString());
                                btnName = btnName.Insert(n, "\n             ");
                                break;
                            }
                            else n--;
                        } catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Debug.WriteLine("String was too short to wrap");
                            break;
                        }
                        
                    }
                }

                newBtn.Text = "\n\n\n\n\n\n" + btnName;
                newBtn.Font = new Font("League Spartan", 8);
                if (Manager.notebookList[i] == "Default")
                {
                    newBtn.BackgroundImage = Properties.Resources.notebookClosed;
                } else
                {
                    Random rand = new Random();
                    int randInt = rand.Next(1, 5);
                    switch (randInt)
                    {
                        default:
                            newBtn.BackgroundImage = Properties.Resources.notebookClosed;
                            break;
                    }
                }
                newBtn.Size = new Size(175,252);
                newBtn.BackgroundImageLayout = ImageLayout.Zoom;
                newBtn.FlatStyle = FlatStyle.Flat;
                newBtn.FlatAppearance.BorderSize = 0;
                //Set button position?
                newBtn.Click += NewBtn_Click;
                homeMenuNotebookFlow.Controls.Add(newBtn);
                Debug.WriteLine("Completed loop " + i.ToString());
            }
            //Add new notebook button at end of flow panel
            Button newNbBtn = new Button();
            newNbBtn.Name = "newNbBtn";
            newNbBtn.Font = new Font("League Spartan", 10);
            newNbBtn.BackColor = Color.Transparent;
            newNbBtn.ForeColor = Color.AliceBlue;
            newNbBtn.FlatStyle = FlatStyle.Flat;
            newNbBtn.FlatAppearance.BorderSize = 0;
            newNbBtn.BackgroundImage = Properties.Resources.plusIconDarkened;
            newNbBtn.Size = new Size(75, 75);
            newNbBtn.BackgroundImageLayout = ImageLayout.Zoom;
            newNbBtn.Anchor = ((System.Windows.Forms.AnchorStyles.Left));
            //Set position?
            newNbBtn.Click += NewNbBtn_Click;
            homeMenuNotebookFlow.Controls.Add(newNbBtn);
        }

        #endregion


        //Convert list to string separated by spaces, used for tags
        public static string listToString (List<string> list)
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

        //Functions to edit and delete tags used on tag menu panel
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

        private void homeNewEntryBtn_Click(object sender, EventArgs e)
        {
            entryBtn2_Click(sender, e);
        }

    }
}
