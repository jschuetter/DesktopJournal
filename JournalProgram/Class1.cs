using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
//Namespaces required to read/write to CSV file using CsvHelper plugin (found on NuGet or GitHub)
using System.Globalization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using CsvHelper;

namespace JournalProgram
{
    public class Manager
    {
        public static int dispInc = 0;  //Index of entry to display on display screen
        public static List<Entry> entryCSV = new List<Entry>(); //List of all current entries found in CSV file (initialized later)
        
        //Lists for sorting entries on display screen
        public static List<string> tagList = new List<string>();    //List of all tags currently being used
        public static List<string> currentEntryTags = new List<string>();   //List of all tags on current entry being made

        public static List<Entry> entryResults = new List<Entry>();   //List for storing search results

        public static bool confirmDeleteTag = false;    //Boolean to create delete confirmation dialog before deleting tag

        public static string displayMode = "default";   //Distinguish between display modes for disp screen -- search results, tags, notebooks, etc.
        public static int searchMode = 0;   //Distinguish between search modes - 0 for body, 1 for title, 2 for tag

        //Path for entry storage CSV file
        public static string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string csvPath = documentsFolderPath + @"\" + "journalEntry.csv";

        //Create lists to store query results in
        public static List<Result> bodyResults = new List<Result>();
        public static List<Result> titleResults = new List<Result>();
        public static List<Result> tagResults = new List<Result>();
        public static string currentQuery;  //Store current search query globally

        public static List<string> notebookList = new List<string>();  //Store names of all notebooks in use
        public static int nbSelected = 0;  //Allows selection of particular notebook in notebook edit window from other scripts
        public static int currentNb = 0;  //Stores index of notebook currently being written to / displayed

        public static Form f = new Form();  //Main display window (to allow activation from other forms)

        //Write headers into CSV file
        public static void initCsv()
        {
            using (var writer = new StreamWriter(csvPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Entry>();
                    csv.NextRecord();
                }
            }
            /*
             * Alternate format: 
             * Declares 'writer' as its own variable, but must be closed after every use

            var writer = new StreamWriter(csvPath);
            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<Entry>();
            csv.NextRecord();
            writer.Close();

            */
        }
        public static void writeEntryToCsv(Entry input)
        {
            //Append to existing CSV file
            bool append = true;
            //Prevent CsvHelper from overwriting CSV headers
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
            config.HasHeaderRecord = !append;

            using (var writer = new StreamWriter(csvPath, append))
            {
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecord(input);
                    csv.NextRecord();
                }
            }
        }

