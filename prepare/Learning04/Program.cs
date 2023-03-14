using System;

class Program
{
    static void Main(string[] args)
    {
        //Create a simple assignment, call the method to get the summary, and then display it to the screen.
        Assignment first = new Assignment("Guy-Jeef TUMBA", "Poincaré conjecture");
        Console.WriteLine(first.GetSummary());
        Console.WriteLine();


        //creating a new MathAssignment object and set its values.
        //testing both the GetSummary() and the GetHomeworkList() methods
        MathAssignment second = new MathAssignment("Guy-Jeef TUMBA", "Riemann hypothesis", "7.5", "42");
        Console.WriteLine(second.GetSummary());
        Console.WriteLine(second.GetHomeworkList());
        Console.WriteLine();

        // test of 'WritingAssignment' class
        WritingAssignment third = new WritingAssignment("Guy-Jeef TUMBA", "European History", "The Causes of World War II");
        Console.WriteLine(third.GetSummary());
        Console.WriteLine(third.GetWritingInformation());
        Console.WriteLine();

    }

}