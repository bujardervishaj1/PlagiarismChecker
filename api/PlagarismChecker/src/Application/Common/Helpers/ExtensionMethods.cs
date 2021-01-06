using System;
using System.Linq;

namespace PlagarismChecker.Application.Common.Helpers
{
    public static class ExtensionMethods
    {
        public static string[] GetWordGroups(this string text, int num)
        {
            string[] tokens = text.Split(' ');
            int numOfGroups = tokens.Length / num;

            string[] groups = new string[numOfGroups];
            int curIndex = 0;
            for (int x = 0; x < numOfGroups; x++)
            {
                for (int i = 0; i < num; i++)
                {

                    if (i != 0) groups[x] += " " + tokens[curIndex];
                    else groups[x] += tokens[curIndex];

                    curIndex++;
                }
            }

            return groups;
        }

        public static string[] GetSentences(this string text)
        {
            char[] sep = { '.', ';', '!', '?', ':' };
            string[] sentences = text.Split(sep);

            return sentences.Where(s => !s.Equals("")).ToArray();
        }

        public static int CountOccurenceswWithinString(this string text, string match)
        {
            int count = 0, n = 0;

            if (match != "")
                while ((n = text.IndexOf(match, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += match.Length;
                    ++count;
                }

            return count;
        }
    }
}
