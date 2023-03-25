public class EternalGoal : Goal
{

  public EternalGoal() : base()
  {

  }

  public EternalGoal(string name, string description, int points) : base(name, description, points)
  {

  }

  public override bool GetAchievement()
  {
    return false;
  }
  public override void SetAchievement(bool achievement)
  {
    _achievement = false;
  }


  public override int completionEvent(){
    SetAchievement(false);
    return GetPoints();
  }


}