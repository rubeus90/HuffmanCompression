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
        public List<bool>[] dictionary = new List<bool>[256];

        private String namePlugin = "CompressionPlugin";
        public String PluginName {
            get{ return this.namePlugin; }
        }

        public bool Compress(ref Huffman.HuffmanData data) {
            byte[] inValue = data.uncompressedData;
            data.frequency = frequency(inValue);
            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());
            data.compressedData = storeContentToByteArray(inValue);
            data.sizeOfUncompressedData = inValue.Count()/2;
            return true;
        }

        public bool Decompress(ref Huffman.HuffmanData data) {
            Node treeTop = createBinaryTree(data.frequency);
            createDictionary(treeTop, new List<bool>());
            data.uncompressedData = decodeBitArray(new BitArray(data.sizeOfUncompressedData), treeTop);
            return true;
        }

        /*
         * Convert String to byte[]
         */
        static public byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /*
         * Calculate to frequency of each character
         */
        static public List<KeyValuePair<byte,int>> frequency(byte[] data) {
            int[] frequenceTable = new int[256];
            int i;

            for (i = 0; i < data.Length; i++) {
                frequenceTable[data[i]]++; 
            }

            List<KeyValuePair<byte, int>> list = new List<KeyValuePair<byte, int>>();

            for (i = 1; i < 256; i++) {
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
        public void createDictionary(Node node, List<bool> bools) {
            if (node.isLeaf()) {
                //Console.WriteLine("Clé du noeud :" + node.Key);
                dictionary[node.Key] =  bools;
            } else {
                //Console.WriteLine("Node de gauche :" + node.Left.Key + " " + node.Left.Value);
                //Console.WriteLine("Node de droite :" + node.Right.Key + " " + node.Right.Value);

                List<bool> bools_copy = new List<bool>(bools); // J'aimerais bien faire ça pour mon futur, on sait jamais, un accident c'est vite venu
                bools_copy.Add(true);
                bools.Add(false);

                createDictionary(node.Right, bools_copy);
                createDictionary(node.Left, bools);
            }
        }

        /*
         * Store the original content into a ByteArray 
         */
        public byte[] storeContentToByteArray(byte[] data) {
            List<bool> encoded = new List<bool>();
            int i;

            for (i = 0; i < data.Length; i += 2) {
                encoded.AddRange(dictionary[data[i]]);
            }

            BitArray bits = new BitArray(encoded.ToArray());

            if (bits.Length % 8 == 0) {
                i = bits.Length / 8;
            } else {
                i = bits.Length / 8 + 1;
            }

            byte[] ret = new byte[i];

            bits.CopyTo(ret, 0); // We have to do something about it !
            return ret;
        }

        /*
         * Decode the encoded BitArray to byte[]
         */
        static public byte[] decodeBitArray(BitArray encoded, Node treeTop) {
            List<byte> list = new List<byte>();
            Node node = treeTop;
            Node left, right;
            int length = encoded.Length;

            for (int i = 0; i < length; i++) {
                left = node.Left;
                right = node.Right;

                if (encoded[i]) {
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
                    list.Add(0);
                    node = treeTop;
                }
            }
            return list.ToArray();
        }
    }
}
