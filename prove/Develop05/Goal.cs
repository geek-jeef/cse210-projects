public class Goal
{
  protected string _name;
  protected string _description;
  protected int _points;
  protected bool _achievement;


  public Goal(){
    Console.Write("What is the name of your goal? ");
    string name = Console.ReadLine();
    Console.Write("What is the short description of it? ");
    string description = Console.ReadLine();
    Console.Write("What is the amount of points associated with this goal? ");
    int points = int.Parse(Console.ReadLine());
    _name = name;
    _description = description;
    _points = points;
    _achievement = false;
  }

  public Goal(string name, string description, int points)
  {
    _name = name;
    _description = description;
    _points = points;
    _achievement = false;
  }

  public Goal(string name, string description, int points, bool achievement)
  {
    _name = name;
    _description = description;
    _points = points;
    _achievement = achievement;
  }

  public string GetName()
  {
    return _name;
  }
  public void SetName(string name)
  {
    _name = name;
  }
  public string GetDescription()
  {
    return _description;
  }
  public void SetDescription(string description)
  {
    _description = description;
  }
  public int GetPoints()
  {
    return _points;
  }
  public void SetPoints(int points)
  {
    _points = points;
  }
  public virtual bool GetAchievement()
  {
    return _achievement;
  }
  public virtual void SetAchievement(bool achievement)
  {
    _achievement = achievement;
  }

  public virtual int completionEvent(){
    SetAchievement(true);
    return GetPoints();
  }

  public virtual string Display(){
    return $"[{(GetAchievement()?'X':' ')}] {GetName()} ({GetDescription()})" ;
  }
  public virtual string Stringify(){
    return $"{GetType()}|{GetName()}|{GetDescription()}|{GetPoints()}|{GetAchievement()}" ;
  }

  



}