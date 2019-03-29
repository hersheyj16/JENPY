using System;
using System.Collections.Generic;
using JENPY.BlockChain;
using JENPY.Exceptions;
using JENPY.Utils;

namespace JENPY.Request
{
    public class BVERHandler : RequestHandler
    {
        public JenpyObject Handle(JenpyObject req)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            for(int i = 0; i < BlockStore.BlockChain.Count; i ++)
            {
                Block block = BlockStore.BlockChain[i];
                bool verified = verify(block, i);
                if (verified)
                {
                    data.Add(block.Hash, block.Data);
                }
                else
                {
                    //TODO : shall I throw RTE instead? would netcat handle it
                    //data.Add(block.Hash, block.Data);
                    string ErrorString = String.Format("data verification failed at block with data: {0}, hash {1}", block.Data, block.Hash);
                    throw new JenpyBlockVerifiabilityException(ErrorString);
                }
            }

            JenpyObject res = new JenpyObject(JenpyConstants.OK, data);
            return res;
        }

        private bool verify(Block block, int i)
        {
            Console.WriteLine("verifying block {0} in pos {1}", block, i);

            if (i == 0) {
                return true;
            }


            //Compare prev hash.
            Block PrevBlock = BlockStore.BlockChain[i - 1];
            Console.WriteLine("debug - prev block should be {0}", BlockStore.BlockChain[i - 1]);
            Console.WriteLine("debug - got prev block {0}", PrevBlock);

            var preBlock = BlockStore.BlockChain[i - 1];
            Console.WriteLine("debug - var prev {0}", preBlock);

            String expectedPrevHashInput = JenpyBlockUtils.ComputedHash(PrevBlock);

            if (block.PrevHash != expectedPrevHashInput) {
                return false;
            }

            return true;
        }
    }
}
