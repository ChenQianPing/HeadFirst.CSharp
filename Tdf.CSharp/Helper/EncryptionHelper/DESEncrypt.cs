using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.EncryptionHelper
{
    public class DesEncrypt
    {

        /// <summary>
        /// 随机生成KEY
        /// </summary>
        /// <returns></returns>
        public string GenerateKey()
        {
            const int len = 8;
            var random = new Random(DateTime.Now.Millisecond);
            var keybyte = new byte[len];
            for (var i = 0; i < len; i++)
            {
                keybyte[i] = (byte)random.Next(65, 122);
            }
            return Encoding.ASCII.GetString(keybyte);
        }

        /// <summary>
        /// DES 加密过程
        /// </summary>
        /// <param name="dataToEncrypt">待加密数据</param>
        /// <param name="desKey"></param>
        /// <returns></returns>
        public string Encrypt(string dataToEncrypt, string desKey)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                // 把字符串放到byte数组中
                var inputByteArray = Encoding.Default.GetBytes(dataToEncrypt);

                // 建立加密对象的密钥和偏移量
                des.Key = Encoding.ASCII.GetBytes(desKey); 
                des.IV = Encoding.ASCII.GetBytes(desKey);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        var ret = new StringBuilder();
                        foreach (var b in ms.ToArray())
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// DES 解密过程
        /// </summary>
        /// <param name="dataToDecrypt">待解密数据</param>
        /// <param name="desKey"></param>
        /// <returns></returns>
        public string Decrypt(string dataToDecrypt, string desKey)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                var inputByteArray = new byte[dataToDecrypt.Length / 2];
                for (var x = 0; x < dataToDecrypt.Length / 2; x++)
                {
                    var i = (Convert.ToInt32(dataToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                // 建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = Encoding.ASCII.GetBytes(desKey); 
                des.IV = Encoding.ASCII.GetBytes(desKey);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        return System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
        }

        #region Test Method

        public void TestDesEncrypt()
        {
            /*
             * desKey:jfAfSxnB
             * encryptStr:154d18973f826bba
             * decryptStr:Bobby
             */
            var desKey = GenerateKey();
            Console.WriteLine("desKey:" + desKey);
            
            var encryptStr = Encrypt("Bobby", desKey);
            Console.WriteLine("encryptStr:" + encryptStr);

            var decryptStr = Decrypt(encryptStr, desKey);
            Console.WriteLine("decryptStr:" + decryptStr);
        }
        #endregion

    }
}


/*
 * 
 * 数据加密标准（DES，Data Encryption Standard）是一种对称加密算法，
 * 很可能是使用最广泛的密钥系统，特别是在保护金融数据的安全中，
 * 是安全性比较高的一种算法，目前只有一种方法可以破解该算法，那就是穷举法。
 * 
 * 在2001年，DES作为一个标准已经被高级加密标准（AES）所取代。
 * 另外，DES已经不再作为国家标准科技协会（前国家标准局）的一个标准。
 * 在某些文献中，作为算法的DES被称为DEA（Data Encryption Algorithm，数据加密算法），以与作为标准的DES区分开来
 */
