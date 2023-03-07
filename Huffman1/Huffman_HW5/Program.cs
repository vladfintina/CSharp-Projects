using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Huffman_HW5
{

    internal class Program
    {
        private static void Main(string[] args)
        {

            int CHUNK_SIZE = 1024;

            if(args.Length != 1)
            {
                Console.WriteLine("Argument Error");

            }
            else
            {
                Huffman huffmanController = new Huffman();
                string inputFilename = args[0];
                try
                {
                    var fileName = args[0];

                    using (FileStream fs = new FileStream(args[0], FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] chunk;

                            chunk = br.ReadBytes(CHUNK_SIZE);
                            while (chunk.Length > 0)
                            {
                                for (int i = 0; i < chunk.Length; i++)
                                    huffmanController.dictionary[chunk[i]]++;
                                chunk = br.ReadBytes(CHUNK_SIZE);
                            }
                        }
                    }

                    Node root = huffmanController.HuffmanTree();
                    huffmanController.recursivePreorder(root);
                    string result = huffmanController.result;
                    result.Remove(result.Length - 1);
                    Console.WriteLine(huffmanController.result);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("File Error");
                }

                

            }

        }
    }
}