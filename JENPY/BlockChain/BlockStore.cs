using System;
using System.Collections.Generic;

namespace JENPY.BlockChain
{
    public class BlockStore
    {
        public static IList<Block> BlockChain { get; set; }

        public BlockStore()
        {
            BlockChain = new List<Block>();
            //TODO think of a better way to stroe the genesis block
            Block GenesisBlock = new Block();
            GenesisBlock.Data = "none";
            GenesisBlock.Hash = "none";
            GenesisBlock.PrevHash = "none";

            long Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            GenesisBlock.Timestamp = Time;
            BlockChain.Add(GenesisBlock);
        }
    }
}
