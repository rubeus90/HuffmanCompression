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
            MainClass classe = new MainClass();
            
            // Test
            checkMinimum();

            /*byte[] data = classe.GetBytes("abbccddd");
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);
            Node treeTop = classe.createBinaryTree(frequencyTable);
            Dictionary<byte, List<bool>> dictionary = classe.createDictionary(frequencyTable, treeTop);
            BitArray compressedData = classe.storeContentToBitArray(dictionary, data);*/
        }

        static private void checkMinimum(){

            // Create and intiate a List<Node>
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
            new MainClass().findMinimum(listNode, ref left, ref right);

            Debug.Assert(left.Value == 2 && right.Value == 3, "Echec de la méthode findMinimum");
        }
    }
}
