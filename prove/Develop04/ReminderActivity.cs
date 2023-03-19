class ReminderActivity : Activity
    {
        public ReminderActivity()
        {
            _activityName = "Reminder Activity";
            _activityDescription = "This activity will help you remember to do something.";
        }

        public override void Start()
        {
            base.Start();

            // Perform activity
            Console.WriteLine("Type what would you like to remember to do?");
            string reminder = Console.ReadLine();
            Console.WriteLine("You will be reminded to " + reminder + " in " + _duration + " seconds.");
            System.Threading.Thread.Sleep(_duration * 1000);
            Console.WriteLine();
            Console.WriteLine("Remember to " + reminder + "!\n");
            base.End();
        }
    }