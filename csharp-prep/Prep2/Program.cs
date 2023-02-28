using System;

class Program
{
    static string signDetermination(int percent){
        if( (percent >= 97) || (percent < 60 ) )
        {
            return "";
        }
        else if(percent%10 >= 7)
        {
            return "+";
        }
        else if (percent%10 < 3)
        {
            return "-";
        }

        return "";
    }
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        int percent = int.Parse(input);

        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        string sign = signDetermination(percent);
        //letter += sign ;

        Console.WriteLine($"Your grade is: {letter}{sign}");
        
        if (percent >= 70)
        {
            Console.WriteLine("Congratulations! You passed.");
        }
        else
        {
            Console.WriteLine("Sorry! You fail. Maybe next time...");
        }
    }
}