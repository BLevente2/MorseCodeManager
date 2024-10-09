using System;
using System.Collections.Generic;

namespace MorseDecompiler
{
    public class Decompiler
    {
        private static readonly Dictionary<string, char> morseCodes = new Dictionary<string, char>()
{
    { ".-", 'A' }, { "-...", 'B' }, { "-.-.", 'C' }, { "-..", 'D' }, { ".", 'E' },
    { "..-.", 'F' }, { "--.", 'G' }, { "....", 'H' }, { "..", 'I' }, { ".---", 'J' },
    { "-.-", 'K' }, { ".-..", 'L' }, { "--", 'M' }, { "-.", 'N' }, { "---", 'O' },
    { ".--.", 'P' }, { "--.-", 'Q' }, { ".-.", 'R' }, { "...", 'S' }, { "-", 'T' },
    { "..-", 'U' }, { "...-", 'V' }, { ".--", 'W' }, { "-..-", 'X' }, { "-.--", 'Y' },
    { "--..", 'Z' }, { ".----", '1' }, { "..---", '2' }, { "...--", '3' },
    { "....-", '4' }, { ".....", '5' }, { "-....", '6' }, { "--...", '7' },
    { "---..", '8' }, { "----.", '9' }, { "-----", '0' }, { ".-.-.-", '.' },
    { "--..--", ',' }, { "..--..", '?' }, { ".----.", '\'' }, { "-.-.--", '!' },
    { "-..-.", '/' }, { "-.--.", '(' }, { "-.--.-", ')' }, { ".-...", '&' },
    { "---...", ':' }, { "-.-.-.", ';' }, { "-...-", '=' }, { ".-.-.", '+' },
    { "-....-", '-' }, { "..--.-", '_' }, { ".-..-.", '"' }, { "...-..-", '$' },
    { ".--.-.", '@' }
};

        private char _shortChar;
        private char _longChar;
        private const char defaultShortChar = '.';
        private const char defaultLongChar = '-';
        private const char charBoundary = ' ';
        private static readonly string[] wordBoundary = new string[] { "/", "   " };

        public Decompiler(char shortChar = defaultShortChar, char longChar = defaultShortChar)
        {
            _shortChar = shortChar;
            _longChar = longChar;
        }

        public string Decompile(string textToDecompile)
        {
            List<char> decompiledText = new List<char>();

            if (_shortChar != defaultShortChar || _longChar != defaultLongChar) textToDecompile = ConvertToStandardMorseCode(textToDecompile);

            string[] words = textToDecompile.Split(wordBoundary, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                string[] morseChars = word.Split(charBoundary, StringSplitOptions.RemoveEmptyEntries);

                foreach (string morseChar in morseChars)
                {
                    if (morseCodes.TryGetValue(morseChar, out char result))
                    {
                        decompiledText.Add(result);
                    }
                    else
                    {
                        decompiledText.AddRange($"?{morseChar}?");
                    }
                }

                decompiledText.Add(' ');
            }

            if (decompiledText.Count > 0) decompiledText.RemoveAt(decompiledText.Count - 1);
            return new string(decompiledText.ToArray());
        }

        private string ConvertToStandardMorseCode(string notStandardMorseCode)
        {
            char[] tempChars = notStandardMorseCode.ToCharArray();
            for (int i = 0; i < tempChars.Length; i++)
            {
                if (tempChars[i] == _shortChar) tempChars[i] = defaultShortChar;
                else if (tempChars[i] == _longChar) tempChars[i] = defaultLongChar;
            }
            return new string(tempChars);
        }
    }
}