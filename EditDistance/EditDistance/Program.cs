using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditDistance
{
    class Program
    {
        private static int dc = 10; //delete cost
        private static int ic = 1;  //insert cost
        private static int sc = 1;  //substitute cost
        
        public static int EditDistance(string src, string target)
        {
            if (target.Length == 0)
                return src.Length * dc;

            if (src.Length == 0)
                return target.Length * ic;

            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];
            int cost;

            for (int i = 0; i <= target.Length; i++)
                v0[i] = i * ic;  //Dist[0, i], src is empty, then distance is same as inserting i chars

            for (int i = 0; i < src.Length; i++)
            {
                v1[0] = (i + 1) * dc; //target is empty (Dist[j, 0]), delete i+1 chars from src to match target.

                for (int j = 0; j < target.Length; j++)
                {
                    cost = (src[i] == target[j]) ? 0 : sc;

                    v1[j + 1] = min(v1[j] + ic, v0[j + 1] + dc, v0[j] + cost);
                }

                for (int j = 0; j < v0.Length; j++)
                {
                    v0[j] = v1[j];
                }
            }

            return v1[target.Length];
        }

        private static int min(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        static void Main(string[] args)
        {
            string src = "abc";
            string target = "ab";
            Console.WriteLine("Edit distance = {0}", EditDistance(src, target));
            Console.ReadLine();
        }
    }
}
