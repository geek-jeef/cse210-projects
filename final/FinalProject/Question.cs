public class Question
{
  private string _text;
  private string _correctAnswer;
  private int _questionId;
  private int _quizId;

  public Question(string text, string correctAnswer)
  {
    _text = text;
    _correctAnswer = correctAnswer.Trim();
    _questionId = 0;
    _quizId = 0;
  }

  public Question(int questionId, int quizId, string text, string correctAnswer)
  {
    _text = text;
    _correctAnswer = correctAnswer.Trim();
    _questionId = questionId;
    _quizId = quizId;
  }

  public string GetText()
  {
    return _text;
  }

  public virtual string GetCorrectAnswer()
  {
    return _correctAnswer;
  }

  public virtual bool CheckAnswer(string answer)
  {
    return (GetCorrectAnswer().ToLower() == answer.Trim().ToLower());
  }

  public int GetId()
  {
    return _questionId;
  }

  public int GetQuizId()
  {
    return _quizId;
  }

  public virtual string AskAnswer(int count = 1 ){
    Console.WriteLine();
    Console.WriteLine($"Q{count}: {GetText()}");
    string answer = Console.ReadLine();
    return answer;
  }

}
