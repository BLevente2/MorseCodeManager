using System;

namespace MorseDecompiler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Morse Code Decompiler! Here you can choose the characters that will represent the short and long signals in Morse code.");

            char shortChar = '\0';
            char longChar = '\0';
            bool readMorseChars = true;

            do
            {
                Console.WriteLine();
                shortChar = GetCharFromUser("Enter the character that will represent the SHORT signal in Morse code: ");
                longChar = GetCharFromUser("Enter the character that will represent the LONG signal in Morse code: ");

                if (shortChar == longChar)
                {
                    Console.WriteLine("Warning! The characters for short and long signals cannot be the same!");
                }
                else
                {
                    readMorseChars = false;
                }
            } while (readMorseChars);

            Decompiler decompiler = new Decompiler(shortChar, longChar);
            bool runDecompiler = true;

            do
            {
                Console.WriteLine($"\nUsing the characters you've entered for short '{shortChar}' and long '{longChar}' signals in Morse code.\n" +
                                   "Use a single space ' ' between the characters of a Morse code.\n" +
                                   "Use a slash '/' or three spaces '   ' between words in Morse code.");

                string textToDecompile = GetStringFromUser("Enter the Morse code you'd like to decompile: ");
                string decompiledText = decompiler.Decompile(textToDecompile);

                Console.WriteLine($"\nYour decompiled Morse code:\n{decompiledText}\n");

                char continueDecompiling = GetCharFromUser("Would you like to continue decompiling Morse codes? (Y/N): ");
                if (continueDecompiling == 'N' || continueDecompiling == 'n') runDecompiler = false;

            } while (runDecompiler);
        }

        private static char GetCharFromUser(string prompt)
        {
            bool success = false;
            char enteredChar = '\0';

            do
            {
                Console.Write(prompt);

                string? enteredText = Console.ReadLine();
                if (enteredText != null && enteredText.Length == 1)
                {
                    enteredChar = enteredText[0];
                    success = true;
                }
                else
                {
                    Console.WriteLine("Warning! You must enter exactly one character.");
                }

            } while (!success);

            return enteredChar;
        }

        private static string GetStringFromUser(string prompt)
        {
            bool success = false;
            string enteredString = string.Empty;

            do
            {
                Console.Write(prompt);

                string? enteredText = Console.ReadLine();
                if (!string.IsNullOrEmpty(enteredText))
                {
                    enteredString = enteredText;
                    success = true;
                }
                else
                {
                    Console.WriteLine("Warning! You must enter a valid text.");
                }

            } while (!success);

            return enteredString;
        }
    }
}