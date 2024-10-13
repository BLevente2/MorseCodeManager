using System;

namespace MorseCodeManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Morse Code Decompiler! Here you can choose the characters that will represent the short and long signals in Morse code.");

            UserInteractions.RunMainMenu();
        }
    }
}