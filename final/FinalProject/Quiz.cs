public class Quiz
{

  private int _quizId;

  private int _authorId;

  private string _title;
  private string _description;
  private List<Question> _questions;
  private int _duration;
  private int _passMark;

  public Quiz(string title, string description, List<Question> questions)
  {
    _title = title;
    _description = description;
    _questions = questions;
    _duration = 0;
    _passMark = 0;
    _quizId = 0;
    _authorId = 0;
    
  }

  public Quiz(string title, string description, int duration , int passMark)
  {
    _title = title;
    _description = description;
    _duration = duration;
    _passMark = passMark;
    _quizId = 0;
    _authorId = 0;

    _questions = new List<Question>();

  }

  public Quiz(int quizId, string title, string description, int duration , int passMark , int authorId)
  {
    _title = title;
    _description = description;
    _duration = duration;
    _passMark = passMark;
    _quizId = quizId;
    _authorId = authorId;
    _questions = new List<Question>();

  }

  public string GetTitle()
  {
    return _title;
  }

  public int GetAuthorId()
  {
    return _authorId;
  }

  public string GetDescription()
  {
    return _description;
  }

  public int GetDuration()
  {
    return _duration;
  }

  public int GetPassmark()
  {
    return _passMark;
  }

  public int GetId()
  {
    return _quizId;
  }

  public List<Question> GetQuestions()
  {

    return _questions;
  }

  public void AddQuestion( Question question)
  {
    _questions.Add(question);

  }

  public void AddQuestions( List<Question> questions)
  {
    _questions.AddRange(questions);
  }

  public void SetDuration(int duration)
  {
    _duration = duration;
  }

  public void SetPassMark(int passMark)
  {
    _passMark = passMark;
  }

}
