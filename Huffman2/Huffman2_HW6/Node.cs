using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_HW5
{
    public class Node
    {
        long frequency;
        int character;
        Node left;
        Node right;
        public StringBuilder code = new StringBuilder();
        StringBuilder encodedData = new StringBuilder();
        

        public long getFrequency()
        {
            return frequency;
        }

        public int getCharacter()
        {
            return character;
        }

        public Node getLeft()
        {
            return left;
        }

        public Node getRight()
        {
            return right;
        }

        public StringBuilder getEncodedData()
        {
            return encodedData;
        }
        public void setFrequency(long freq)
        {
            this.frequency = freq;
        }

        public void setCharacter(int c)
        {
            character = c;
        }

        public void setLeft(Node left)
        {
            this.left = left;
        }

        public void setRight(Node right)
        {
            this.right = right;
        }

        public void setEncodedData(StringBuilder data)
        {
            this.encodedData = data;
        }

    }
}
