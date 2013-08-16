using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LongestCommonSubsequence
{
    class Program
    {
        //1. Using dynamic programming: Time = O(mn)
        //2. Calculate length of the longest common subsequence
        //3. LCS[i, j] is the longest common subseq till X[0 to i-1] and Y[0 to j-1]
        //4. LCS[m, n] is the longest common subsequence
        //5. Traverse the matrix in a way that gives me the LCS
        //6. LCS[i, j] = LCS[i-1, j-1] + 1, iff X[i-1] == Y[j-1]
        //7. LCS[i, j] = Math.Max(LCS[i-1, j], LCS[i, j-1]), otherwise
        
        static void LongestCommonSubsequence(string a, string b)
        {
            int la = a.Length;
            int lb = b.Length;
            int[,] lcs = new int[la + 1, lb + 1];

            for (int i = 1; i <= la; i++)
            {
                for (int j = 1; j <= lb; j++)
                {
                    if (a[i - 1] == b[j - 1])
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                    else
                        lcs[i, j] = Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                }
            }
            Console.WriteLine("Length of longest common subsequence = {0}\nLongest Common Subsequence = {1}", lcs[la, lb], getLCS(lcs, la, lb, b, ""));
            print2D(lcs);

        }

        static string getLCS(int[,] lcs, int i, int j, string b, string s)
        {            
            if (i <= 0 || j <= 0)
                return s;

            if (lcs[i, j] == lcs[i, j - 1])
                return getLCS(lcs, i, j - 1, b, s);

            else if (lcs[i, j] == lcs[i - 1, j])
                return getLCS(lcs, i - 1, j, b, s);

            else if (lcs[i, j] == (lcs[i - 1, j - 1] + 1))
                return getLCS(lcs, i - 1, j - 1, b, b[j - 1] + s);
            else
                return "";
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
            string a = "AGGTAB";
            string b = "GXTXAYB";
            LongestCommonSubsequence(a, b);
            lcs(a, b);
            Console.ReadLine();
        }
    }
}
