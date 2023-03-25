public class CheckListGoal : Goal
{
  private int _achievementCount;
  private int _achievementGoal;
  private int _bonus;

  public CheckListGoal() : base()
  {
    Console.Write("How many time this goal need to be accomplished for a bonus? ");
    int achievementGoal = int.Parse(Console.ReadLine());
    Console.Write("What is the bonus for accomplishing it that many times? ");
    int bonus = int.Parse(Console.ReadLine());

    _bonus = bonus;
    _achievementCount = 0;
    _achievementGoal = achievementGoal;
  }

  public CheckListGoal(string name, string description, int points, int achievementGoal, int bonus) : base(name, description, points)
  {
    _bonus = bonus;
    _achievementCount = 0;
    _achievementGoal = achievementGoal;
  }

  public CheckListGoal(string name, string description, int points, int achievementCount, int achievementGoal, int bonus) : base(name, description, points)
  {
    _bonus = bonus;
    _achievementCount = achievementCount;
    _achievementGoal = achievementGoal;
  }

  public int GetAchievementCount()
  {
    return _achievementCount;
  }
  public void SetAchievementCount(int achievementCount)
  {
    _achievementCount = achievementCount;
  }

  public int GetAchievementGoal()
  {
    return _achievementGoal;
  }
  public void SetAchievementGoal(int achievementGoal)
  {
    _achievementGoal = achievementGoal;
  }

  public int GetBonus()
  {
    return _bonus;
  }
  public void SetBonus(int bonus)
  {
    _bonus = bonus;
  }

  public void IncrementAchievementCount()
  {
    _achievementCount += 1;
  }

  public override int completionEvent(){
    if(GetAchievementCount() < GetAchievementGoal()){
      IncrementAchievementCount();
      SetAchievement(GetAchievementCount() == GetAchievementGoal());
      return GetPoints();
    }
    return 0;
  }



  public override string Display()
  {
    return $"{base.Display()} --- Currently Completed: {_achievementCount}/{_achievementGoal}";
  }
  public override string Stringify()
  {
    return $"{GetType()}|{GetName()}|{GetDescription()}|{GetPoints()}|{GetAchievementCount()}|{GetAchievementGoal()}|{GetBonus()}";
  }

}