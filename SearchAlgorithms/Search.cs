﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    public  class Search
    {
        const int AMOUNT_OF_LETTERS = 256;
        public static void bruteForce (String pattern, string text)
        {
            int patternLength = pattern.Length;
            int textLength = text.Length;

            for(int i =0; i < textLength-patternLength; i++)
            {
                int j;

                for(j =0; j < patternLength; j++) {
                    if (text[i+j] != pattern[j])
                    {
                        break;
                    }
                }

               // if (j == patternLength) return i;
                //works
            }
            //doesn't work
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
        public static void kmp(String pattern, String text)
        {
            int m = pattern.Length;
            int n = text.Length;

            int[] lps = new int[m];
            int j = 0;

            lps=getLps(pattern, m, lps);


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
                }

                if(i<n && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
        }
        public static void rabinKarp(String pattern, String text, int pNumber)
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
                        Console.WriteLine("Pattern found at index " + i);
                }

                if (i < n - m)
                {
                    textHash = (AMOUNT_OF_LETTERS * (textHash - text[i] * h) + text[i + m]) % pNumber;

                    if (textHash < 0)
                        textHash = (textHash + pNumber);
                }
            }
        }
    }
}
