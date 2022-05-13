using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    public static class Search
    {
        const int AMOUNT_OF_LETTERS = 256;
        public static void RabinKarp(char[] pattern,char[] text,int q) {
            int M = pattern.Length;
            int N = text.Length;
            int i, j;
            int hashPattern = 0; 
            int hashText = 0; 
            int h = 1;

            for(i =0; i < M - 1; i++)
            {
                h = (h * AMOUNT_OF_LETTERS) % q;
            }

            for (i =0; i<M; i++) {
                hashPattern= (AMOUNT_OF_LETTERS * pattern[i]) % q;
                hashText = (AMOUNT_OF_LETTERS * text[i]) % q;
            }

            for (i = 0; i <= N - M; i++)
            { 
                if(pattern == text)
                {
                    bool flag = true;
                    for (j = 0; j < M; j++)
                    {
                        if (text[i + j] != pattern[j])
                        {
                            flag = false;
                            break;
                        }
                       
                    }
                    if (j == M) ;
                }
                if (i < N - M)
                {
                    hashText = (AMOUNT_OF_LETTERS * (hashText - text[i] * h) + text[i + M]) % q;

                    if (hashText < 0)
                        hashText = (hashText + q);
                }
            }
        }
    }
}
