using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleInCS
{
    internal class Program
    {
        static bool won = false;
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void WriteAtBottomLeft(string input)
        {
            int pos = Console.CursorTop;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            ClearCurrentConsoleLine();
            Console.Write(input);
            Console.SetCursorPosition(0, pos);
        }
        public static void CompareWords(string input, string compareto)
        {
            if(!won)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                char[] charinput = input.ToCharArray();
                char[] charcompare = compareto.ToCharArray();
                char[] modifiable = compareto.ToCharArray();
                for (int i = 0; i < 5; i++)
                {

                    if (charinput[i] == charcompare[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(charinput[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Random random = new Random();
            List<string> words = File.ReadAllLines("words.txt").ToList();
            string word = words[random.Next(words.Count)];
            Console.WriteLine(word);
            int guessCount = 0;
            while (guessCount < 5)
            {
                string inputword = Console.ReadLine();
                if (inputword == word)
                {
                    guessCount++;
                    won = true;
                    CompareWords(inputword, word);
                    Console.ForegroundColor = ConsoleColor.Green;
                    WriteAtBottomLeft("You won!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                    break;
                }
                if (inputword != word && inputword.Length == 5)
                {
                    if (words.Contains(inputword))
                    {
                        guessCount++;
                        CompareWords(inputword, word);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteAtBottomLeft("Your input is not a real word.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                }
                if (inputword.Length != 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteAtBottomLeft("Your guess is not 5 letters.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            if (!won)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteAtBottomLeft("You lost. The word was " + word + ".");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();
            }
        }
    }
}
