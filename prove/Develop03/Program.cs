using System;

/***********************************************************************************************
 - Showing Creativity and Exceeding Requirements

 1) This program work with a library of scriptures rather than a single one.
 2) This program loads scriptures from a file

************************************************************************************************/
class Program
{
    static void Main(string[] args)
    {
        int choice = -1;
        do
        {   
            Console.Write("""
            Would you like to:

            1) Memorize A Random Scripture
            2) Select A Preexisting Scripture From The Data Base
            3) Add A New Scripture To The Database
            0) Exit

            Please Type The Number Corresponding With Your Choice: 
            """);
            
            choice = int.Parse(Console.ReadLine());

            switch(choice) 
            {
                case 1:
                    MemorizeRandom();
                    break;
                case 2:
                    MemorizeSelected();
                    break;
                case 3:
                    AddScriptureToDatabase();
                    break;
                case 0:
                    // display good bye message
                    Console.Clear();
                    Console.WriteLine("Good Bye");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please Select a correct value");
                    break;
            }
        }while (choice != 0);
    }


    public static void MemorizeRandom(){
        Console.Clear();

        Random random = new Random();
        List<string> scripturLibrary = LoadLibrary();
        int selectedIndex = random.Next(0, scripturLibrary.Count());
        string[] selectedVerse = scripturLibrary[selectedIndex].Split("|");
        Scripture scripture = new Scripture(selectedVerse[0], selectedVerse[1], selectedVerse[2], selectedVerse[3]);

        MemorizeScripture(scripture);
    }

    public static void MemorizeSelected(){
        Console.Clear();
        List<string> scripturLibrary = LoadLibrary();
        List<string> referenceList = new List<string>();
        for (int i = 0; i < scripturLibrary.Count(); i++)
        {
            string[] parts = scripturLibrary[i].Split("|");
            referenceList.Add($"{parts[0]} {parts[1]}:{parts[2]}");
            Console.WriteLine($"{i + 1}) {referenceList[i]}");
        }
        Console.Write("\nPlease type the number for the scripture reference you want to memorize: ");
        string selectedReference = Console.ReadLine();
        int selectedIndex = int.Parse(selectedReference) - 1;
        string[] selectedVerse = scripturLibrary[selectedIndex].Split("|");
        Scripture scripture = new Scripture(selectedVerse[0], selectedVerse[1], selectedVerse[2], selectedVerse[3]);

        MemorizeScripture(scripture);
    }

    public static void MemorizeScripture(Scripture scripture){
        Console.Write("How many words would you like to have disapear each time?\nPlease enter an integer: ");
        int difficulty = int.Parse(Console.ReadLine());
        string userInput = "";
        while(userInput != "quit")
        {
            Console.Clear();
            Console.Write($"{scripture.GetRefrence()} -> ");
            scripture.DisplayScripture();
            if(!scripture.AllHidden()){
                scripture.HideWords(difficulty);
                Console.Write("\nPress ENTER to continue or type 'quit' to exit: ");
                userInput = Console.ReadLine();
            }else{
                break;
            }
        }
        if(userInput != "quit"){
            Console.Write("\nPress any key to end memorization ");
            Console.ReadKey();
        }
        Console.Clear();
    }

    public static void AddScriptureToDatabase(){
        Console.Write("Type The Name Of The Book: ");
        string newScripture = Console.ReadLine() + "|";
        Console.Write("Type The Chapter Number: ");
        newScripture += Console.ReadLine() + "|";
        Console.Write("Type The Verse Number(s): (Ex. 5 or 2-7) ");
        newScripture += Console.ReadLine() + "|";
        Console.Write("Type Out The Verse(s): ");
        newScripture += Console.ReadLine();
        NewEntry(newScripture);
        Console.Clear();
        Console.WriteLine("Scripture added to database");
        Console.WriteLine();
    }

    public static List<string> LoadLibrary()
    {
        List<string> library = new List<string>();
        string[] lines = System.IO.File.ReadAllLines("ScripturLibrary.txt");
        foreach (string line in lines)
        {
            library.Add(line);
        }
        return library;
    }

    public static void NewEntry(string newLine)
    {
        List<string> oldLibrary = LoadLibrary();
        using (StreamWriter streamWriter = new StreamWriter("ScripturLibrary.txt"))
        {
            foreach (string entry in oldLibrary)
            {
                streamWriter.WriteLine(entry);
            }
            streamWriter.WriteLine(newLine);
        }
    }
}