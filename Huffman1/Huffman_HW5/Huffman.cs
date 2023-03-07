using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_HW5
{

    public class Huffman
    {
        public int[] dictionary;
        List<Node> nodes;
        public string result;

        public Huffman()
        {
            dictionary = new int[256];
            nodes = new List<Node>();
            result = "";
        }

        void AddToList(List<Node> myList, Node myNode)
        {
            bool ok = false;
            for(int i=0; i<myList.Count; i++)
            {
                var listNode = myList[i];
                if (listNode.getFrequency() > myNode.getFrequency())
                {
                    myList.Insert(i, myNode);
                    ok = true;
                    break;
                }
                if(listNode.getFrequency() == myNode.getFrequency())
                    if(listNode.getCharacter() > myNode.getCharacter())
                    {
                        myList.Insert(i, myNode);
                        ok = true;
                        break;
                    }
            }    
            if(ok == false)
                myList.Add(myNode);

        }

        public Node HuffmanTree()
        {

            for(int i = 0; i < dictionary.Length;i++)
            {
                if (dictionary[i] != 0)
                {
                    Node node = new Node();
                    node.setFrequency(dictionary[i]);
                    node.setCharacter(i);
                    //Console.WriteLine(i);
                    node.setRight(null);
                    node.setLeft(null);
                    AddToList(this.nodes, node);
                }

            }

            int innerNode = 256;
            int n = nodes.Count();
            //Console.WriteLine(n);
            for(int i = 1; i < n; i++)
            {
                Node z = new Node();

                Node x = nodes.ElementAt(0);
                //Console.WriteLine(x.getCharacter());
                //Console.WriteLine(x.getFrequency());
                nodes.RemoveAt(0);

                Node y = nodes.ElementAt(0);
                //Console.WriteLine(y.getCharacter());
                //Console.WriteLine(y.getFrequency());
                nodes.RemoveAt(0);

                z.setLeft(x);
                z.setRight(y);
                z.setCharacter(innerNode);
                z.setFrequency(x.getFrequency() + y.getFrequency());
                AddToList(this.nodes, z);

            }
            Node root = nodes.ElementAt(0);
            return root;

        }

        public void recursivePreorder(Node root)
        {
            if (root.getCharacter() != 256)
                result += "*" + root.getCharacter() + ":" + root.getFrequency() + " ";
            else
                result += root.getFrequency() + " ";
            if (root.getLeft() != null)
            {
               
                recursivePreorder(root.getLeft());
            }
            if (root.getRight() != null)
            {
                recursivePreorder(root.getRight());
            }
        }


    }
}
