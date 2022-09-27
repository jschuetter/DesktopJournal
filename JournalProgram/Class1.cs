using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
//Namespaces required to read/write to .csv using CsvHelper
using System.Globalization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using CsvHelper;

namespace JournalProgram
{
    public class Manager
    {
        //Initialize entryList for all scripts
        public static List<string> entryList = new List<string>();
        public static Form1 entryScreen;
        //Form2 searchScreen = new Form2();
        public static int dispInc = 0;
        public static List<Entry> entryCSV = new List<Entry>();
        
        //Lists for sorting entries on display screen
        public static Dictionary<string, Button> dispNbBtns = new Dictionary<string, Button>();
        public static List<string> tagList = new List<string>();
        public static List<string> currentEntryTags = new List<string>();

        //List for storing search results
        public static List<Entry> entryResults = new List<Entry>();

        //Boolean to create delete confirmation dialog before deleting tag
        public static bool confirmDeleteTag = false;

        //Distinguish between display modes for disp screen -- search results, tags, notebooks, etc.
        public static string displayMode = "default";
        //Distinguish between search modes - 0 for body, 1 for title, 2 for tag
        public static int searchMode = 0;
        
        //Path for entry storage CSV file
        public static string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string csvPath = documentsFolderPath + @"\" + "journalEntry.csv";

        //Create lists to store results in
        public static List<Result> bodyResults = new List<Result>();
        public static List<Result> titleResults = new List<Result>();
        public static List<Result> tagResults = new List<Result>();

        public static Form f = new Form();


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
        public static List<Entry> getCsvEntries()
        {
            List<Entry> entryList = new List<Entry>();

            using (var reader = new StreamReader(csvPath))
            { 
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var entry = new Entry();
                    var entries = csv.GetRecords<Entry>();
                    entryList = new List<Entry>(entries);
                    
                }
            }
            return entryList;
        }
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

        //Mode 0 searches body, mode 1 searches title
        public static List<Result> getCsvEntriesWithKeyword(string key, int mode)
        {
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
        public static void searchEntries(string search)
        {
            bodyResults.Clear();
            titleResults.Clear();
            tagResults.Clear();

            bodyResults = getCsvEntriesWithKeyword(search, 0);
            titleResults = getCsvEntriesWithKeyword(search, 1);

            List<Entry> tempTagResults = getCsvEntriesWithTag(search);
            foreach (Entry e in tempTagResults)
            {
                tagResults.Add(new Result { Entry = e, Context = String.Empty});
            }

            Manager.displayMode = "search";
            Manager.searchMode = 0;

            //Populate search results in popup window
            Form2 searchScreen = new Form2();
            searchScreen.Show();
        }


        public static void rewriteCsv()
        {
            using (var writer = new StreamWriter(csvPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(Manager.entryCSV);
                    csv.NextRecord();
                }
            }
        }
        public static void reInitCsv()
        {
            initCsv();
            Debug.WriteLine("Re-init csv");
            rewriteCsv();
        }

        //public static bool stringNoCase
        
    }
    
    //Create class for entries to be stored in local CSV file
    public class Entry
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }

    //Create class for search results
    public class Result
    {
        public Entry Entry { get; set; }
        public string Context { get; set; }
    }
}
