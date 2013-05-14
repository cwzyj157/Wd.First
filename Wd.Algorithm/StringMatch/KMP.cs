using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Algorithm
{
    /// <summary>
    /// KMP字符串匹配算法
    /// </summary>
    public class KMP
    {
        private Dictionary<string, int> partialMatchingTable;
        private string word;
        private string search;
        public KMP(string search, string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException("word", "match word is null or empty");
            if (string.IsNullOrEmpty(search))
                throw new ArgumentNullException("search", "search word is null or empty");
            this.word = word;
            this.search = search;
            Init();
        }
        private void Init()
        {
            if (partialMatchingTable == null)
                partialMatchingTable = new Dictionary<string, int>();
            string key = string.Empty;
            for (var i = 1; i < word.Length; i++)
            {
                key = word.Substring(0, i + 1);
                partialMatchingTable[key] = GetPreSuffixion(key);
            }
        }
        private int GetPreSuffixion(string input)
        {
            int length = input.Length - 1;
            string[] preffixion = new string[length];
            string[] suffixion = new string[length];
            for (var i = 0; i < length; i++)
            {
                preffixion[i] = input.Substring(0, i + 1);
                suffixion[i] = input.Substring(length - i);
            }

            var match = preffixion.SingleOrDefault(m => { return suffixion.Contains(m); });
            return match != null ? match.Length : 0;
        }

        /// <summary>
        /// 部分匹配
        /// </summary>
        /// <returns>返回第一个匹配项的位置</returns>
        public int Match()
        {
            return Matches().First();
        }
        /// <summary>
        /// 全部匹配
        /// </summary>
        /// <returns>返回匹配项的位置列表</returns>
        public IEnumerable<int> Matches()
        {
            int j = 0;
            int step = 0;
            int first = 0;
            bool isMatch = false;
            for (var i = 0; i < search.Length; i++)
            {
                if (search[i] != word[j])
                {
                    if (j == 0) continue;
                    partialMatchingTable.TryGetValue(word.Substring(0, j), out step);
                    i = first + (j - step) - 1;
                    j = 0;
                }
                else
                {
                    if (j == 0) first = i;
                    if (j == word.Length - 1)
                    {
                        i = first + j;
                        j = 0;
                        isMatch = true;
                        yield return first;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            if (!isMatch) yield return -1;
        }
    }
}
