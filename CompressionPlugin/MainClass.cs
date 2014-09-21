using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman;
using System.Collections;

namespace CompressionPlugin
{
    public class MainClass : MarshalByRefObject, IPlugin{
        private String namePlugin = "CompressionPlugin";
        public String PluginName {
            get{ return this.namePlugin; }
        }

        public bool Compress(ref Huffman.HuffmanData data) {

            return true;
        }

        public bool Decompress(ref Huffman.HuffmanData data) {
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
            return frequenceTable.OrderBy(x => x.Value).ToList();
        }

        /*
         * Find the 2 nodes which have the 2 minimum values
         * Left is the minimum
         * Right is the second minimum
         */
        public void findMinimum(List<Node> list, ref Node left,ref Node right) {
            for (int i = 0; i < list.Count; i++){
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
            //Etape 0
            List<Node> listNode = new List<Node>();

            // Maybe do something about this foreach because foreach sucks
            foreach (KeyValuePair<byte, int> pair in data) {
                listNode.Add(new Node { Key = pair.Key, Value = pair.Value });
            }

            while(listNode.Count > 1) {
                Node left = listNode[0];
                Node right = listNode[1];            
                findMinimum(listNode,ref left, ref right);  
    
                Node parent = new Node { Value = left.Value + right.Value, Left = left, Right = right };

                listNode.Add(parent);

                listNode.Remove(listNode.Find(x => x.Key==left.Key));
                listNode.Remove(listNode.Find(x => x.Key==right.Key));
            } 
            return listNode[0];
        }

        /*
         * Get through the Huffman tree to establish the binary code for each letter
         */
        public List<bool> readTree(byte key, Node node, List<bool> list) {
            if (node.Left == null && node.Right == null) {
                if (node.Key.Equals(key) && list.Count != 0) {
                    return list;
                }
                else if (node.Key.Equals(key) && list.Count == 0) {
                    list.Add(false);
                    return list;
                }
            }
            else {
                List<bool> listLeft = new List<bool>(list);
                listLeft.Add(false);
                List<bool> listRight = new List<bool>(list);
                listRight.Add(true);

                listLeft = readTree(key, node.Left, listLeft);
                listRight = readTree(key, node.Right, listRight);

                if (listLeft != null) {
                    return listLeft;
                }
                else {
                    return listRight;
                }
            }
            return null;
        }

        /*
         * Create the dictionay: each letter corresponds to a binary code (a list of booleans)
         */
        public Dictionary<byte,List<bool>> createDictionary(List<KeyValuePair<byte,int>> frequency, Node node) {
            Dictionary<byte, List<bool>> dictionary = new Dictionary<byte, List<bool>>();

            foreach (KeyValuePair<byte, int> pair in frequency) {
                dictionary.Add(pair.Key, readTree(pair.Key, node, new List<bool>()));
            }

            return dictionary;
        }

        /*
         * Store the original content into a BitArray 
         */
        public BitArray storeContentToBitArray(Dictionary<byte,List<bool>> dictionary, byte[] data) {
            List<bool> encoded = new List<bool>();
            for (int i = 0; i < data.Length; i=i+2) {
                encoded.AddRange(dictionary[data[i]]);
            }
            return new BitArray(encoded.ToArray());
        }

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[bits.Length / 8];
            bits.CopyTo(ret, 0);
            return ret;
        }
    }
}
