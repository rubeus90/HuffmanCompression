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
            
            // Test Frequency
            checkFrequency1();
            checkFrequency2();
            checkFrequency3();

            // Test Minimum
            checkMinimum1();
            checkMinimum2();
            checkMinimum3();
            checkMinimum4();

            // test Tree
            checkTree1();
            checkTree2();

            // test Dictionnary
            checkDictionnary();

            // test compress
            checkCompress("So let me feel, let me feel, Let me breathe you without a sound It's the only thing I'm waking up for, up for now"); // <-- C'est ici qu'on s'amuse !
        }

        /***************************************************************************************************************/

        static private void checkFrequency1() {
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

        static private void checkFrequency2() {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = classe.GetBytes("hey thibault!");
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);

            KeyValuePair<byte, int> pair = frequencyTable.Find(x => x.Key == 104); //Check h frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency : h");

            pair = frequencyTable.Find(x => x.Key == 101); //Check e frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : e");

            pair = frequencyTable.Find(x => x.Key == 121); //Check y frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : y");

            pair = frequencyTable.Find(x => x.Key == 32); //Check space frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency : space");

            pair = frequencyTable.Find(x => x.Key == 116); //Check t frequency
            Debug.Assert(pair.Value == 2, "Echec de la méthode frequency :t");

            pair = frequencyTable.Find(x => x.Key == 105); //Check i frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :i");

            pair = frequencyTable.Find(x => x.Key == 98); //Check b frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :b");

            pair = frequencyTable.Find(x => x.Key == 97); //Check a frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :a");

            pair = frequencyTable.Find(x => x.Key == 117); //Check u frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :u");

            pair = frequencyTable.Find(x => x.Key == 108); //Check l frequency
            Debug.Assert(pair.Value == 1, "Echec de la méthode frequency :l");
        }

        static private void checkFrequency3() {
            String yourString = "Si toi aussi tu t'amuse à faire du C# le mardi après-midi, tape dans tes mains !";
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            byte[] data = classe.GetBytes(yourString);
            int lengh = yourString.Count();
            int totalFrequency = 0;
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);
            foreach (KeyValuePair<byte, int> pair in frequencyTable) {
                // Console.WriteLine("Key :"+pair.Key + "Pair :" + pair.Value);
                totalFrequency += pair.Value;
            }
            // Console.WriteLine(totalFrequency);
            // Console.WriteLine(lengh);
            Debug.Assert(lengh == totalFrequency, "Echec de la methode frequency");
        }

        /***************************************************************************************************************/

        static private void checkMinimum1() {

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

        static private void checkMinimum2() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 1 });
            listNode.Add(new Node { Key = 1, Value = 2 });
            listNode.Add(new Node { Key = 2, Value = 4 });
            listNode.Add(new Node { Key = 3, Value = 3 });
            listNode.Add(new Node { Key = 4, Value = 4 });
            listNode.Add(new Node { Key = 5, Value = 6 });
            listNode.Add(new Node { Key = 6, Value = 8 });

            Node left = listNode[0];
            Node right = listNode[1];
            new CompressionPlugin.CompressionPlugin().findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 2, "Echec méthode findMinimum");
        }

        static private void checkMinimum3() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 2 });
            listNode.Add(new Node { Key = 1, Value = 1 });
            listNode.Add(new Node { Key = 2, Value = 1 });
            listNode.Add(new Node { Key = 3, Value = 4 });
            listNode.Add(new Node { Key = 4, Value = 4 });
            listNode.Add(new Node { Key = 5, Value = 6 });
            listNode.Add(new Node { Key = 6, Value = 8 });

            Node left = listNode[0];
            Node right = listNode[1];
            new CompressionPlugin.CompressionPlugin().findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 1, "Echec méthode findMinimum");
        }

        static private void checkMinimum4() {
            // Create and intiate an awesome List<Node>
            List<Node> listNode = new List<Node>();
            listNode.Add(new Node { Key = 0, Value = 5 });
            listNode.Add(new Node { Key = 1, Value = 5 });
            listNode.Add(new Node { Key = 2, Value = 5 });
            listNode.Add(new Node { Key = 3, Value = 5 });
            listNode.Add(new Node { Key = 4, Value = 5 });
            listNode.Add(new Node { Key = 5, Value = 1 });
            listNode.Add(new Node { Key = 6, Value = 1 });

            Node left = listNode[0];
            Node right = listNode[1];
            new CompressionPlugin.CompressionPlugin().findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 1 && right.Value == 1, "Echec méthode findMinimum");
        }

        /***************************************************************************************************************/

        static private void checkTree1() {

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

            Debug.Assert(valueTotal == treeTop.Value, "Erreur dans la méthode createBinaryTree");
        }

        static private void checkTree2() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            Node treeTop = new CompressionPlugin.CompressionPlugin().createBinaryTree(listPair);
            Debug.Assert(treeTop.Left.Left.Key == 4 && treeTop.Left.Right.Key == 3 && treeTop.Right.Right.Left.Key == 2 && treeTop.Right.Right.Right.Key == 1 && treeTop.Right.Left.Key == 0, "Erreur dans la méthode createBinaryTree");
        }


        /***************************************************************************************************************/

        static private void checkDictionnary() {

            // Create and intiate an awesome List<KeyValuePair>
            List<KeyValuePair<byte, int>> listPair = new List<KeyValuePair<byte, int>>();
            listPair.Add(new KeyValuePair<byte, int>(0, 5));
            listPair.Add(new KeyValuePair<byte, int>(1, 5));
            listPair.Add(new KeyValuePair<byte, int>(2, 5));
            listPair.Add(new KeyValuePair<byte, int>(3, 5));
            listPair.Add(new KeyValuePair<byte, int>(4, 5));

            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();
            Node treeTop = classe.createBinaryTree(listPair);

            classe.createDictionary(treeTop, new List<bool>()); // Something wrong with this

            Dictionary<byte, List<bool>> dictionary = classe.dictionary;
            Debug.Assert(dictionary[1][0] && dictionary[1][1] && dictionary[1][2] , "Erreur dans la création du dico"); // Check for 0
            Debug.Assert(dictionary[0][0] && !dictionary[0][1], "Erreur dans la création du dico");
            Debug.Assert(dictionary[2][0] && dictionary[2][1] && !dictionary[2][2], "Erreur dans la création du dico");
            Debug.Assert(!dictionary[4][0] && !dictionary[4][1], "Erreur dans la création du dico");
            Debug.Assert(!dictionary[3][0] && dictionary[3][1], "Erreur dans la création du dico");
        }


        /***************************************************************************************************************/

        static private void checkCompress(String pString) {
            CompressionPlugin.CompressionPlugin classe = new CompressionPlugin.CompressionPlugin();

            byte[] data = classe.GetBytes(pString);
            List<KeyValuePair<byte, int>> frequency = classe.frequency(data);
            Node treeTop = classe.createBinaryTree(frequency);
            classe.createDictionary(treeTop, new List<bool>());
            BitArray bits = classe.storeContentToBitArray(data);
            byte[] compressedData = classe.BitArrayToByteArray(bits);
            int sizeofCompressData = data.Count() / 2; // Histoire des 0 au milieu ...
            
            /***/
            classe.dictionary.Clear();

            Node treeTop2 = classe.createBinaryTree(frequency);
            classe.createDictionary(treeTop2, new List<bool>());
            byte[] dataDecompressed = classe.decodeBitArray(new BitArray(compressedData), treeTop);

            for (int i = 0, j=0; i < data.Count(); i+=2, j++) {
                Debug.Assert(data[i] == dataDecompressed[j], "Erreur dans la tout le programme, courage et écoute de la House/Big Room/Trance !");
            }
        }

        /*static private void checkDecompress() {
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
        }*/

        /***************************************************************************************************************/
    }
}
