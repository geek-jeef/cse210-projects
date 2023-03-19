class Activity
{
  protected const string ANIMATION_SPINNER = "spinner";
  protected const string ANIMATION_COUNTDOWN = "countdown";
  protected const string ANIMATION_PAUSE = "pause";


  protected string _activityName;
  protected string _activityDescription;
  public int _duration;

  public Activity(){
    _activityName = "Activity";
    _activityDescription = "This activity can help you to take more time to be mindful";
  }

  public Activity(string activityName, string activityDescription){
    _activityName = activityName;
    _activityDescription = activityDescription;
  }
  
  public virtual void Start()
  {
    Console.Clear();
    Console.WriteLine($"Welcome to the {_activityName}");
    Console.WriteLine();
    Console.WriteLine(_activityDescription);
    Console.WriteLine();
    // Get duration from user
    Console.Write("How long would you like to do this activity for? (in seconds) : ");
    _duration = int.Parse(Console.ReadLine());
    // Start activity
    Console.Clear();
    Console.WriteLine("Get Ready...");
    Animation(ANIMATION_SPINNER);
  }

  public virtual void End(){
    Console.WriteLine();
    Console.WriteLine("Well Done !!");
    Animation(ANIMATION_SPINNER);

    Console.WriteLine($"You have completed another {_duration} seconds of the {_activityName} ");
    Animation(ANIMATION_SPINNER);

  }




  protected void Animation(string type, int time = 3)
  {

    if (type == ANIMATION_PAUSE)
    {
      Console.Write("Pause");
      for (int i = 0; i < time; i++)
      {
        Console.Write(".");
        System.Threading.Thread.Sleep(1000);
      }
      Console.WriteLine();
    }
    else if (type == ANIMATION_COUNTDOWN)
    {
      //Console.Write("Starting in ");
      for (int i = time; i > 0; i--)
      {
        Console.Write(i);
        Thread.Sleep(1000);
        Console.Write("\b \b");
      }
      //Console.WriteLine();
    }
    else if(type == ANIMATION_SPINNER )
    {
      DateTime futureTime = DateTime.Now.AddSeconds(time);
      DateTime currentTime = DateTime.Now;

      string spin = "|/-\\";

      do{

        foreach (char item in spin)
        {
          Console.Write($"{item}");
          Thread.Sleep(400);
          Console.Write("\b \b");
        }

        currentTime = DateTime.Now;
      }while(currentTime < futureTime);
      
      Console.WriteLine();
    }

  }

}