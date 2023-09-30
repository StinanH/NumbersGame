//Stina Hedman
//NET23
using System;

namespace NumbersGame
{
    internal class Program
    {
        //method for setting difficulty by setting the highest number in the range.
        private static int SetDifficulty(int chosendifficulty)
        {
            int highestNr = 0;

            switch (chosendifficulty)
            {
                case 1:
                    highestNr = 10;
                    break;
                case 2:
                    highestNr = 15;
                    break;
                case 3:
                    highestNr = 20;
                    break;
            }

            return highestNr;
        }

        //function for creating a random number.
        private static int GenerateRandomNumber(int highestPossible)
        {
            Random rnd = new Random();
            int randomNr = rnd.Next(1, highestPossible);
            return randomNr;
        }
        //function for checking if the player wants to play again.
        private static bool CheckRetry()
        {
            bool checkingInput = true;
            bool wantToRetry = false;
            string input;

            //while loop running to check for valid input from user.
            while (checkingInput)
            {
                Console.WriteLine("\nVill du spela igen? Ja/Nej");
                input = Console.ReadLine();

                if (String.Equals(input.ToUpper(),"JA"))
                {
                    wantToRetry = true;
                    checkingInput = false;
                }
                else if (String.Equals(input.ToUpper(), "NEJ"))
                {
                    wantToRetry = false;
                    checkingInput = false;
                }
                else
                {
                    Console.WriteLine("Ange ja eller nej.");
                }

            }
            return wantToRetry;
        }

        //function for checking if used guessed right.
        private static bool CheckGuess(int guessedNr, int rightAnswer, string[] high, string[] low, string[] close)
        {   
            Random rand = new Random();
            bool guessedRight;

            if (guessedNr >= (rightAnswer-2) && (guessedNr <= rightAnswer+2) && guessedNr != rightAnswer)
            {
                Console.WriteLine(close[rand.Next(0, (close.Length))]);
            }
            if (guessedNr < rightAnswer)
            {
                Console.WriteLine(low[rand.Next(0,(low.Length))]);
                guessedRight = false;
            }
            else if (guessedNr > rightAnswer)
            {
                Console.WriteLine(high[rand.Next(0,(high.Length))]);
                guessedRight = false;
            }
            else
            {
                Console.WriteLine("Wohoo! Du klarade det!");
                guessedRight = true;
            }
            return guessedRight;
        }
        static void Main(string[] args)
        {
            bool playing = true;
            bool isGuessing = true;
            bool guessedright;
            string[] tooHigh = { "för högt!", "Tyvärr! inte lågt nog!", "Gissa lägre!" };
            string[] tooLow = { "för lågt..", "Ta i lite, högre!", "Tänk större! för lågt"};
            string[] close = { "nära!", "oooo! nästan!", "inte långt ifrån!" };
            string input;
            int guessedNr;
            int difficulty;
            int highestNumber;
            int nrOfGuesses = 0;

            Console.Write("Välkommen! ");
            Console.WriteLine("\nJag tänker på ett nummer. Kan du gissa vilket? Du får fem försök.");

            //runs for as long as player wants to play the game.
            while (playing)
            {   
                Console.WriteLine("\nVälj svårighetsgrad:\n[1] Lätt 1-10\n[2] Medel 1-15\n[3] Svårt 1-20\n");

                input = Console.ReadLine();
                while(!int.TryParse(input, out difficulty) || difficulty < 1 || difficulty > 3)
                {
                    Console.WriteLine("Ange \"1\", \"2\" eller \"3\"");
                    input =  Console.ReadLine(); 
                }
                highestNumber = SetDifficulty(difficulty);
                
                int rightAnswer = GenerateRandomNumber(highestNumber);
                isGuessing = true;

                Console.WriteLine($"Ok, jag tänker på ett tal mellan 1-{highestNumber}, gissa vilket:");

                //runs while player is guessing the answer
                while (isGuessing)
                {
                    input = Console.ReadLine();

                    //checks that userinput is an int, else asks for new userinput.
                    while(!int.TryParse(input,out guessedNr))
                    {
                        Console.WriteLine("Du angav inte ett tal, prova igen");
                        input = Console.ReadLine();
                    }

                    //checks if the user guessed right using CheckGuess
                    guessedright = CheckGuess(guessedNr, rightAnswer, tooHigh, tooLow, close);

                    //if they guessed right, check if they wanna play again and leave while-loop for guessing.
                    if (guessedright)
                    {
                        Console.WriteLine($"Du gissade {nrOfGuesses} gånger.");
                        nrOfGuesses = 0;
                        playing = CheckRetry();
                        isGuessing = false;
                    }
                    //else the user guessed wrong, add to count and let them guess again.
                    else
                    {
                         nrOfGuesses++;
                    }

                    //if the user has guessed 5 times, end game, present right answer and check if they wanna play again.
                    if (nrOfGuesses == 5)
                    {
                        Console.WriteLine("Tyvärr, du lyckades inte gissa talet på fem försök!");
                        Console.WriteLine($"Det rätta svaret var : {rightAnswer}");
                        nrOfGuesses = 0;
                        playing = CheckRetry();
                        isGuessing = false;
                    }
                }
            }
            Console.WriteLine("Tack för att du spela.");
        }
    }
}