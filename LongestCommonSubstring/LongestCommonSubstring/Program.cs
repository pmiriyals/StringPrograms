using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LongestCommonSubstring
{
    class Program
    {
        //1. Define a matrix LCS[i, j], where it calculates the length of longest common substring from X[0 to i] and Y[0 to j], this takes O(mn)
        //2. After calculating the length traverse the matrix in such a way that it gives the LCS, this takes O(m + n)
        //3. Total time taken using Dynamic Programming is O(mn) (probably suffix tree might be more efficient)

        static void LCS(string a, string b)
        {
            int la = a.Length;
            int lb = b.Length;
            int[,] lcs = new int[la + 1, lb + 1];

            int max = 0;
            int mi = 0, mj = 0;

            for (int i = 0; i <= la; i++)
            {
                for (int j = 0; j <= lb; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        lcs[i, j] = 0;  //one of them is an empty string, then there is no lcs
                        continue;
                    }

                    if (a[i - 1] == b[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                        if (max < lcs[i, j])
                        {
                            max = lcs[i, j];
                            mi = i; mj = j;
                        }
                    }
                    else
                        lcs[i, j] = 0;// Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                }
            }
            Console.WriteLine("Length of lcs = {0}\nLongest Common Substring = {1}", max, getLCS(lcs, mi, mj, a, ""));
            //print2D(lcs);
        }

        static string getLCS(int[,] lcs, int i, int j, string a, string s)
        {
            if (i <= 0 || j <= 0 || lcs[i, j] == 0)
                return s;

            return getLCS(lcs, i - 1, j - 1, a, a[i - 1] + s);
        }

        static void print2D(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write("{0} ", arr[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string a = "this is longest common substring";
            string b = "this is not longest common subsequence";
            LCS(a, b);        
            Console.ReadLine();
        }
    }
}
