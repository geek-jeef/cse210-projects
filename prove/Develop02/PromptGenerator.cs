using System;

public class PromptGenerator
{
    private List<string> _prompts = new List<string>(){
        "Who was the most interesting person I interacted with today?" ,
        "What was the best part of my day?" ,
        "How did I see the hand of the Lord in my life today?" ,
        "What was the strongest emotion I felt today?" ,
        "If I had one thing I could do over today, what would it be?" ,
        "What's something you learned today that you didn't know before?" ,
        "What's one thing you did today that made you proud of yourself?" ,
        "Did you encounter any challenges today, and if so, how did you handle them?" ,
        "Did you make any progress towards achieving a personal or professional goal today? If so, what did you do?" ,
        "Was there a moment today when you felt particularly grateful or blessed? If so, what happened?" ,
        "Did you have any meaningful conversations or connections with others today? If so, who did you talk to and what did you discuss?" ,
        "Did you take any steps today towards taking care of your physical or mental health? If so, what did you do?" ,
        "Was there a moment today when you felt particularly happy or joyful? If so, what brought you that feeling?" ,
        "Did you make any mistakes or experience any setbacks today? If so, how did you handle them, and what did you learn from them?" ,
        "Did you take any opportunities today to help someone else or make a positive impact?" ,
    };

    public string Prompt()
    {
        Random randomNumberGenerator = new Random();
        int index = randomNumberGenerator.Next(0, _prompts.Count);
        return _prompts[index];
    }
}