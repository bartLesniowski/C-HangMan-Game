using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangMan
{
    class Program
    {
        static void DrawMan(int lives)
        {
            if (lives == 6)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("      |  |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 5)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("      |  |  ");
                Console.WriteLine("      O  |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 4)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("      |  |  ");
                Console.WriteLine("      O  |  ");
                Console.WriteLine("      |  |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 3)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("     |   |  ");
                Console.WriteLine("     O   |  ");
                Console.WriteLine("    /|   |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 2)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("     |   |  ");
                Console.WriteLine("     O   |  ");
                Console.WriteLine("    /|\\  |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 1)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("     |   |  ");
                Console.WriteLine("     O   |  ");
                Console.WriteLine("    /|\\  |  ");
                Console.WriteLine("    /    |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else if (lives == 0)
            {
                Console.WriteLine("  ________  ");
                Console.WriteLine("     |   |  ");
                Console.WriteLine("     O   |  ");
                Console.WriteLine("    /|\\  |  ");
                Console.WriteLine("    / \\  |  ");
                Console.WriteLine("         |  ");
                Console.WriteLine("  _______|  ");

            }
            else
            {
                Console.WriteLine("Aplication error!");
            }
        }
            static void Main(string[] args) 
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string[] wordBank = System.IO.File.ReadAllLines(@"C:\HangmanGame\countries_and_capitals.txt");
            string toGuess = wordBank[random.Next(0, wordBank.Length)];
            string wordToGuess = toGuess.Substring(toGuess.IndexOf("| ")+2);
            string hint = toGuess.Substring(0, toGuess.IndexOf(" | "));
            string wordToGuessUppercase = wordToGuess.ToUpper();

            StringBuilder displayToPlayer = new StringBuilder(wordToGuess.Length);
            for (int i = 0; i < wordToGuess.Length; i++)
                displayToPlayer.Append('_');

            List<char> correctGuesses = new List<char>();
            List<char> incorrectGuesses = new List<char>();
            

            int lives = 6;
            DrawMan(6);
            bool won = false;
            int lettersRevealed = 0;

            string input;
            char guess;
            string playAgain;


            {
                while (!won)
                {

                    while (lives > 0)
                    {
                        Console.Write("Guess a letter: ");
                        input = Console.ReadLine().ToUpper();
                        guess = input[0];
                        string items = string.Join(", ", incorrectGuesses);

                        if (correctGuesses.Contains(guess))
                        {
                            Console.WriteLine("You've already tried '{0}', and it was correct!", guess);
                            Console.WriteLine("Lives left: '{0}'", lives);
                            Console.WriteLine("Not in word letters: \n" + items);
                            continue;
                        }
                        else if (incorrectGuesses.Contains(guess))
                        {
                            Console.WriteLine("You've already tried '{0}', and it was wrong!", guess);
                            Console.WriteLine("Lives left: '{0}'", lives);
                            Console.WriteLine("Not in word letters: \n" + items);
                            continue;
                        }

                        if (wordToGuessUppercase.Contains(guess))
                        {
                            correctGuesses.Add(guess);

                            for (int i = 0; i < wordToGuess.Length; i++)
                            {
                                if (wordToGuessUppercase[i] == guess)
                                {
                                    displayToPlayer[i] = wordToGuess[i];
                                    lettersRevealed++;
                                }
                            }

                            if (lettersRevealed == wordToGuess.Length)
                                won = true;
                        }
                        else
                        {
                            incorrectGuesses.Add(guess);

                            Console.WriteLine("Nope, there's no '{0}' in it!", guess);
                            Console.WriteLine("Lives left: '{0}'", lives);
                            Console.WriteLine("Not in word letters: \n" + items);
                            lives--;
                            DrawMan(lives);

                        }
                        while (lives == 1)
                        {
                            Console.WriteLine("1 life left! Hint: This city is capital of {0}.", hint);
                            break;
                        }
                        Console.WriteLine(displayToPlayer.ToString());
                    }
                
                    if (won)
                    {
                        Console.WriteLine("You won!");
                    }
                    else
                    {
                        Console.WriteLine("You lost! It was '{0}'", wordToGuess);
                    }
                    // Restart the game 
                        Console.WriteLine("Do you want to play again? (press 'y' to play again)");
                        playAgain = Console.ReadLine();
                    if (playAgain == "y")
                    {
                        lives = 6;
                        won = false;
                        lettersRevealed = 0;
                        DrawMan(6);
                        incorrectGuesses.Clear();
                    }
                    else
                    {
                        Console.Write("Press ENTER to exit...");
                        Console.ReadLine();
                        System.Environment.Exit(0);
                    }
                    
                }
            }
            

        }
    }
}