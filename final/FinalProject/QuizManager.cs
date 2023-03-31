using System.IO; 
using System.Text.Json;
using static System.Guid;

public class QuizManager
{
  private List<Quiz> _quizzes;
  private Teacher _user;
  private Database _database;

  public QuizManager(Teacher user)
  {
    _user = user;
    _database = new Database(_user);
    GetQuizzesFromDatabase();
  }

  public void CreateQuiz(){
    Console.Write("Enter the quiz title : ");
    string title = Console.ReadLine();
    Console.Write("Enter the quiz description: ");
    string description = Console.ReadLine();
    Console.Write("Enter the quiz duration (0 for no time limit): ");
    int duration = int.Parse(Console.ReadLine());
    Console.Write("Enter the quiz passing mark (%): ");
    int passMark = int.Parse(Console.ReadLine());
    Quiz quiz = new Quiz(title, description, duration , passMark);
    AddQuestionToQuiz(ref quiz);
    Console.WriteLine("Please Wait during storing to database");
    _database.AddQuiz(quiz);
    GetQuizzesFromDatabase();
  }

  private void AddQuestionToQuiz( ref Quiz quiz)
  {
    int choice = -1 ;
    Console.WriteLine("""

    Choose a way to add Question To Quiz :
      1) From json file dataset
      2) Manually
      0) Quit and Add question later

    """);
    Console.Write("Please Type The Number Corresponding With Your Choice: ");
    choice = int.Parse(Console.ReadLine());
    if(choice == 1)
    {
      //show file
      Console.WriteLine("Question quiz will be load from a files");
      try
      {
        string[] dirs = Directory.GetFiles(@"Database\files\", "*.json");
        for (int i = 0; i < dirs.Length; i++)
        {
          Console.WriteLine($"{i + 1}. {dirs[i]}");
        }
        Console.Write("Choose the file to load:");
        int fileIndex = int.Parse(Console.ReadLine());
        if(dirs[fileIndex-1] != null){
          string filename = dirs[fileIndex-1] ;
          string jsonString = File.ReadAllText(filename);
          List<Dictionary<string, string>>  questionList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString)!;

          Console.WriteLine($"They are {questionList.Count()} questions in this file");
          int questionNumber = -1;
          do
          {
            Console.Write("How many of them do you want to use :");
            questionNumber = int.Parse(Console.ReadLine());
          } while ((questionNumber <= 0) || (questionNumber > questionList.Count() ));
          List<Dictionary<string, string>>  questionsSelected = questionList.OrderBy(x => Guid.NewGuid()).Take(questionNumber).ToList();

          foreach (Dictionary<string, string> item in questionsSelected)
          {
            if(item["type"] == typeof(MultipleChoiceQuestion).ToString() ){
              MultipleChoiceQuestion question = new MultipleChoiceQuestion(item["question"],item["answer"],item["choices"],"\n");
                if(question != null){quiz.AddQuestion(question);}else{Console.WriteLine("Invalid question");};
            }else if(item["type"] == typeof(TrueFalseQuestion).ToString() ){
              TrueFalseQuestion question = new TrueFalseQuestion(item["question"], (item["answer"] == true.ToString()));
                if(question != null){quiz.AddQuestion( question);}else{Console.WriteLine("Invalid question");};
            }else{
              Question question = new Question(item["question"], item["answer"]);
              if(question != null){quiz.AddQuestion(question);}else{Console.WriteLine("Invalid question");};
            };
          }
        }
      }
      catch (Exception e)
      {
          Console.WriteLine("The process failed: {0}", e.ToString());
      }

    }
    else if(choice == 2)
    {
      int questionCount = 0;
      int action = -1 ;

      do
      {
        Console.WriteLine($" You have added {questionCount} to this quiz :");
        Console.WriteLine("""

        Choose a type of Question To Add :
          1) Simple Question
          2) Multiple Choice Question
          3) True / False Question
          0) Exit

        """);
        Console.Write("let go: ");
        action = int.Parse(Console.ReadLine());

        switch (action)
        {
          case 1 :
              // ask question
            var response = CreateQuestion(typeof(Question).ToString());
            if(response != null){ quiz.AddQuestion( response); Console.WriteLine("Question added.");}else{Console.WriteLine("Error - Invalid question");};
            questionCount++;
            break;
          case 2 :
            var response2 = CreateQuestion(typeof(MultipleChoiceQuestion).ToString());
            if(response2 != null){ quiz.AddQuestion( response2); Console.WriteLine("Question added.");}else{Console.WriteLine("Error - Invalid question");};
            questionCount++;
            break;
          case 3 :
            var response3 = CreateQuestion(typeof(TrueFalseQuestion).ToString());
            if(response3 != null){ quiz.AddQuestion( response3); Console.WriteLine("Question added.");}else{Console.WriteLine("Error - Invalid question");};
            questionCount++;
            break;
          case 0 :
            Console.WriteLine("no more question for this quiz");
            break;
          default:
            Console.WriteLine("Invalid option. press 0 to exit");
            break;
        }
        
      } while( action != 0);
    }
    else
    {
      Console.Write("Ok, the question can be add later");
    }
  }


