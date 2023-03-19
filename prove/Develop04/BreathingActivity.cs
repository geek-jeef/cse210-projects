class BreathingActivity : Activity
{
        public BreathingActivity() 
        {
            _activityName = "Breathing Activity";
            _activityDescription = "This activity will help you focus on your breathing and relax";
        }
        public override void Start()
        {
            // Start activity
            base.Start();
            // Perform activity

            DateTime currentTime = DateTime.Now;
            DateTime futureTime = DateTime.Now.AddSeconds(_duration);
            do{
                Console.WriteLine();
                Console.Write("Breathe in...");
                Animation(ANIMATION_COUNTDOWN, 4);
                Console.WriteLine();
                Console.Write("Breathe out...");
                Animation(ANIMATION_COUNTDOWN, 6);
                Console.WriteLine();
                currentTime = DateTime.Now;
            }while(currentTime < futureTime);

            base.End();
        }
}