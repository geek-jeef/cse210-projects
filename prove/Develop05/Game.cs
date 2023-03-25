using System.IO; 
public class Game
{
  List<Goal> _goals = new List<Goal>();

  private int _points;

  public Game()
  {
    _points = 0;
  }

  public int getPoints()
  {
    return _points;
  }

  public void AddPoints(int points)
  {
    _points += points;
  }

  public void Create()
  {
    Console.WriteLine("""

    The type of goals are:
      1)Simple Goal
      2)Eternal Goal
      3)CheckList Goal

    """);
    Console.Write("Which type of goal would you want to create? ");
    int goalType = int.Parse(Console.ReadLine());
    switch (goalType)
    {
      case 1:
        Goal simple = new Goal();
        AddGoal(simple);
        break;
      case 2:
        EternalGoal eternal = new EternalGoal();
        AddGoal(eternal);
        break;
      case 3:
        CheckListGoal check = new CheckListGoal();
        AddGoal(check);
        break;
      default:
        Console.WriteLine("Please Select a Correct Goal type");
        break;
    }
  }

  private void AddGoal(Goal goal)
  {
    _goals.Add(goal);
    Console.WriteLine("Goal created!");
  }

  public void List()
  {
    Console.WriteLine("The goals are:");
    for (int i = 0; i < _goals.Count(); i++)
    {
      Console.WriteLine($"{i + 1}. {_goals[i].Display()}");
    }
  }

  public void Progress()
  {
    List();
    Console.WriteLine("");
    Console.Write("Which goal have you completed?: ");
    int goalIndex = int.Parse(Console.ReadLine()) -1;
    AddPoints(_goals[goalIndex].completionEvent());
    Console.WriteLine("Goal updated!");
  }

  public void Save()
  {
    Console.Write("Type file name (with .txt extension): ");
    string filename = $"data\\{Console.ReadLine()}";
    using (StreamWriter outputFile = new StreamWriter(filename))
    {
        outputFile.WriteLine($"{_points}");
        for (int i = 0; i < _goals.Count(); i++)
        {
          outputFile.WriteLine($"{_goals[i].Stringify()}");
        }
        
    }
    Console.WriteLine("Goals Saved");
  }

  public void Load()
  {
    try
    {
      string[] dirs = Directory.GetFiles(@"data\", "*.txt");
      for (int i = 0; i < dirs.Length; i++)
      {
        Console.WriteLine($"{i + 1}. {dirs[i]}");
      }
      Console.Write("Choose the file to load:");
      int fileIndex = int.Parse(Console.ReadLine());
      if(dirs[fileIndex-1] != null){
        string filename = dirs[fileIndex-1] ;
        string[] lines = File.ReadAllLines(filename);
        string first = lines.First();
        lines = lines.Except(new string[] { first }).ToArray();
        _goals.Clear();
        _points = int.Parse(first);
        foreach (string line in lines)
        {
          string[] parts = line.Split("|");
          if(parts[0] == "Goal"){
            _goals.Add(new Goal(parts[1],parts[2],int.Parse(parts[3]), bool.Parse(parts[4])) );
          }else if(parts[0] == "EternalGoal"){
            _goals.Add(new EternalGoal(parts[1],parts[2],int.Parse(parts[3])));
          }else if(parts[0] == "CheckListGoal"){
            _goals.Add(new CheckListGoal(parts[1],parts[2],int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]) , int.Parse(parts[6])   ));
          }
        }
      }
    }
    catch (Exception e)
    {
        Console.WriteLine("The process failed: {0}", e.ToString());
    }
    Console.WriteLine("Goals Loaded");
  }

}