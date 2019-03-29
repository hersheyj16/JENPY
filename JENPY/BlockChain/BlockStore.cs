using System;
using System.Collections.Generic;

namespace JENPY.BlockChain
{
    public class BlockStore
    {
        public static IList<Block> BlockChain { get; set; }

        static BlockStore()
        {
            BlockChain = new List<Block>();
            //TODO think of a better way to stroe the genesis block
            Block GenesisBlock = new Block("genesis");

            long Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            BlockChain.Add(GenesisBlock);
        }
    }
}
