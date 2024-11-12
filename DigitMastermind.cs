using System;
using System.Collections.Generic;
using System.Linq;

namespace QuadaxProgrammingProject
{
	public class DigitMastermind
	{
		public static void Main(string[] args)
		{
            const int numDigits = 4;
            const int numGuesses = 10;
            const int largestDigit = 6;
            
            //Randomly generate answer
            //Duplicate digits are allowed
            Random rand = new Random();
            int[] trueDigits = new int[numDigits];
            for(int i = 0; i < numDigits; i++){
                trueDigits[i] = rand.Next(largestDigit) + 1;
            }
            
            //Process guesses
            bool guessedCorrectly = false;
            for(int guessesLeft = numGuesses; guessesLeft > 0; guessesLeft--){
                List<int> guessDigits = new List<int>(numDigits);

                //Collect valid guess
                bool validGuess;
                string errorMessage = $"Guesses must consist of exactly {numDigits} digits, each of which must range from 1 to {largestDigit}";
                do {
                    validGuess = true;
                    Console.WriteLine($"You have {guessesLeft} tries remaining to guess the secret answer");
                    Console.WriteLine("Enter a guess:");
                    string guess = Console.ReadLine();

                    if(guess.Length != numDigits){
                        Console.WriteLine(errorMessage);
                        validGuess = false;
                        continue;
                    }

                    guessDigits.Clear();
                    for(int j = 0; j < numDigits; j++){
                        int nextDigit = guess[j] - '0';
                        if(nextDigit < 1 || nextDigit > largestDigit){
                            Console.WriteLine(errorMessage);
                            validGuess = false;
                            break;
                        }
                        guessDigits.Add(nextDigit);
                    }
                } while(!validGuess);

                //Check correct digits
                int numAllCorrect = 0;
                int numHalfCorrect = 0;
                List<int> remainingDigits = new List<int>(numDigits);
                for(int j = numDigits-1; j >= 0; j--){
                    if(guessDigits[j] == trueDigits[j]){
                        numAllCorrect++;
                        guessDigits.RemoveAt(j);
                    }else{
                        remainingDigits.Add(trueDigits[j]);
                    }
                }

                foreach(int digit in remainingDigits){
                    if(guessDigits.Remove(digit)){ numHalfCorrect++; }
                }

                Console.Write($"Result: ");
                Console.Write(new String('+', numAllCorrect));
                Console.WriteLine(new String('-', numHalfCorrect));

                //This block can be moved up to skip displaying result details for correct guesses
                if(numAllCorrect == numDigits){ 
                    guessedCorrectly = true;
                    break;
                }
            }

            //Indicate Success or failure
            if(guessedCorrectly){
                Console.WriteLine("You won");
            }else{
                Console.WriteLine("You have no more guesses left");
                Console.Write("The correct answer was ");
                foreach (int digit in trueDigits){ Console.Write(digit); }
                Console.WriteLine();
            }
		}
    }
}
