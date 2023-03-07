using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_HW5
{

    public class Huffman
    {
        public long[] dictionary;
        List<Node> nodes;
        public StringBuilder result;
        public StringBuilder[] codes;
        public StringBuilder remainingCode;

        public Huffman()
        {
            dictionary = new long[256];
            nodes = new List<Node>();
            result = new StringBuilder();
            codes = new StringBuilder[256];
            for(int i = 0; i < 256; i++)
                codes[i] = new StringBuilder();
            remainingCode = new StringBuilder();
        }

        void AddToList(List<Node> myList, Node myNode)
        {
            bool ok = false;
            for (int i = 0; i < myList.Count; i++)
            {
                var listNode = myList[i];
                if (listNode.getFrequency() > myNode.getFrequency())
                {
                    myList.Insert(i, myNode);
                    ok = true;
                    break;
                }
                if (listNode.getFrequency() == myNode.getFrequency())
                    if (listNode.getCharacter() > myNode.getCharacter())
                    {
                        myList.Insert(i, myNode);
                        ok = true;
                        break;
                    }
            }
            if (ok == false)
                myList.Add(myNode);

        }

        //creates the huffmanTree
        public Node HuffmanTree()
        {

            for (int i = 0; i < dictionary.Length; i++)
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
            for (int i = 1; i < n; i++)
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

        // while traversing the tree in preorder it encodes the current node ant prints his information
        public void recursivePreorder(Node root, FileStream fs)
        {
            
            root.setEncodedData(encodeNode(root));
            PrintStringToBytes(root.getEncodedData(), fs);
            if (root.getLeft() != null)
            {
                recursivePreorder(root.getLeft(), fs);
            }
            if (root.getRight() != null)
            {
                recursivePreorder(root.getRight(), fs);
            }
        }


        public void iterativePreorder(Node root, FileStream fs)
        {
            Stack<Node> nodeStack = new Stack<Node>();
            nodeStack.Push(root);
            int a = nodeStack.Count();
            //StringBuilder nodeBits = new StringBuilder();
            while (nodeStack.Count() > 0)
            {
                Node node = nodeStack.Pop();
                node.setEncodedData(encodeNode(node));
                PrintStringToBytes(root.getEncodedData(), fs);
                if (node.getRight() != null)
                {
                    nodeStack.Push(node.getRight());
                }
                if (node.getLeft() != null)
                {
                    nodeStack.Push(node.getLeft());
                }
            }
           
        }

        //
        public void dfsHuffman(Node x, string s)
        {
            if (x.getLeft() != null)
            {
                dfsHuffman(x.getLeft(), s + "0");
            }
            if (x.getRight() != null)
            {
                dfsHuffman(x.getRight(), s + "1");
            }                

            if (x.getLeft() == null && x.getRight() == null)
            {
                //Console.WriteLine(s);
                x.code.Append(s);
                //codes.Insert(x.getCharacter() , x.code);
                codes[x.getCharacter()].Append(s);
            }
            
        }

        public string convertingLongToBytesString(long longNumber, bool isCharacter)
        {
            string result = "";
            while(longNumber != 0)
            {
                if (longNumber % 2 == 1)
                {
                    result = result + "1";
                }
                else
                {
                    result = result + "0";
                }
                longNumber /= 2;
            }
            if(isCharacter)
            {
                if (result.Length < 8)
                {
                    while (result.Length != 8)
                        result =  result + "0" ;
                }
                return result;
            }
            if (result.Length != 55)
                if (result.Length > 55)
                {
                    result = result.Remove(0, result.Length - 55);

                }
                else
                {
                    while (result.Length != 55)
                        result = result + "0";
                }
            return result;
            
        }

        StringBuilder encodeNode(Node myNode)
        {

            StringBuilder nodeEncoding = new StringBuilder();
            nodeEncoding.Append(convertingLongToBytesString(myNode.getFrequency(), false));
            if (myNode.getCharacter() == 256)
            {
                //nodeEncoding = "0" + nodeEncoding;
                nodeEncoding.Insert(0, "0");
                string innerNodeEnding = "00000000";
                nodeEncoding.Append(innerNodeEnding);
            }
            else
            {
                //nodeEncoding = "1" + nodeEncoding;
                nodeEncoding.Insert(0, "1");
                int character = myNode.getCharacter();
                string characterNodeEnding = convertingLongToBytesString((long)character, true);
                nodeEncoding.Append(characterNodeEnding);

            }

            return nodeEncoding;

        }

        public void PrintStringToBytes(StringBuilder myBytes, FileStream fs)
        {
            byte[] result = new byte[8];
            int number = 0;
            int contor = 0;
            int exponent = 1;
            //Console.WriteLine(myBytes);
            int len = myBytes.Length;
            for (int i = 0; i < len; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    exponent = 1;
                    result[contor] = (byte)number;
                    //Console.WriteLine(number);
                    contor++;
                    number = 0;
                }
                if (myBytes[i] == '1')
                    number = number + exponent;
                exponent *= 2;
            }
            result[contor] = (byte)number;

            //byte[] test = new byte[1];
            
            fs.Write(result);
        }

        public void printEncodedText(StringBuilder code, FileStream fs)
        {
            //Console.WriteLine(code.Length);
            if(remainingCode.Length > 0)
            {
                code.Insert(0, remainingCode);
                //code = remainingCode + code;
                remainingCode.Clear();
            }
            //Console.WriteLine(code.Length);
            if (code.Length % 8 != 0)
            {
                int len = code.Length;
                int nrOfRemainingCharacters = len % 8;
                //Console.WriteLine(code);
                //Console.WriteLine(nrOfRemainingCharacters);
                //Console.WriteLine(code.Length);
                //Console.WriteLine(code.Length - nrOfRemainingCharacters - 1);
                remainingCode.Append(code, len - nrOfRemainingCharacters, nrOfRemainingCharacters);
                //Console.WriteLine(remainingCode);
                //Console.WriteLine(remainingCode.Length);
                //code = code.Remove(code.Length - nrOfRemainingCharacters);
                code.Remove(len - nrOfRemainingCharacters , nrOfRemainingCharacters);
                

            }
            int nrOfBytes = code.Length / 8;
            byte[] myBytes = new byte[nrOfBytes];

            int number = 0;
            int contor = 0;
            int exponent = 1;
            int i = 0;
            //Console.WriteLine(code.Length);
            //Console.WriteLine();
            for (i = 0; i < code.Length; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    exponent = 1;
                    myBytes[contor] = (byte)number;
                    //Console.WriteLine(number);
                    contor++;
                    number = 0;
                }
                if (code[i] == '1')
                    number = number + exponent;
                exponent *= 2;
            }
            myBytes[contor] = (byte)number;
            fs.Write(myBytes);
        }

        public void printLastByte(StringBuilder myCode, FileStream fs)
        {
            int number = 0;
            int exponent = 1;
            byte[] byteToPrint = new byte[1];
            for (int i = 0;i < myCode.Length;i++)
            {
                if (myCode[i] == '1')
                    number = number + exponent;
                exponent *= 2;
            }
            byteToPrint[0] = (byte)number;
            //if(number!=0)
            fs.Write(byteToPrint);
        }
    }
}
