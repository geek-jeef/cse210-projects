public class MultipleChoiceQuestion : Question
{
  private List<string> _options;


  public MultipleChoiceQuestion(string text, string correctAnswer, List<string> options) : base(text, correctAnswer)
  {
    _options = options;
  }
  public MultipleChoiceQuestion(int questionId, int quizId,string text, string correctAnswer, List<string> options) : base(questionId,quizId,text, correctAnswer)
  {
    _options = options;
  }

  public MultipleChoiceQuestion(string text, string correctAnswer, string options, string separator = "\n") : base(text, correctAnswer)
  {
    _options = options.Split(separator).ToList();
  }
  public MultipleChoiceQuestion(int questionId, int quizId,string text, string correctAnswer, string options, string separator = "\n") : base(questionId,quizId,text, correctAnswer)
  {
    _options = options.Split(separator).ToList();
  }

  public List<string> GetOptions()
  {
    return _options;
  }

  public string GetStringOptions(string separator = "\n")
  {
    string choices = "";
    foreach (var choice in _options)
    {
      choices += Converter.StripAndEncode(choice)+separator;
    }
    return choices.Remove(choices.Length -1, 1);
  }

  public override bool CheckAnswer(string answer)
  {
    return (GetCorrectAnswer() == answer.Trim());
  }

  public bool CheckAnswer(int answerIndex)
  {
    int index = _options.IndexOf(GetCorrectAnswer());
    return (index == answerIndex);
  }

  public override string AskAnswer(int count = 1 ){
    Console.WriteLine();
    Console.WriteLine($"Q{count}: {GetText()}");
    List<string> alpha = new List<string>() {"A","B","C","D","E","F"}.Take(_options.Count()).ToList();
    for (int i = 0; (i < _options.Count()) && (i< alpha.Count()) ; i++)
    {
      Console.WriteLine($"{alpha[i]}) {_options[i]}");
    }
    string choice = "";
    do
    {
      if(choice != ""){
        Console.Write("Invalid entry , Make your choice again : ");
      }else{
        Console.Write("Make your choice : ");
      }
      choice = Console.ReadLine().ToUpper();
    } while (!alpha.Contains(choice));
    string answer = _options[alpha.IndexOf(choice)];
    return answer ;
  }

}
