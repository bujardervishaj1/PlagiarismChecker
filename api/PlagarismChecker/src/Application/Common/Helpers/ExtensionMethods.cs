using Konscious.Security.Cryptography;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            char[] sep = { '.', ';', '!', '?', ':', '\n', '\t' };
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

        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[16];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }

        public static byte[] GenerateHashPassword(this string password, byte[] salt) =>
             new Argon2id(Encoding.UTF8.GetBytes(password))
             {
                 Salt = salt,
                 Iterations = 2,
                 MemorySize = 1024 * 4,
                 DegreeOfParallelism = 2
             }.GetBytes(16);

        public static bool VerifyHash(this string password, byte[] salt, byte[] hash) =>
            hash.SequenceEqual(GenerateHashPassword(password, salt));
    }
}
