using System.Text.Json;
public class QuizResult
{
  private int _resultId;
  private int _studentId;
  private int _quizId;
  private int _correctAnswers ;
  private int _totalQuestions ;
  private double _score ;
  private int _time ;
  Dictionary<string, string> _quizData ;
  List<Dictionary<string, string>> _resultData ;


  public QuizResult(int studentId, int quizId,int correctAnswers, int totalQuestions, int time ,Dictionary<string, string> quizData ,List<Dictionary<string, string>> resultData )
  {
    _resultId = 0;
    _studentId = studentId;
    _quizId = quizId;
    _correctAnswers = correctAnswers;
    _totalQuestions = totalQuestions;
    _score = (double)correctAnswers / totalQuestions * 100;
    _time = time;
    _resultData  = resultData;
    _quizData  = quizData;
  }
  public QuizResult(int resultId ,int studentId, int quizId,int correctAnswers, int totalQuestions, int time , string quizData , string resultData )
  {
    _resultId = resultId;
    _studentId = studentId;
    _quizId = quizId;
    _correctAnswers = correctAnswers;
    _totalQuestions = totalQuestions;
    _score = (double)correctAnswers / totalQuestions * 100;
    _time = time;
    _quizData  = JsonSerializer.Deserialize<Dictionary<string, string>>(quizData) ;
    _resultData  = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(resultData) ;
  }

  public int GetId(){
    return _resultId;
  }

  public int GetStudentId(){
    return _studentId;
  }

  public int GetQuizId(){
    return _quizId;
  }

  public int GetCorrectAnswers(){
    return _correctAnswers;
  }

  public int GetTotalQuestions(){
    return _totalQuestions;
  }

  public double GetScore(){
    return _score;
  }

  public int GetTime(){
    return _time;
  }

  public Dictionary<string, string> GetQuizData(){
    return _quizData;
  }
  public List<Dictionary<string, string>> GetResultData(){
    return _resultData;
  }

  public string GetQuizDataToString(){
    return JsonSerializer.Serialize(_quizData);
  }
  public string GetResultDataToString(){
    return JsonSerializer.Serialize(_resultData);
  }

  public override string ToString()
  {
    return $"You answered {_correctAnswers} out of {_totalQuestions} questions correctly ({_score}%).";
  }

  public void showDetails(){
    int count = 1;
    int resultDataCount = _resultData.Count();
    foreach (var item in _resultData)
    {
      Console.WriteLine($"Question {count} of {resultDataCount}: {item["questionText"]}");
      string valid = (item["validity"] == true.ToString())?("Correct"):("Incorrect");
      Console.WriteLine($"\tYour answer: {item["answer"]} - {valid} \n");
      count++;
    }
    Console.WriteLine($"{this.ToString()} \n");
  }
}