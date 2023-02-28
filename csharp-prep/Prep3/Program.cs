using System;

internal class NewBaseType
{
    static void Main(string[] args)
    {
        // Console.Write("What is the magic number? ");
        // int magicNumber = int.Parse(Console.ReadLine());
        string playGame = "yes";

        do
        {
            Random randomNumberGenerator = new Random();
            int magicNumber = randomNumberGenerator.Next(1, 101);

            int guess = -1;
            int attempts = 1;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                    attempts++;
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                    attempts++;
                }
                else
                {
                    Console.WriteLine($"You guessed it! after {attempts} attempts");
                    Console.Write("Do you want to continue (yes/no) ? ");
                    playGame = Console.ReadLine();
                }
            }

        }while (playGame == "yes");

        Console.WriteLine("Good Bye");

    }
}