using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interleaving
{
    class Program
    {
        static void IsInterLeaving(string a, string b, string c)
        {
            bool[,] IL = new bool[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
            {
                for (int j = 0; j <= b.Length; j++)
                {
                    if (i == 0 && j == 0)
                        IL[i, j] = true;

                    else if (i == 0 && b[j - 1] == c[j - 1])
                        IL[i, j] = IL[i, j - 1];

                    else if (j == 0 && a[i - 1] == c[i - 1])
                        IL[i, j] = IL[i - 1, j];

                    else if (i == 0 || j == 0)
                        continue;

                    else if (a[i - 1] == c[i + j - 1] && b[j - 1] != c[i + j - 1])
                        IL[i, j] = IL[i - 1, j];

                    else if (a[i - 1] != c[i + j - 1] && b[j - 1] == c[i + j - 1])
                        IL[i, j] = IL[i, j - 1];

                    else if (a[i - 1] == c[i + j - 1] && b[j - 1] == c[i + j - 1])
                        IL[i, j] = (IL[i - 1, j] || IL[i, j - 1]);
                }
            }

            if (IL[a.Length, b.Length])
                Console.WriteLine("{0} is an interleaving of {1} and {2}", c, a, b);
            else
                Console.WriteLine("Not an interleaving");
        }

        static void Main(string[] args)
        {
            string a = "xxy";
            string b = "xxz";
            string c = "xxzxxy";
            IsInterLeaving(a, b, c);
            Console.ReadLine();
        }
    }
}
