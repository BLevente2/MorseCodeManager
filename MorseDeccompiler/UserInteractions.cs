using System;

namespace MorseCodeManager
{
    public static class UserInteractions
    {
        private static readonly MorseCode morseCode = new MorseCode();

        public static void RunMainMenu()
        {
            bool runMainMenu = true;

            do
            {
                Console.WriteLine("\n1. Enter 1 to compile text to Morse code." +
                                  "\n2. Enter 2 to decompile Morse code to text." +
                                  "\n3. Enter 3 to set the character for the short signal." +
                                  "\n4. Enter 4 to set the character for the long signal." +
                                  "\n5. Enter 5 to check the current characters that represent the short and long signals." +
                                  "\n6. Enter 6 to quit the Morse Code Manager application.");

                char enteredChar = GetCharFromUser("Enter the corresponding number of the menu option you want to choose: ");

                switch (enteredChar)
                {
                    case '1':
                        string textToCompile = GetStringFromUser("Enter the text you want to compile to Morse code: ");
                        Console.WriteLine($"Compiled text:\n{morseCode.Compile(textToCompile)}");
                        break;
                    case '2':
                        string textToDecompile = GetStringFromUser("Enter the Morse code you want to decompile to text: ");
                        Console.WriteLine($"Decompiled text:\n{morseCode.Decompile(textToDecompile)}");
                        break;
                    case '3':
                        char enteredShortChar = GetCharFromUser("Enter the character that will represent the short signal: ");
                        if (enteredShortChar != morseCode.LongChar)
                        {
                            morseCode.ShortChar = enteredShortChar;
                            Console.WriteLine($"The character for the short signal was set to: {morseCode.ShortChar}");
                        }
                        else
                        {
                            Console.WriteLine($"Unable to set short signal to '{enteredShortChar}', because the long signal is currently represented by it.");
                        }
                        break;
                    case '4':
                        char enteredLongChar = GetCharFromUser("Enter the character that will represent the long signal: ");
                        if (enteredLongChar != morseCode.ShortChar)
                        {
                            morseCode.LongChar = enteredLongChar;
                            Console.WriteLine($"The character for the long signal was set to: {morseCode.LongChar}");
                        }
                        else
                        {
                            Console.WriteLine($"Unable to set long signal to '{enteredLongChar}', because the short signal is currently represented by it.");
                        }
                        break;
                    case '5':
                        Console.WriteLine(morseCode);
                        break;
                    case '6':
                        runMainMenu = false;
                        Console.WriteLine("The Morse Code Manager application has been closed.");
                        break;
                    default:
                        Console.WriteLine($"Invalid input! '{enteredChar}' is not a valid option. Please try again.");
                        break;
                }

            } while (runMainMenu);

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
