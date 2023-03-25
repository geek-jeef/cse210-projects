using System;
/***********************************************************************************************
 - Showing Creativity and Exceeding Requirements
 1) a game class to handle all gamification features
 2)Game data can be stored to a file in data directory
 3)Clean code
************************************************************************************************/
class Program
{
  static void Main(string[] args)
  {
    DisplayWelcomeMessage();
    int choice = -1;
    Game game = new Game();
    do
    {
      Console.WriteLine($"You have {game.getPoints()} points");
      ShowMenu();
      Console.Write("Please Type The Number Corresponding With Your Choice: ");
      choice = int.Parse(Console.ReadLine());
      switch (choice)
      {
        case 1:
          game.Create();
          break;
        case 2:
          game.List();
          break;
        case 3:
          game.Save();
          break;
        case 4:
          game.Load();
          break;
        case 5:
          game.Progress();
          break;
        case 0:
          DisplayEndMessage();
          break;
        default:
          Console.Clear();
          Console.WriteLine("Please Select a correct value");
          break;
      }

    } while (choice != 0);
  }

  static void DisplayWelcomeMessage()
  {
    Console.WriteLine("Welcome to the Gamification Program!");
  }

  static void DisplayEndMessage()
  {
    Console.Clear();
    Console.WriteLine("Good Bye");
  }

  static void ShowMenu()
  {
    Console.Write("""

    Menu Options :
      1) Create new Goal
      2) List of Goals
      3) Save Goals
      4) Load Goals
      5) Record Progress
      0) Quit

    """);
  }

}
