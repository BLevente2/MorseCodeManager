using System.Collections.Generic;

namespace MorseCodeManager
{
    public class MorseCode
    {
        private static readonly Dictionary<char, string> complierCodes = new Dictionary<char, string>()
{
    { 'A', ".-" }, { 'B', "-..." }, { 'C', "-.-." }, { 'D', "-.." }, { 'E', "." },
    { 'F', "..-." }, { 'G', "--." }, { 'H', "...." }, { 'I', ".." }, { 'J', ".---" },
    { 'K', "-.-" }, { 'L', ".-.." }, { 'M', "--" }, { 'N', "-." }, { 'O', "---" },
    { 'P', ".--." }, { 'Q', "--.-" }, { 'R', ".-." }, { 'S', "..." }, { 'T', "-" },
    { 'U', "..-" }, { 'V', "...-" }, { 'W', ".--" }, { 'X', "-..-" }, { 'Y', "-.--" },
    { 'Z', "--.." }, { '1', ".----" }, { '2', "..---" }, { '3', "...--" },
    { '4', "....-" }, { '5', "....." }, { '6', "-...." }, { '7', "--..." },
    { '8', "---.." }, { '9', "----." }, { '0', "-----" }, { '.', ".-.-.-" },
    { ',', "--..--" }, { '?', "..--.." }, { '\'', ".----." }, { '!', "-.-.--" },
    { '/', "-..-." }, { '(', "-.--." }, { ')', "-.--.-" }, { '&', ".-..." },
    { ':', "---..." }, { ';', "-.-.-." }, { '=', "-...-" }, { '+', ".-.-." },
    { '-', "-....-" }, { '_', "..--.-" }, { '"', ".-..-." }, { '$', "...-..-" },
    { '@', ".--.-." }
        };

        private static readonly Dictionary<string, char> decomplierCodes = new Dictionary<string, char>()
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


        public const char DEFAULT_SHORT_CHAR = '.';
        public const char DEFAULT_LONG_CHAR = '-';
        public const char CHARACTER_BOUNTARY = ' ';
        private static readonly string[] WORD_BOUNDARY = new string[] { "/", "   " };

        public char ShortChar {  get; set; }
        public char LongChar { get; set; }

        public MorseCode(char shortChar = DEFAULT_SHORT_CHAR, char longChar = DEFAULT_LONG_CHAR)
        {
            this.ShortChar = shortChar;
            this.LongChar = longChar;
        }

        public override string ToString()
        {
            return $"Short: '{ShortChar}', Long: '{LongChar}'";
        }

        #region Compiler
        public string Compile(string textToCompile)
        {
            List<char> compiledText = new List<char>();

            textToCompile = textToCompile.ToUpper();

            foreach (char character in textToCompile)
            {
                if (character == ' ')
                {
                    compiledText.AddRange("  ");
                }
                else if (complierCodes.TryGetValue(character, out string? result) && result != null)
                {
                    compiledText.AddRange($" {result}");
                }
                else
                {
                    compiledText.AddRange($" ?{character}?");
                }
            }

            return ConvertToNotStandardMorseCode(new string(compiledText.ToArray()));
        }

        private string ConvertToNotStandardMorseCode(string standardMorseCode)
        {
            if (ShortChar == DEFAULT_SHORT_CHAR && LongChar == DEFAULT_LONG_CHAR) return standardMorseCode;
            if (ShortChar != DEFAULT_SHORT_CHAR) standardMorseCode = standardMorseCode.Replace(DEFAULT_SHORT_CHAR, ShortChar);
            if (LongChar != DEFAULT_LONG_CHAR) standardMorseCode = standardMorseCode.Replace(DEFAULT_LONG_CHAR, LongChar);
            return standardMorseCode;
        }
        #endregion

        #region Decompiler
        public string Decompile(string textToDecompile)
        {
            List<char> decompiledText = new List<char>();

            textToDecompile = ConvertToStandardMorseCode(textToDecompile);

            string[] words = textToDecompile.Split(WORD_BOUNDARY, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                string[] morseChars = word.Split(CHARACTER_BOUNTARY, StringSplitOptions.RemoveEmptyEntries);

                foreach (string morseChar in morseChars)
                {
                    if (decomplierCodes.TryGetValue(morseChar, out char result))
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
            if (ShortChar == DEFAULT_SHORT_CHAR && LongChar == DEFAULT_LONG_CHAR) return notStandardMorseCode;
            if (ShortChar != DEFAULT_SHORT_CHAR) notStandardMorseCode = notStandardMorseCode.Replace(ShortChar, DEFAULT_SHORT_CHAR);
            if (LongChar != DEFAULT_LONG_CHAR) notStandardMorseCode = notStandardMorseCode.Replace(LongChar, DEFAULT_LONG_CHAR);
            return notStandardMorseCode;
        }
        #endregion
    }
}