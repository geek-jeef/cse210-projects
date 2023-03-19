class ReflectionActivity : Activity
{
    private string[] _prompts = new string[] {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };


    private string[] _questions = new string[] {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
        
    public ReflectionActivity()
    {
        _activityName = "Reflection Activity";
        _activityDescription ="This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override void Start()
    {
        // Start activity
        base.Start();

        Random random = new Random();
        int promptIndex = random.Next(_prompts.Length);
        string prompt = _prompts[promptIndex];
            // Perform activity
            Console.WriteLine("Consider de following prompt \n");
            Console.WriteLine($"\t--- {prompt} ---");
            Console.WriteLine();
            Console.WriteLine("When you have something in mind press enter to continue.");
            Console.ReadKey();

            Console.WriteLine("Reflect about the following questions.");
            Console.Write("You may begin in: ");
            Animation(ANIMATION_COUNTDOWN,3);
            Console.Clear();

            DateTime currentTime = DateTime.Now;
            DateTime futureTime = DateTime.Now.AddSeconds(_duration);

            do{
                int questionIndex = random.Next(_questions.Length);
                string question = _questions[questionIndex];

                Console.Write($"> {question} ");
                Animation(ANIMATION_SPINNER, 5);
                Console.WriteLine();

                currentTime = DateTime.Now;
            }while(currentTime < futureTime);
        
        base.End();
    }
}
