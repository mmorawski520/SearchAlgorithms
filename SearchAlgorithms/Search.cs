using System;
using System.Collections.Generic;

namespace SearchAlgorithms
{
    public class Search
    {
        const int AMOUNT_OF_LETTERS = 256;

        private static void incorrectCharHeuristic(string str, int size, ref int[] incorrectChar)
        {
            int i;

            for (i = 0; i < AMOUNT_OF_LETTERS; i++)
                incorrectChar[i] = -1;

            for (i = 0; i < size; i++)
                incorrectChar[(int)str[i]] = i;
        }

        static int[] getLps(string pattern, int m, int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0;

            while (i < m)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
            return lps;
        }
        public static bool boyerMoore(string pattern, string text)
        {
            List<int> returnedVal = new List<int>();
            int m = pattern.Length;
            int n = text.Length;

            int[] incorrectChar = new int[256];

            incorrectCharHeuristic(pattern, m, ref incorrectChar);

            int s = 0;
            while (s <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pattern[j] == text[s + j])
                    --j;

                if (j < 0)
                {
                    returnedVal.Add(s);
                    s += (s + m < n) ? m - incorrectChar[text[s + m]] : 1;
                }
                else
                {
                    s += Math.Max(1, j - incorrectChar[text[s + j]]);
                }
            }
            if (returnedVal.Count > 0)
                return true;
            else
                return false;
        }
        public static bool bruteForce(String pattern, string text)
        {
            int patternLength = pattern.Length;
            int textLength = text.Length;

            for (int i = 0; i < textLength - patternLength; i++)
            {
                int j;

                for (j = 0; j < patternLength; j++)
                {
                    if (text[i + j] != pattern[j])
                    {
                        break;
                    }
                }

                if (j == patternLength)
                    return true;

            }
            return false;
        }

        public static bool kmp(String pattern, String text)
        {
            int m = pattern.Length;
            int n = text.Length;

            int[] lps = new int[m];
            int j = 0;

            lps = getLps(pattern, m, lps);


            int i = 0;
            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }
                if (j == m)
                {
                    j = lps[j - 1];
                    return true;
                }

                if (i < n && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
            return false;
        }
        public static bool rabinKarp(String pattern, String text, int pNumber)
        {
            int i, j;

            int patternHash = 0;
            int textHash = 0;
            int h = 1;

            int m = pattern.Length;
            int n = text.Length;

            for (i = 0; i < m - 1; i++)
                h = (h * AMOUNT_OF_LETTERS) % pNumber;

            for (i = 0; i < m; i++)
            {
                patternHash = (AMOUNT_OF_LETTERS * patternHash + pattern[i]) % pNumber;
                textHash = (AMOUNT_OF_LETTERS * textHash + text[i]) % pNumber;
            }

            for (i = 0; i <= n - m; i++)
            {
                if (patternHash == textHash)
                {
                    for (j = 0; j < m; j++)
                    {
                        if (text[i + j] != pattern[j])
                            break;
                    }

                    if (j == m)
                        return true;
                }

                if (i < n - m)
                {
                    textHash = (AMOUNT_OF_LETTERS * (textHash - text[i] * h) + text[i + m]) % pNumber;

                    if (textHash < 0)
                        textHash = (textHash + pNumber);
                }
            }
            return false;
        }
    }
}
