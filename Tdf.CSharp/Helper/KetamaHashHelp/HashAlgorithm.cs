using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.KetamaHashHelp
{
    public  class HashAlgorithm
    {
        public static long Hash(byte[] digest, int nTime)
        {
            var rv = ((long) (digest[3 + nTime*4] & 0xFF) << 24)
                     | ((long) (digest[2 + nTime*4] & 0xFF) << 16)
                     | ((long) (digest[1 + nTime*4] & 0xFF) << 8)
                     | ((long) digest[0 + nTime*4] & 0xFF);

            return rv & 0xffffffffL; /* Truncate to 32-bits */
        }

        /**
         * Get the md5 of the given key.
         */
        public static byte[] ComputeMd5(string k)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            var keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(k));
            md5.Clear();
            return keyBytes;
        }
    }
}
