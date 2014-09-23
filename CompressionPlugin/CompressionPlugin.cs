using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman;
using System.Collections;

namespace CompressionPlugin
{
    public class CompressionPlugin : MarshalByRefObject, IPlugin{
        public Dictionary<byte,String> dictionary = new Dictionary<byte, String>();

        private String namePlugin = "CompressionPlugin";
        public String PluginName {
            get{ return this.namePlugin; }
        }

        public bool Compress(ref Huffman.HuffmanData data) {
            /*byte[] inValue = data.uncompressedData;
            data.frequency = frequency(inValue);
            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());
            BitArray bits = storeContentToBitArray(inValue);
            data.compressedData = BitArrayToByteArray(bits); */
            return true;
        }

        public bool Decompress(ref Huffman.HuffmanData data) {
            /*Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());
            data.uncompressedData = decodeBitArray(new BitArray(data.compressedData), treeTop);*/
            return true;
        }

        /*
         * Convert String to byte[]
         */
        public byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /*
         * Convert byte[] to String
         */
        public string GetString(byte[] bytes) {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /*
         * Calculate to frequency of each character
         */
        public List<KeyValuePair<byte,int>> frequency(byte[] data) {
            Dictionary<byte,int> frequenceTable = new Dictionary<byte,int>();

            for (int i = 0; i < data.Length; i++) {
                if (!frequenceTable.ContainsKey(data[i])) {
                    if (data[i] != 0) {
                        frequenceTable.Add(data[i], 1);
                    }                    
                }
                else {
                    frequenceTable[data[i]]++;
                }
            }
            return frequenceTable.ToList();
        }

        /*
         * Find the 2 nodes which have the 2 minimum values
         * Left is the minimum
         * Right is the second minimum
         */
        public void findMinimum(List<Node> list, ref Node left,ref Node right) {
            for (int i = 1; i < list.Count; i++){
                if (list[i].Value <= right.Value){
                    if (list[i].Value <= left.Value){
                        right = left;
                        left = list[i];
                    }else{
                        right = list[i];
                    }
                }     
            }
        }

        /*
         * Create the Huffman tree from the frequency table
         */
        public Node createBinaryTree(List<KeyValuePair<byte, int>> data) {
            List<Node> listNode = new List<Node>();

            foreach (KeyValuePair<byte, int> pair in data) {
                listNode.Add(new Node { Key = pair.Key, Value = pair.Value });
            }

            while(listNode.Count > 1) {
                Node left = listNode[0];
                Node right = listNode[1];            
                findMinimum(listNode,ref left, ref right);  
    
                Node parent = new Node {Value = left.Value + right.Value, Left = left, Right = right };

                listNode.Add(parent);
                listNode.Remove(left);
                listNode.Remove(right);
            } 
            return listNode[0];
        }

        /*
         * Get through the Huffman tree to establish the binary code for each letter
         */
        public void createDictionary(Node node, String list) {
            //Console.WriteLine("noed actuel : " + node.Key + " " + node.Value);

            if (node.isLeaf()) {
                //Console.WriteLine("Clé du noeud :" + node.Key);
                dictionary.Add(node.Key, list);
            }

            else {
               // Console.WriteLine("Node de gauche :" + node.Left.Key + " " + node.Left.Value);
                //Console.WriteLine("Node de droite :" + node.Right.Key + " " + node.Right.Value);

                createDictionary(node.Right, list + 1);
                createDictionary(node.Left, list + 0);
            }
        }

        /*
         * Store the original content into a BitArray 
         */
        /*public BitArray storeContentToBitArray(byte[] data) {
            List<bool> encoded = new List<bool>();
            for (int i = 0; i < data.Length; i=i+2) {
                encoded.AddRange(dictionary[data[i]]);
            }
            return new BitArray(encoded.ToArray());
        }*/

        /*
         * Decode the encoded BitArray to byte[]
         */
        public byte[] decodeBitArray(BitArray encoded, Node treeTop) {
            List<byte> list = new List<byte>();
            Node node = treeTop;

            for (int i = 0; i < encoded.Length; i++) {
                if (encoded[i]) {
                    if (node.Right != null) {
                        node = node.Right;
                    }
                }
                else {
                    if (node.Left != null) {
                        node = node.Left;
                    }
                }
                if (node.isLeaf()) {
                    list.Add(node.Key);
                    node = treeTop;
                }
            }
            return list.ToArray();
        }

        public byte[] BitArrayToByteArray(BitArray bits){
            byte[] ret;
            if (bits.Length % 8 == 0) {
                ret = new byte[bits.Length / 8];
            }
            else {
                ret = new byte[bits.Length / 8 + 1];
            }
            
            bits.CopyTo(ret, 0);
            return ret;
        }
    }
}
