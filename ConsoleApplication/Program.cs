using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompressionPlugin;
using System.Collections;
using System.Diagnostics;

namespace ConsoleApplication {
    class Program {
        static void Main(string[] args) {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            
            // Test
            checkMinimum();
            checkFrequency();
            checkTree();
            checkDictionnary();
            checkDecompress();
            checkFunction();

            /*byte[] data = classe.GetBytes("abbccddd");
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);
            Node treeTop = classe.createBinaryTree(frequencyTable);
            Dictionary<byte, List<bool>> dictionary = classe.createDictionary(frequencyTable, treeTop);
            BitArray compressedData = classe.storeContentToBitArray(dictionary, data);*/
        }

        static private void checkMinimum(){

            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 5 });
            listNode.Add(new Node { Key = 1, Value = 8 });         
            listNode.Add(new Node { Key = 2, Value = 15 });
            listNode.Add(new Node { Key = 3, Value = 2 });
            listNode.Add(new Node { Key = 4, Value = 7 });
            listNode.Add(new Node { Key = 5, Value = 2 });
            listNode.Add(new Node { Key = 6, Value = 3 });

            Node left = listNode[0];
            Node right = listNode[1];
            new CompressionPlugin.CompressionPlugin().findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 2 && right.Value == 2, "Echec méthode findMinimum");
        }

        static private void checkFrequency() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = classe.GetBytes("abbcccdddd"); // One a, two b, three c and 4 d, so How many e ? Yes 0 ! 
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data); // Counts and sorts data

            KeyValuePair<byte, int> pair = frequencyTable.Find(x => x.Key == 97); //Check a frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 98); //Check b frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 99); //Check c frequency
            Debug.Assert(pair.Value == 3, "Echec de la méthode frequency");

            pair = frequencyTable.Find(x => x.Key == 100); //Check d frequency
            Debug.Assert(pair.Value == 4, "Echec de la méthode frequency");
        }

        static private void checkTree() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(1, 9));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 15));
            listPair.Add(new KeyValuePair<byte, int>(4, 3));
            listPair.Add(new KeyValuePair<byte, int>(5, 5));
            listPair.Add(new KeyValuePair<byte, int>(6, 4));
            listPair.Add(new KeyValuePair<byte, int>(7, 13));
            listPair.Add(new KeyValuePair<byte, int>(8, 11));
            listPair.Add(new KeyValuePair<byte, int>(9, 1));

            Node treeTop = new CompressionPlugin.CompressionPlugin().createBinaryTree(listPair);
            int valueTotal = 0;
            for (int i = 0; i < listPair.Count; i++) {
                valueTotal += listPair[i].Value;
            }
            Console.WriteLine("Calculer : "+ valueTotal);
            Console.WriteLine("Retour de l'arbre :" + treeTop.Value);
            Debug.Assert(valueTotal == treeTop.Value, "Erreur dans la méthode createBinaryTree");
        }

        static private void checkDictionnary() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(102, 6));
            listPair.Add(new KeyValuePair<byte, int>(97, 10));
            listPair.Add(new KeyValuePair<byte, int>(98, 10));
            listPair.Add(new KeyValuePair<byte, int>(100, 16));
            listPair.Add(new KeyValuePair<byte, int>(99, 25));
            listPair.Add(new KeyValuePair<byte, int>(101, 36));

            Node treeTop = classe.createBinaryTree(listPair);

            classe.createDictionary(treeTop, new List<bool>());
            Dictionary<byte, List<bool>> dictionary = classe.dictionary;
        }

        static private void checkDecompress() {
            bool[] bools = new bool[10];
            bools[3] = true;
            bools[5] = true;
            bools[6] = true;
            bools[7] = true;
            bools[8] = true;
            bools[9] = true;
            BitArray bits = new BitArray(bools);
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = classe.GetBytes("abbcccc");
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);
            Node treeTop = classe.createBinaryTree(frequencyTable);
            classe.createDictionary(treeTop, new List<bool>());
            Dictionary<byte, List<bool>> dictionary = classe.dictionary;
            byte[] result = classe.decodeBitArray(bits, treeTop);
        }

        static private void checkFunction() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] bytes = classe.GetBytes("abcdefghijklmnopq");
            Huffman.HuffmanData data = new Huffman.HuffmanData();
            data.uncompressedData = bytes;
            classe.Compress(ref data);
            classe.Decompress(ref data);
        }
    }
}
