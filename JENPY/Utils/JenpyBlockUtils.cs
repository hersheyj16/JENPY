using System;
using System.Security.Cryptography;
using System.Text;
using JENPY.BlockChain;

namespace JENPY.Utils
{
    public class JenpyBlockUtils
    {

        public static string ComputedHash(Block block)
        {
            String ComputedHashInput = block.Data + block.PrevHash + block.Timestamp;
            var Hash = ComputedHashForString(ComputedHashInput);
            Console.WriteLine("debug input hash{0}, hash is {1}", ComputedHashInput, Hash);
            return Hash;
        }

        public static string ComputedHashForString(string computedHashInput)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(computedHashInput));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
