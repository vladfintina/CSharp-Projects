using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_HW5
{
    public class Node
    {
        int frequency;
        int character;
        Node left;
        Node right;

        public int getFrequency()
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

        public void setFrequency(int freq)
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

    }
}
