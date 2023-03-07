using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Huffman_HW5
{

    internal class Program
    {

        public static void writeHeader(FileStream fs)
        {
            byte[] byteArray = new byte[8];
            byteArray[0] = 123;
            byteArray[1] = 104;
            byteArray[2] = 117;
            byteArray[3] = 124;
            byteArray[4] = 109;
            byteArray[5] = 125;
            byteArray[6] = 102;
            byteArray[7] = 102;

            fs.Write(byteArray);
        }
        public static void Main(string[] args)
        {

            int CHUNK_SIZE = 1024;

            if (args.Length != 1)
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
                            byte[] chunk= new byte[1024];
                            int nr = fs.Read(chunk, 0, CHUNK_SIZE);
                            while (nr > 0)
                            {
                                for (int i = 0; i < nr; i++)
                                    huffmanController.dictionary[(int)chunk[i]]++;
                                nr = fs.Read(chunk, 0, CHUNK_SIZE);
                            }             
                    }

                    string outputFile = args[0] + ".huff";
                    ///var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

                    using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    {
                        writeHeader(fileStream);
                        Node root = huffmanController.HuffmanTree();

                        string s = "";
                        huffmanController.dfsHuffman(root,s);
                        huffmanController.recursivePreorder(root, fileStream);

                        byte[] endOfTree = new byte[8];
                        for (int i = 0; i < 8; i++)
                            endOfTree[i] = 0; ;
                        fileStream.Write(endOfTree);
                        //string result = huffmanController.result;
                        //result.Remove(result.Length - 1);
                        //Console.WriteLine(huffmanController.result);
                        using (FileStream fs = new FileStream(args[0], FileMode.Open, FileAccess.Read))
                        {
                       
                                byte[] chunk2 = new byte[1024];
                                int nr = fs.Read(chunk2, 0, CHUNK_SIZE);
                                while (nr > 0)
                                {
                                    StringBuilder bytesToPrint = new StringBuilder();
                                    for (int i = 0; i < nr; i++)
                                    {
                                        int index = chunk2[i];
                                        bytesToPrint.Append(huffmanController.codes[index]);
                                    }
                                    huffmanController.printEncodedText(bytesToPrint, fileStream);
                                    nr = fs.Read(chunk2, 0, CHUNK_SIZE);
                                }

                                while (huffmanController.remainingCode.Length % 8 != 0)
                                {
                                    huffmanController.remainingCode.Append("0");
                                }

                                if (huffmanController.remainingCode.Length > 0)
                                    huffmanController.printLastByte(huffmanController.remainingCode, fileStream);
                        }
                    }
                    //fileStream.Close();
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("File Error");
                   
                }
            }
            
        }       
    }
}