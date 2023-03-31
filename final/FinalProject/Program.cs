using System;
/***********************************************************************************************
Information
1) this program use sqlite to store data

2) Demo credentials

Demo Teacher username: brad ; password : brad

Demo Student username: jeef ; password : jeef

3) Source :
    OpenTriviaQA
    A creative commons dataset of trivia, multiple choice questions and answers.
    url: https://github.com/uberspot/OpenTriviaQA

************************************************************************************************/
class Program
{
  public static Database database = new Database();
  
  static void Main(string[] args)
  {
    DisplayWelcomeMessage();
    User user =  null;
    while(user == null){
      user = Authentication();
    }

    int choice = -1;

    if($"{user.GetType()}"  == "Student")
    {
      QuizTaker quizTaker = new QuizTaker((Student)user);
      do{
        Console.Clear();
        Console.WriteLine($"Hello Student {user.GetUsername()}");
        StudentMenu();
        Console.Write("Please Type The Number Corresponding With Your Choice: ");
        choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
          case 1:
            quizTaker.PrintQuizList();
            break;
          case 2:
            quizTaker.PrintGradeList();
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
    else if ($"{user.GetType()}"  == "Teacher"){

      QuizManager quizManager = new QuizManager((Teacher)user);
      do
      {
        Console.Clear();
        Console.WriteLine($"Hello Teacher {user.GetUsername()}");
        TeacherMenu();
        Console.Write("Please Type The Number Corresponding With Your Choice: ");
        choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
          case 1:
            quizManager.CreateQuiz();
            break;
          case 2:
            quizManager.PrintQuizList();
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
  }

  static void DisplayWelcomeMessage()
  {
    Console.WriteLine("Welcome to the Quiz App!");
  }

  static User Authentication()
  {
    int choice = -1;
    User user = null;
    do
    {
      Console.WriteLine("""
      Authentication :
          1) Login
          2) Register
          0) Exit

      """);
      Console.Write("Choose one authentication option : ");
      choice = int.Parse(Console.ReadLine());
      switch (choice)
      {
        case 1:
          user = Login();
          break;
        case 2:
          user = Register();
          break;
        case 0:
          user = null;
          break;
        default:
          Console.Write("Choose a correct authentication option : ");
          break;
      }
    }while( (user == null ) || (choice == 0 ) );

    return user;
  }

  static User Login()
  {
    Console.Write("Enter your username : ");
    string username = Console.ReadLine();

    Console.Write("Enter your password: ");
    string password = Console.ReadLine();

    if(database.VerifyUser(username,password)){
      return database.GetUserByName(username);
    }else{
      Console.WriteLine("Authentication Fail if you dont have account please register : ");
      return null;
    }
  }

  static User Register()
  {
    Console.Write("Enter your username : ");
    string username = Console.ReadLine();
    Console.Write("Enter your password: ");
    string password = Console.ReadLine();

    Console.WriteLine("""
    User Type :
        1) Student
        2) Teacher
        0) Exit

    """);

    int choice = -1 ;
    string type = "";

    do
    {
      Console.Write("Choose a user type: ");
      choice = int.Parse(Console.ReadLine());

      if(choice == 1){
        type = "Student";
      }else if(choice == 2){
        type = "Teacher";
      }else{
        Console.WriteLine("Choose a correct user type");
      }
    } while ( (choice != 0) && (type == ""));

    return database.createUser(type,username, password);
  }

  static void DisplayEndMessage()
  {
    Console.Clear();
    Console.WriteLine("Good Bye");
  }

  static void TeacherMenu()
  {
    Console.Write("""

    Menu Options :
      1) Create a Quiz
      2) List of Quiz
      0) Quit

    """);
  }

  static void StudentMenu()
  {
    Console.Write("""

    Menu Options :
      1) List of Quiz
      2) Grade
      0) Quit

    """);
  }

}
