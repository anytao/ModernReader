// 2012 OKr Works, http://okr.me

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace OKr.Common.Utils
{
    public class MD5
    {
        public static string Hash(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm("MD5");
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);

            if (hashed.Length != alg.HashLength)
            {
                throw new System.Exception("HashAlgorithmProvider failed to generate a hash of proper length!\n");
            }

            return CryptographicBuffer.EncodeToHexString(hashed);
        }
    }
}