        //Get all entries in CSV as list of Entry objects
        public static List<Entry> getCsvEntries()
        {
            List<Entry> entryList = new List<Entry>();
            bool goodStart = true;

            using (var reader = new StreamReader(csvPath))
            { 
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var entry = new Entry();
                    var entries = csv.GetRecords<Entry>();
                    try
                    {
                        entryList = new List<Entry>(entries);
                    } catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        //Manager.reInitCsv();
                        goodStart = false;
                    }
                    
                }
            }
            if (!goodStart) reInitCsv();
            return entryList;
        }
        //Get list of only entries containing certain tag
        public static List<Entry> getCsvEntriesWithTag(string tag)
        {
            List<Entry> entryList = getCsvEntries();
            List<Entry> withTag = new List<Entry>();
            foreach (Entry e in entryList)
            {
                if (e.Tags.Contains(tag)) withTag.Add(e);
            }
            return withTag;
        }

        //Return results of search query
        //Mode 0 searches body, mode 1 searches title, mode 2 searches tags
        public static List<Result> getCsvEntriesWithKeyword(string key, int mode)
        {
            //Ensure that case-sensitivity does not affect results
            key = key.ToLower();
            //Split key into individual words for wider search
            List<string> keys = new List<string>();
            if (key.Contains(' '))
            {
                string[] keySplit = key.Split(' ');
                foreach (string s in keySplit)
                {
                    keys.Add(s);
                }
            } else keys.Add(key);

            List<Entry> entryList = getCsvEntries();
            List<Result> results = new List<Result>();
            bool resultsFound = false;

            //Search for results
            foreach (Entry e in entryList)
            {
     //Add condition for exact search here?
                foreach (string str in keys)
                {
                    switch (mode)
                    {
                        default:
                            if (e.Body.ToLower().Contains(str))
                            {
                                string resultContext;
                                int resultIndex;

                                string[] strSplit = e.Body.ToLower().Split(' ');

                                //Determine context in which result was found
                                int i = 0;
                                foreach (string s in strSplit)
                                {
                                    if (s.Contains(str))
                                    {
                                        resultIndex = i;
                                        break;
                                    }
                                    i++;
                                }
                                if (i - 1 >= 0)
                                {
                                    if (i + 2 < strSplit.Count())
                                    {
                                        resultContext = "\". . . " + strSplit[i - 1] + " " + strSplit[i] + " " + strSplit[i + 1] + " . . .\"";
                                    }
                                    else
                                    {
                                        resultContext = "\". . . " + " " + strSplit[i - 1] + " " + strSplit[i] + " . . .\"";
                                    }
                                }
                                else if (i + 1 < strSplit.Count())
                                {
                                    resultContext = ". . . " + strSplit[i] + " " + strSplit[i + 1] + " . . .";
                                }
                                else resultContext = "\". . . " + strSplit[i] + " . . .\"";

                                results.Add(new Result { Entry = e, Context = resultContext }) ;
                                Debug.WriteLine("Body result found: " + results[results.Count()-1].Entry.Title + ": " + results[results.Count()-1].Context);
                                resultsFound = true;
                            }
                            break;

                        case 1:
                            if (e.Title.ToLower().Contains(str))
                            {
                                results.Add(new Result { Entry = e, Context = String.Empty});
                                Debug.WriteLine("Title result found: " + results[results.Count() - 1].Entry.Title);
                                resultsFound = true;
                            }
                            break;
                        case 2:
                            if (e.Tags.ToLower().Contains(str))
                            {
                                results.Add(new Result { Entry = e, Context = e.Tags });
                                Debug.WriteLine("Tag result found: " + results[results.Count() - 1].Entry.Title);
                                resultsFound = true;
                            }
                            break;
                    }                    
                }
            }

            //Generate response if no results returned
            Entry noResult = new Entry { Title = "No results found" };
            if (!resultsFound)
            {
                results.Add(new Result { Entry = noResult, Context = "No results." });
                Debug.WriteLine("No results found");
            }

            return results;
        }
        //Populate result lists automatically and show search results screen
        public static void searchEntries(string search)
        {
            bodyResults.Clear();
            titleResults.Clear();
            tagResults.Clear();

            bodyResults = getCsvEntriesWithKeyword(search, 0);
            titleResults = getCsvEntriesWithKeyword(search, 1);
            tagResults = getCsvEntriesWithKeyword(search, 2);

            Manager.displayMode = "search";
            Manager.searchMode = 0;

            //Populate search results in popup window
            Form2 searchScreen = new Form2();
            searchScreen.Show();
        }

        //Rewrite CSV entries from locally stored entry list when entry is deleted or edited
        public static void rewriteCsv()
        {
            using (var writer = new StreamWriter(csvPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                { 
                    //Rewrite id fields
                    for (int i = 0; i < entryCSV.Count; i++)
                    {
                        Manager.entryCSV[i].Id = i;
                    }
                    csv.WriteRecords(entryCSV);
                    if (entryCSV.Count > 0) csv.NextRecord();       //Prevent creation of empty space in CSV file if all entries are deleted
                }
            }
        }
        //Overwrite entire CSV from local entry list in case of formatting error
        public static void reInitCsv()
        {
            initCsv();
            Debug.WriteLine("Re-init csv");
            rewriteCsv();
        }
    }

    //Create class for entries to be stored in local CSV file
    public class Entry
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate {get; set;}
        public string Body { get; set; }
        public string Tags { get; set; }
        public string Book { get; set; }
    }

    //Create class for search results
    public class Result
    {
        public Entry Entry { get; set; }
        public string Context { get; set; }
    }

}
