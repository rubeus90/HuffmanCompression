using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompressionPlugin;
using System.Collections;

namespace ConsoleApplication {
    class Program {
        static void Main(string[] args) {
            MainClass classe = new MainClass();

            byte[] data = classe.GetBytes("abbccddd");
            List<KeyValuePair<byte, int>> frequencyTable = classe.frequency(data);
            Node treeTop = classe.createBinaryTree(frequencyTable);
            Dictionary<byte, List<bool>> dictionary = classe.createDictionary(frequencyTable, treeTop);
            BitArray compressedData = classe.storeContentToBitArray(dictionary, data);
        }
    }
}
