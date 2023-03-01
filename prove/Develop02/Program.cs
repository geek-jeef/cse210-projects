using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();
        int choice = -1;
        Journal journal = new Journal();
        do
        {   
            //create a journal instance
            Console.WriteLine("Please select one of the following choices : ");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.WriteLine("What would you like to do ? ");
            choice = int.Parse(Console.ReadLine());

            switch(choice) 
            {
                case 1:
                    //write entry into journal instance
                    journal.AddEntry();
                    break;
                case 2:
                    // display all entry for journal instance
                    journal.Display();
                    break;
                case 3:
                    // load entry into journal instance from file
                    journal.Load();
                    break;
                case 4:
                    // save journal entries into file
                    journal.Save();
                    break;
                case 5:
                    // display good bye message
                    Console.WriteLine("Good Bye");
                    break;
                default:
                    // display error
                    Console.WriteLine("Please Select a correct value");
                    break;
            }
        }while (choice != 5);
    }
    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Journal program!");
    }
}