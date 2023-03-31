using System.Text.Json;

public class QuizTaker
{
  Student _user;
  private List<Quiz> _quizzes;

  private Database _database;


  public QuizTaker(Student user)
  {
    _user = user;
    _database = new Database(_user);
    _quizzes = _database.GetQuizzes();
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

      Console.Write("Select One Quiz to take (0 to quit) : ");
      choice = int.Parse(Console.ReadLine());

      if( ( choice > 0 ) && (choice <= (_quizzes.Count())) ){
        Quiz quiz = _quizzes[choice-1] ;
        TakeQuiz(ref quiz);
      }
      if(choice > _quizzes.Count() ){
        Console.WriteLine(" There are not as many quizzes.");
      }
    }while(choice != 0);
  }

  public void TakeQuiz(ref Quiz quiz){

    List<Question> questions = _database.GetQuizQuestion(quiz);
    List<Dictionary<string, string>> answers  = new List<Dictionary<string, string>>() ;
    int count =  0;
    int questionCount = questions.Count();
    int correctAnswerCount = 0;
    Time timer = new Time();
    timer.SetCountdown(quiz.GetDuration());
    timer.Start();
    foreach (var question in questions)
    {
      Dictionary<string, string> answer = new Dictionary<string, string>();
      count++;
      Console.Clear();
      Console.WriteLine($"Question {count} of {questionCount} : \n");
      answer["questionId"] = question.GetId().ToString();
      answer["questionText"] = question.GetText();
      answer["answer"] = question.AskAnswer(count);
      answer["validity"] = question.CheckAnswer(answer["answer"]).ToString();
      if(answer["validity"] == true.ToString()){
        correctAnswerCount++;
      }   
      answer["correctAnswer"] = question.GetCorrectAnswer();
      answers.Add(answer);

      if(timer.CheckCountdown())
      {
        Console.WriteLine($"Time is up ... \n");
        break;
      }
    }
    timer.Stop();
    Console.Clear();
    Console.WriteLine($"Congratulation ! you have completed this quiz");

    Dictionary<string, string> quizData = new Dictionary<string, string>();
    quizData["title"] = quiz.GetTitle();
    quizData["passMark"] = quiz.GetPassmark().ToString();
    quizData["duration"] = quiz.GetDuration().ToString();
    quizData["authorId"] = quiz.GetAuthorId().ToString();
    quizData["attemptDuration"] = timer.GetElapsed().ToString();

    int time = timer.GetMinutesElapsed();
    QuizResult quizResult = new QuizResult(_user.GetId(),quiz.GetId(), correctAnswerCount, questionCount, time ,quizData,answers);
    Console.WriteLine(quizResult.ToString());
    _database.AddQuizResult(quizResult);
    Console.WriteLine($"Result saved in database. Press any key to exit ");
    Console.ReadKey();
  }


  public void PrintGradeList(){
    List<QuizResult> results = _database.GetStudentResults(_user);
    
    int choice = -1;
    do
    {
      Console.Clear();
      Console.WriteLine("Available Quiz Results:");
      for (int i = 0; i < results.Count(); i++)
      {
        Console.WriteLine($"{i+1} -- Quiz : {results[i].GetQuizData()["title"]} | Submission n: {results[i].GetId()}");
        Console.WriteLine($"\t {results[i].ToString()} \n");
      }

      Console.Write("\nSelect One Result to manage (0 to quit) : ");
      choice = int.Parse(Console.ReadLine());
      if( ( choice > 0 ) && (choice <= (results.Count())) ){
        QuizResult result = results[choice-1] ;
        manageQuizResult(ref result, ref results);
      }
      if(choice > results.Count() ){
        Console.WriteLine(" There are not as many quiz results.");
      }
    }while(choice != 0);
  }

  public void manageQuizResult(ref QuizResult result, ref List<QuizResult> results)
  {
    Console.WriteLine("Manage Quiz Result");
    int choice = -1;
    Dictionary<string, string> quizInfo = result.GetQuizData();
    do{
      Console.Clear();
      Console.WriteLine($"Quiz : {quizInfo["title"]} | Submission n: {result.GetId()}");
      Console.WriteLine($"\t{result.ToString()}\n");
      Console.WriteLine("Attempt Duration: {0:hh\\:mm\\:ss} {1}", TimeSpan.Parse(quizInfo["attemptDuration"]) , (quizInfo["duration"] != 0.ToString())?$"( max : {quizInfo["duration"]} minutes )":"");
      Console.WriteLine("Score: {0}% ({1}% required to pass)\n", result.GetScore() , quizInfo["passMark"]  );

      Console.WriteLine("""

        1) Show Details
        2) Delete Result
        0) Exit

      """);

      Console.Write("Action on Quiz Result : ");
      choice = int.Parse(Console.ReadLine());
      switch (choice)
      {

        case 1:
          result.showDetails();
          Console.ReadKey();
          break;
        case 2 :
          _database.DeleteQuizResult(result);
          results.Remove(result);
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