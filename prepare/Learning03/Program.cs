using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction first = new Fraction();
        Console.WriteLine(first.GetFractionString());
        Console.WriteLine(first.GetDecimalValue());
        first.SetTop(22);
        first.SetBottom(7);
        Console.WriteLine(first.GetTop());
        Console.WriteLine(first.GetBottom());
        Console.WriteLine(first.GetFractionString());
        Console.WriteLine(first.GetDecimalValue());

        Fraction second = new Fraction(3);
        Console.WriteLine(second.GetTop());
        Console.WriteLine(second.GetBottom());
        Console.WriteLine(second.GetFractionString());
        Console.WriteLine(second.GetDecimalValue());

        Fraction third = new Fraction(1, 4);
        Console.WriteLine(third.GetTop());
        Console.WriteLine(third.GetBottom());
        Console.WriteLine(third.GetFractionString());
        Console.WriteLine(third.GetDecimalValue());

    }
}