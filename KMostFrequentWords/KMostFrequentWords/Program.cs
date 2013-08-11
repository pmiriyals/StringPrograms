using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KMostFrequentWords
{
    class Node
    {
        public string word { get; set; }
        public int count { get; set; }
        public Node next { get; set; }

        public Node(string word) : this(word, 1, null) { }        
        public Node(string word, int count, Node next)
        {
            this.word = word;
            this.count = count;
            this.next = next;
        }
    }
    
    class Program
    {
        static Dictionary<string, Node> dict = new Dictionary<string, Node>();
        static Node head = new Node("");

        static void AddWord(string s)
        {
            if (dict.ContainsKey(s))
            {
                dict[s].count++;
            }
            else
            {
                Node n = new Node(s);
                dict.Add(s, n);
                n.next = head.next;
                head.next = n;
            }
        }

        static void heapify(Node[] nodes, int count)
        {
            int end = count - 1; //count is total number of nodes in the array, which is k.
            int start = (end - 1) / 2;  //get parent of last but one child, that is the last parent in the array

            while (start >= 0)
            {
                siftDown(nodes, start, end);    //end is the index of last node in the array
                start--;
            }
        }

        static void siftDown(Node[] nodes, int start, int end)
        {
            int root = start;

            while ((2 * root + 1) <= end)
            {
                int swap = root;
                int child = (2 * root + 1); //get child of the current root node

                if (nodes[swap].count > nodes[child].count)
                    swap = child;

                if ((child + 1 <= end) && (nodes[swap].count > nodes[child + 1].count))
                    swap = child + 1;

                if (swap != root)
                {
                    Node temp = nodes[swap];
                    nodes[swap] = nodes[root];
                    nodes[root] = temp;
                    root = swap;
                }
                else
                    return;
            }
        }
        
        static void FindKMostFrequentWords(int k)
        {
            Node[] nodes = new Node[k];
            Node cur = head.next;
            //Get 1st K Nodes from linked list: O(k)
            int i;
            for (i = 0; i < k && cur != null; i++)
            {
                nodes[i] = cur;
                cur = cur.next;
            }

            //O(n), building a heap
            heapify(nodes, i);   //place 1st k nodes in min heap, either i is k or i is the total number of words which is < k

            while (cur != null)
            {
                if (cur.count > nodes[0].count)
                {
                    nodes[0] = cur; //O(log n)
                    heapify(nodes, nodes.Length);
                }
                cur = cur.next;
            }
            Console.WriteLine("K most frequently occurring words, where k = {0} are,", k);
            foreach (Node n in nodes)
            {
                Console.WriteLine("{0} : [count = {1}]", n.word, n.count);
            }
        }

        static void Main(string[] args)
        {
            string path = @"C:\Praneeth\C#\TestSampleFiles\words.txt";
            using(StreamReader sr = new StreamReader(path))
            {
                string line = "";
                while((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    foreach(string s in words)
                    {
                        AddWord(s);
                    }
                }
            }

            //At this point I have my dictionary ready
            FindKMostFrequentWords(5);
            Console.ReadLine();
        }
    }
}