  private void SetQuestionAnswer(string type, string text , ref Question question){

    string answer ;

    if(type == typeof(Question).ToString() )
    {
      Console.Write("Enter the answer : ");
      answer = Console.ReadLine();
      question = new Question(text, answer);
    }
    else if(type == typeof(TrueFalseQuestion).ToString() )
    {
      do
      {
        Console.WriteLine("""
      
        1) True
        2) False

        """);
        Console.Write("Select the answer : ");
        answer = Console.ReadLine();
      } while ( (answer != "1") && (answer != "2") );
      question = new TrueFalseQuestion(text, (answer == "1"));
    }
    else if(type == typeof(MultipleChoiceQuestion).ToString() )
    {
      List<string> optionList = new List<string>();
      bool quit = false;
      do
      {
        Console.WriteLine($"There are already {optionList.Count()} options: {string.Join(" | ", optionList)}");
        Console.Write("Enter one answer option (max of 4) (press Enter to quit): ");
        string opt = Console.ReadLine();
        if(opt == ""){
          if(optionList.Count() >= 2)
          {
            Console.WriteLine("Quit");
            quit = true;
          }else{
            Console.WriteLine("Minimum of 2 answers options required");
          }
        }else if(optionList.Contains(opt)){
          Console.WriteLine("Option is already in List");
        }
        else{
          optionList.Add(opt);
          if(optionList.Count() >= 4){
            quit = true;
          }
        }
      } while ((!quit)) ;

      int answerIndex = -1;
      do
      {
        Console.WriteLine("");
        Console.WriteLine($"{text}");
        for (int i = 0; i < optionList.Count(); i++)
        {
          Console.WriteLine($"{i+1} -- {optionList[i]}");
        }
        Console.WriteLine("Choose the Correct Answer :");
        answerIndex = int.Parse(Console.ReadLine()) - 1;
      }while ((answerIndex<0)|| (answerIndex >= optionList.Count()) );
      question = new MultipleChoiceQuestion(text,optionList[answerIndex],optionList);
    }
  }

  private Question CreateQuestion(string type){
    Question question = null;
    Console.Write("Enter the question text : ");
    string text = Console.ReadLine();

    SetQuestionAnswer(type, text , ref question);

    return question;
  }

  
  private void GetQuizzesFromDatabase()
  {
    _quizzes = _database.GetUserQuizzes(_user);
  }

  public void PrintQuizList()
  {
    int choice = -1;
    do
    {
      Console.Clear();
      Console.WriteLine("Available Quizzes:");
      for (int i = 0; i < _quizzes.Count(); i++)
      {
        Console.WriteLine($"{i+1} -- {_quizzes[i].GetTitle()}");
      }

      Console.Write("Select One Quiz to manage (0 to quit) : ");
      choice = int.Parse(Console.ReadLine());

      if( ( choice > 0 ) && (choice <= (_quizzes.Count())) ){
        Quiz quiz = _quizzes[choice-1] ;
        manageQuiz(ref quiz);
      }
      if(choice > _quizzes.Count() ){
        Console.WriteLine(" There are not as many quizzes.");
      }
    }while(choice != 0);
  }

  public void PrintQuestionList(Quiz quiz)
  {
    List<Question> questions = _database.GetQuizQuestion(quiz);
    int choice = -1;
    do
    {
      Console.Clear();
      Console.WriteLine("Quiz: {0}", quiz.GetTitle());
      Console.WriteLine("Description: {0}\n", quiz.GetDescription());

      Console.WriteLine("Available Questions:");
      for (int i = 0; i < questions.Count(); i++)
      {
        Console.WriteLine($"{i+1}-- Q: {questions[i].GetText()} R:{questions[i].GetCorrectAnswer()} \n");
      }
      Console.Write("\nSelect One Question to manage (0 to quit) : ");
      choice = int.Parse(Console.ReadLine());

      if( ( choice > 0 ) && (choice <= (questions.Count())) ){
        Question question = questions[choice-1] ;
        manageQuestion(ref question, ref questions);
      }
      if(choice > questions.Count() ){
        Console.WriteLine(" There are not as many questions.");
      }
    }while(choice != 0);
  }

  private void manageQuiz(ref Quiz quiz)
  {
    int choice = -1;
    do{
      Console.Clear();
      Console.WriteLine("Quiz: {0}", quiz.GetTitle());
      Console.WriteLine("Description: {0}", quiz.GetDescription());
      Console.WriteLine("Duration: {0} minutes", quiz.GetDuration());
      Console.WriteLine("Pass mark: {0}%\n", quiz.GetPassmark());

      Console.WriteLine("""

        1) Add Questions
        2) View Questions
        3) Delete Quiz
        0) Exit

      """);

      Console.Write("Action On Quiz : ");
      choice = int.Parse(Console.ReadLine());
      switch (choice)
      {

        case 1:
          AddQuestionToQuiz(ref quiz);
          _database.AddQuestionToQuiz(quiz.GetId(),quiz.GetQuestions());
          break;
        case 2 :
          PrintQuestionList(quiz);
          break;
        case 3 :
          _database.DeleteQuiz(quiz);
          _quizzes.Remove(quiz);
          choice = 0;
          break;
        case 0:
          break;
        
        default:
          break;
      }


    }while(choice != 0);
  }

  private void manageQuestion(ref Question question, ref List<Question> questions)
  {
    Console.WriteLine("Manage Question");
    int choice = -1;
    do{
      Console.Clear();
      Console.WriteLine("Question: {0}", question.GetText());
      Console.WriteLine("Answer: {0}", question.GetCorrectAnswer());
      if(question.GetType() == typeof(MultipleChoiceQuestion)){
        Console.WriteLine("Options: {0}", (question as MultipleChoiceQuestion).GetStringOptions(" | "));
      }

      Console.WriteLine("""

        1) Change Answer
        2) Delete Question
        0) Exit

      """);

      Console.Write("Action On Question : ");
      choice = int.Parse(Console.ReadLine());
      switch (choice)
      {

        case 1:
          SetQuestionAnswer(question.GetType().ToString(), question.GetText(), ref question);
          _database.UpdateQuestion(question);
          break;
        case 2 :
          _database.DeleteQuestion(question);
          questions.Remove(question);
          choice = 0;
          break;
        case 0:
          break;
        default:
          break;
      }
    }while(choice != 0);
  }

}
