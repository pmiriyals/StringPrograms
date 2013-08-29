using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlattenDictionary
{
    class Program
    {
        //I am building both dictionary and simple string output
        
        static Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
        
        static void Flatten(string str)
        {
            string[] tokens = str.Split(',');
            string flattened = "[";

            for(int i = 0; i < tokens.Length; i++)
            {
                if(i != tokens.Length -1)
                    flattened += Parse(tokens[i]) + ", ";
                else
                    flattened += Parse(tokens[i]) + "]";
            }
            Console.WriteLine("Falttened String = {0}", flattened);
            printDictionary();
        }

        static void printDictionary()
        {
            Console.Write("\n\nFrom dictionary data structure: ");
            string s = "[";
            foreach (string key in dict.Keys)
            {
                foreach (int i in dict[key])
                {
                    s = s + "(" + key + "," + i.ToString() + "),";
                }
            }
            s = s.Substring(0, s.Length - 1) + "]";
            Console.Write(s);
        }

        static string Parse(string token)
        {
            string[] arr = token.Split(':');
            int val = -1;
            string s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (Int32.TryParse(arr[i].Trim(), out val) && i > 0)
                {
                    s = arr[i - 1];
                    break;
                }
            }
            if (val > 0 && s.Length > 0)
            {
                s = s.Trim();
                if (!dict.ContainsKey(s))
                {
                    List<int> lstInt = new List<int>();
                    lstInt.Add(val);
                    dict.Add(s, lstInt);
                }
                else
                    dict[s].Add(val);

                s = "(" + s + "," + val.ToString() + ")";
            }
            return s;
        }



        static void Main(string[] args)
        {
            string str = "{'a': 1, 'c': {'d': {'e': 4}}, 'b': {'a': 2, 'b': 3}}";
            string refine = "";
            foreach (char c in str)
            {
                if (!(c == '{' || c == '}'))
                    refine += c;
            }
            //Console.WriteLine(refine);
            Flatten(refine);
            Console.ReadLine();
        }
    }
}
