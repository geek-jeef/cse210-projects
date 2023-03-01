using System;
using System.IO; 
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void Display()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
            Console.WriteLine("");
        }
    }

    public void AddEntry()
    {
        Entry entry = new Entry();
        entry.Edit();
        _entries.Add(entry);
    }
    

    public void Save()
    {
        // get file name
        string filename = GetValidFileName();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                string line = string.Format("{0}~|~{1}~|~{2}",
                                entry._date,
                                entry._prompt,
                                entry._userText                                    
                            );
                outputFile.WriteLine($"{line}");
            }
        }
    }

    public void Load()
    {
        string filename = GetValidFileName();
        string[] lines = System.IO.File.ReadAllLines(filename);
        _entries.Clear();
        foreach (string line in lines)
        {
            string[] parts = line.Split("~|~");
            _entries.Add(new Entry(parts[0],parts[1],parts[2]));
        }
        Console.WriteLine("Entries are loaded from file");
    }

    string GetValidFileName(){

        bool filenameValidity = false;
        string filename;
        do
        {
            Console.WriteLine("Please enter a filename");
            filename = Console.ReadLine();
            if(IsValidfileName(filename))
            {
                filenameValidity = true;
            }else
            {
                Console.WriteLine("Incorrect filename! please try again");
            }
        }while (!filenameValidity);
        return filename;
    }

    bool IsValidfileName(string filename)
    {
        return !Regex.IsMatch(
            filename,
            string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))));
    }

}
