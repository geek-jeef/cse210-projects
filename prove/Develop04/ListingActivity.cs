class ListingActivity : Activity
{
    private string[] _prompts = new string[] {
        "Who are people that you appreciate?.",
        "What are personal strengths of yours?.",
        "Who are people that you have helped this week?.",
        "When have you felt the Holy Ghost this month?.",
        "Who are some of your personal heroes?."
    };
    public ListingActivity()
    {
        _activityName = "Listing Activity";
        _activityDescription = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void Start()
    {
        // Start activity
        base.Start();
        // Get prompt
            Random random = new Random();

            int index = random.Next(_prompts.Length);
            string prompt = _prompts[index];

            List<string> answers = new List<string>();

            Console.WriteLine("list as many responses you can to the following prompt");
            Console.WriteLine($"\t---{prompt}---");
            Console.Write("You may begin in: ");
            Animation(ANIMATION_COUNTDOWN,3);
            Console.WriteLine();

            DateTime currentTime = DateTime.Now;
            DateTime futureTime = DateTime.Now.AddSeconds(_duration);
            do{
                Console.Write("> ");
                answers.Add(Console.ReadLine());
                Console.WriteLine();
                currentTime = DateTime.Now;
            }while(currentTime < futureTime);

            Console.WriteLine($"You listed {answers.Count()} items!");



        
        base.End();
    }
}