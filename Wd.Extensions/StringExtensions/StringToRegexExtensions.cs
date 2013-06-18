using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wd.Extensions
{
    /// <summary>
    /// 字符串到正则表达扩展
    /// </summary>
    public static class StringToRegexExtensions
    {
        /// <summary>
        /// 字符串是否匹配模式
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="pattern">正则表达式匹配模式</param>
        /// <returns>是否匹配</returns>
        /// <remarks>author:chenwei 2012-05-25</remarks>
        public static bool IsMatch(this string input, string pattern)
        {
            return IsMatch(input, pattern, StringRegexOptions.None);
        }
        /// <summary>
        /// 字符串是否匹配模式
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="pattern">正则表达式匹配模式</param>
        /// <param name="options">正则表达式匹配选项</param>
        /// <returns>是否匹配</returns>
        /// <remarks>author:chenwei 2012-05-25</remarks>
        public static bool IsMatch(this string input, string pattern, StringRegexOptions options)
        {
            return Regex.IsMatch(input, pattern, ROptions(options));
        }
        /// <summary>
        /// 字符串是否匹配模式
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="pattern">正则表达式匹配模式</param>
        /// <param name="options">正则表达式匹配选项</param>
        /// <param name="startAt">开始匹配位置</param>
        /// <returns>是否匹配</returns>
        /// <remarks>author:chenwei 2012-05-25</remarks>
        public static bool IsMatch(this string input, string pattern, StringRegexOptions options, int startAt)
        {
            Regex regex = new Regex(pattern, ROptions(options));
            return regex.IsMatch(input, startAt);
        }
        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// <remarks>author:chenwei 2012-05-25</remarks>
        public static Match Match(this string input, string pattern)
        {
            return Match(input, pattern, StringRegexOptions.None);
        }
        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this string input, string pattern, StringRegexOptions options)
        {
            return Regex.Match(input, pattern, ROptions(options));
        }
        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <param name="startAt">The zero-based character position at which to start the search.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this string input, string pattern, StringRegexOptions options, int startAt)
        {
            Regex regex = new Regex(pattern, ROptions(options));
            return regex.Match(input, startAt);
        }
        /// <summary>
        /// Searches the specified input string for all occurrences of a regular expression,  beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>A collection of the System.Text.RegularExpressions.Match objects found by the search.</returns>
        public static MatchCollection Matches(this string input, string pattern)
        {
            return Matches(input, pattern, StringRegexOptions.None);
        }
        /// <summary>
        /// Searches the specified input string for all occurrences of a regular expression,  beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
        /// <returns>A collection of the System.Text.RegularExpressions.Match objects found by the search.</returns>
        public static MatchCollection Matches(this string input, string pattern, StringRegexOptions options)
        {
            return Regex.Matches(input, pattern, ROptions(options));
        }
        /// <summary>
        /// Searches the specified input string for all occurrences of a regular expression,  beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
        /// <param name="startAt">The character position in the input string at which to start the search.</param>
        /// <returns>A collection of the System.Text.RegularExpressions.Match objects found by the search.</returns>
        public static MatchCollection Matches(this string input, string pattern, StringRegexOptions options, int startAt)
        {
            Regex regex = new Regex(pattern, ROptions(options));
            return regex.Matches(input, startAt);
        }
        private static RegexOptions ROptions(StringRegexOptions options)
        {
            RegexOptions rOptions;
            bool success = Enum.TryParse<RegexOptions>(options.ToString(), out rOptions);
            if (!success)
            {
                throw new ArgumentException("params StringRegexOptions is Exception");
            }
            return rOptions;
        }
    }
    public enum StringRegexOptions
    {
        /// <summary>
        /// Specifies that no options are set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies case-insensitive matching.
        /// </summary>
        IgnoreCase = 1,
        /// <summary>
        /// Multiline mode. Changes the meaning of ^ and $ so they match at the beginning
        /// and end, respectively, of any line, and not just the beginning and end of
        /// the entire string.
        /// </summary>
        Multiline = 2,
        /// <summary>
        /// Specifies that the only valid captures are explicitly named or numbered groups
        /// of the form (?<name>…). This allows unnamed parentheses to act as noncapturing
        /// groups without the syntactic clumsiness of the expression (?:…).
        /// </summary>
        ExplicitCapture = 4,
        /// <summary>
        /// Specifies that the regular expression is compiled to an assembly. This yields
        /// faster execution but increases startup time. This value should not be assigned
        /// to the System.Text.RegularExpressions.RegexCompilationInfo.Options property
        /// when calling the System.Text.RegularExpressions.Regex.CompileToAssembly(System.Text.RegularExpressions.RegexCompilationInfo[],System.Reflection.AssemblyName)
        /// method.
        /// </summary>
        Compiled = 8,
        /// <summary>
        /// Specifies single-line mode. Changes the meaning of the dot (.) so it matches
        ///   every character (instead of every character except \n).
        /// </summary>
        Singleline = 16,
        /// <summary>
        /// Eliminates unescaped white space from the pattern and enables comments marked
        /// with #. However, the System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace
        /// value does not affect or eliminate white space in character classes.
        /// </summary>
        IgnorePatternWhitespace = 32,
        /// <summary>
        /// Specifies that the search will be from right to left instead of from left to right.
        /// </summary>
        RightToLeft = 64,
        /// <summary>
        /// Enables ECMAScript-compliant behavior for the expression. This value can
        /// be used only in conjunction with the System.Text.RegularExpressions.RegexOptions.IgnoreCase,
        /// System.Text.RegularExpressions.RegexOptions.Multiline, and System.Text.RegularExpressions.RegexOptions.Compiled
        /// values. The use of this value with any other values results in an exception.
        /// </summary>
        ECMAScript = 256,
        /// <summary>
        /// Specifies that cultural differences in language is ignored. See Performing
        /// Culture-Insensitive Operations in the RegularExpressions Namespace for more information.
        /// </summary>
        CultureInvariant = 512,
    }
}
