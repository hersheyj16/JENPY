using System;
using System.Security.Cryptography;
using System.Text;

namespace JENPY.BlockChain
{
    public class Block
    {
        public string Hash;
        public string PrevHash;
        public string Data;
        public long Timestamp;

        public Block() { 
        }

        public Block(string Data)
        {
            this.Data = Data;
            int BlockStoreChainLastIdx = BlockStore.BlockChain.Count - 1;
            this.PrevHash = BlockStore.BlockChain[BlockStoreChainLastIdx].Hash;

            long Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this.Timestamp = Time;

            String ComputedHashInput = Data + PrevHash + Time;
            string ComputedHash = ComputedHashForString(ComputedHashInput);
            this.Hash = ComputedHash;

        }

        private string ComputedHashForString(string computedHashInput)
        {
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(computedHashInput));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static void Main(string[] args)
        {
            BlockStore bs = new BlockStore();

            //TODO these adding the new block to the chain needs to handled by BADD
            Block b = new Block("b");
            var ans = b.Hash;
            Console.WriteLine(ans);
            BlockStore.BlockChain.Add(b);
            Console.WriteLine(BlockStore.BlockChain.Count);

            Block c = new Block("c");
            var ans2 = c.Hash;
            Console.WriteLine(ans2);
            BlockStore.BlockChain.Add(c);
            Console.WriteLine(BlockStore.BlockChain.Count);

        }
    }
}
