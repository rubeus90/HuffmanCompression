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
        public List<byte>[] dictionary = new List<byte>[256];

        private String namePlugin = "CompressionPlugin";
        public String PluginName {
            get{ return this.namePlugin; }
        }

        public bool Compress(ref Huffman.HuffmanData data) {
            byte[] inValue = data.uncompressedData;
            data.frequency = frequency(inValue);

            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<byte>());

            data.compressedData = storeContentToByteArray(inValue);
            data.sizeOfUncompressedData = inValue.Count();

            return true;
        }

        public bool Decompress(ref Huffman.HuffmanData data) {
            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<byte>());
            data.uncompressedData = decodeBitArray(data.compressedData, treeTop);
            return true;
        }

        /*
         * Calculate to frequency of each character
         */
        static public List<KeyValuePair<byte,int>> frequency(byte[] data) {
            int[] frequenceTable = new int[256];
            int i, length = data.Length;

            for (i = 0; i < length; i++) {
                frequenceTable[data[i]]++; 
            }

            List<KeyValuePair<byte, int>> list = new List<KeyValuePair<byte, int>>();

            for (i = 0; i < 256; i++) {
                if (frequenceTable[i] != 0) {
                    list.Add(new KeyValuePair<byte, int>((byte)i, frequenceTable[i]));
                }
            }

            return list;
        }

        /*
         * Find the 2 nodes which have the 2 minimum values
         * Left is the minimum
         * Right is the second minimum
         */
        static public void findMinimum(List<Node> list, ref Node left,ref Node right) {
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
        static public Node createBinaryTree(List<KeyValuePair<byte, int>> data) {
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
        public void createDictionary(Node node, List<byte> bytes) {
            if (node.isLeaf()) {
                //Console.WriteLine("Clé du noeud :" + node.Key);
                dictionary[node.Key] =  bytes;
            } else {
                //Console.WriteLine("Node de gauche :" + node.Left.Key + " " + node.Left.Value);
                //Console.WriteLine("Node de droite :" + node.Right.Key + " " + node.Right.Value);

                List<byte> bytes_copy = new List<byte>(bytes);
                bytes_copy.Add(1);
                bytes.Add(0);

                createDictionary(node.Right, bytes_copy);
                createDictionary(node.Left, bytes);
            }
        }

        /*
         * Store the original content into a ByteArray 
         */
        public byte[] storeContentToByteArray(byte[] data) {
            List<byte> encoded = new List<byte>();
            int i, length = data.Length;

            for (i = 0; i < length; i++) {
                encoded.AddRange(dictionary[data[i]]);
            }

            byte[] ret = new byte[i];

            return encoded.ToArray();
        }

        /*
         * Decode the encoded BitArray to byte[]
         */
        static public byte[] decodeBitArray(byte[] encoded, Node treeTop) {
            List<byte> list = new List<byte>();
            Node node = treeTop;
            Node left, right;

            int length = encoded.Length, i;

            for (i = 0; i < length; i++) {
                left = node.Left;
                right = node.Right;

                if (encoded[i] == 1) {
                    if (right != null) {
                        node = right;
                    }
                }
                else {
                    if (left != null) {
                        node = left;
                    }
                }

                if (node.Left == null && node.Right == null) {
                    list.Add(node.Key);
                    node = treeTop;
                }
            }
            return list.ToArray();
        }
    }
}
