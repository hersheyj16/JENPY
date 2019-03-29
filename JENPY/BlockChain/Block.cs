using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using JENPY.Utils;

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
            if (BlockStoreChainLastIdx >= 0)
            {
                this.PrevHash = BlockStore.BlockChain[BlockStoreChainLastIdx].Hash;
            }
            else {
                this.PrevHash = "genesis-no-prev-hash";
            }

            this.Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this.Hash = JenpyBlockUtils.ComputedHash(this);
        }


        public override string ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
                result.AppendFormat("{0}={1} ", propertyInfo.Name, propertyInfo.GetValue(this));
            return result.ToString();
        }
    }
}
