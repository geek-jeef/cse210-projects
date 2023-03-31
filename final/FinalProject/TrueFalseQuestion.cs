public class TrueFalseQuestion : Question
{

  public TrueFalseQuestion(string text, bool correctAnswer) : base(text, correctAnswer.ToString())
  {
  }
  public TrueFalseQuestion(int questionId, int quizId,string text, bool correctAnswer) : base(questionId,quizId,text, correctAnswer.ToString())
  {
  }
  

  public bool CheckAnswer(bool answer)
  {
    return base.CheckAnswer(answer.ToString());
  }

  public override string AskAnswer(int count = 1 ){
    Console.WriteLine();
    Console.WriteLine($"Q{count}: {GetText()}");
    int choice = -1;
    do
    {
      Console.WriteLine("""

      1) True
      2) False
      
      """);
      Console.Write("Make your choice : ");
      choice = int.Parse(Console.ReadLine());
    } while ( (choice != 1) && (choice != 2)  );
    return (choice == 1).ToString();
  }
}