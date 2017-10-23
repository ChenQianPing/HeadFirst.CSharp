using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.EncryptionHelper
{
    public class AesCrypt
    {
        #region Constant
        // 编译时常量用关键字const来定义；
        public const string RetError = "x07x07x07x07x07";
        private const string CryptoKey = "ADVANCEDENCRYPTIONSTANDARD";

        // 运行时常量用关键字readonly来定义；
        public readonly int CryptoKeyLength = 32;
        #endregion

        #region Field
        private readonly byte[] _iv =
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90,
            0xAB, 0xCD, 0xEF
        };

        private byte[] _key =
        {
            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab,
            0x76, 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, 0xb7,
            0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7, 0x23,
            0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b,
            0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1,
            0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45,
            0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda,
            0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64,
            0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b,
            0xdb, 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7,
            0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, 0xba, 0x78, 0x25,
            0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e, 0xb5, 0x66, 0x48,
            0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e,
            0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41,
            0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16
        };


        private readonly AesCryptoServiceProvider _mAesCryptoServiceProvider;
        #endregion

        #region Property
        public string Message { get; set; }

        /// <summary>
        /// True：密文中包含密钥
        /// False：密文中不包含密钥
        /// </summary>
        public bool ContainKey { get; set; }
        #endregion

        #region Ctor
        public AesCrypt()
        {
            _mAesCryptoServiceProvider = new AesCryptoServiceProvider();
            ContainKey = true;
            Message = string.Empty;
        }

        public AesCrypt(bool containKey)
            : this()
        {
            ContainKey = containKey;
        }
        #endregion

        #region 指定密钥对明文进行AES加密、解密
        /// <summary>
        /// 指定密钥对明文进行AES加密
        /// </summary>
        /// <param name="sCrypto">明文</param>
        /// <param name="sKey">加密密钥</param>
        /// <returns></returns>
        public string Encrypt(string sCrypto, string sKey)
        {
            var key = new byte[CryptoKeyLength];

            var temp = String2Byte(sKey);
            if (temp.Length > key.Length)
            {
                Message = "Key too long,need less than 32 Bytes key.";
                return RetError;
            }
            key = String2Byte(sKey.PadRight(key.Length));
            return Encrypt(sCrypto, key);
        }

        /// <summary>
        /// 指定密钥，并对密文进行解密
        /// </summary>
        /// <param name="sEncrypted">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public string Decrypt(string sEncrypted, string sKey)
        {
            var key = new byte[CryptoKeyLength];
            var temp = String2Byte(sKey);

            if (temp.Length > key.Length)
            {
                Message = "Key invalid.too long,need less than 32 Bytes";
                return RetError;
            }

            key = String2Byte(sKey.PadRight(key.Length));
            if (ContainKey)
            {
                sEncrypted = sEncrypted.Substring(CryptoKeyLength * 2);
            }
            return Decrypt(sEncrypted, key);
        }
        #endregion

        #region 动态生成密钥，并对明文进行AES加密、解密
        /// <summary>
        /// 动态生成密钥，并对明文进行AES加密
        /// </summary>
        /// <param name="sCrypto">明文</param>
        /// <returns></returns>
        public string Encrypt(string sCrypto)
        {
            _mAesCryptoServiceProvider.GenerateKey();
            var key = _mAesCryptoServiceProvider.Key;
            return Encrypt(sCrypto, key);
        }

        /// <summary>
        /// 从密文中解析出密钥，并对密文进行解密
        /// </summary>
        /// <param name="sEncrypted">密文</param>
        /// <returns></returns>
        public string Decrypt(string sEncrypted)
        {
            var sKey = string.Empty;

            if (sEncrypted.Length <= CryptoKeyLength * 2)
            {
                Message = "Encrypted string invalid.";
                return RetError;
            }

            if (ContainKey)
            {
                sKey = sEncrypted.Substring(0, CryptoKeyLength * 2);
                sEncrypted = sEncrypted.Substring(CryptoKeyLength * 2);
            }
            var key = HexString2Byte(sKey);
            return Decrypt(sEncrypted, key);
        }
        #endregion

        #region 私有方法

        private string Encrypt(string sCrypto, byte[] key)
        {
            var sEncryped = string.Empty;

            try
            {
                var crypto = String2Byte(sCrypto);
                _mAesCryptoServiceProvider.Key = key;
                _mAesCryptoServiceProvider.IV = _iv;
                var ct = _mAesCryptoServiceProvider.CreateEncryptor();
                var encrypted = ct.TransformFinalBlock(crypto, 0, crypto.Length);
                if (ContainKey)
                {
                    sEncryped += Byte2HexString(key);
                }
                sEncryped += Byte2HexString(encrypted);
                return sEncryped;
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
                return RetError;
            }
        }

        private string Decrypt(string sEncrypted, byte[] key)
        {
            var sDecrypted = string.Empty;

            try
            {
                var encrypted = HexString2Byte(sEncrypted);
                _mAesCryptoServiceProvider.Key = key;
                _mAesCryptoServiceProvider.IV = _iv;
                var ct = _mAesCryptoServiceProvider.CreateDecryptor();
                var decrypted = ct.TransformFinalBlock(encrypted, 0, encrypted.Length);
                sDecrypted += Byte2String(decrypted);
                return sDecrypted;
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
                Message = "Decrypt fail.";
                return RetError;
            }
        }

        protected virtual string Byte2HexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        protected virtual byte[] HexString2Byte(string hex)
        {
            var len = hex.Length / 2;
            var bytes = new byte[len];
            for (var i = 0; i < len; i++)
            {
                bytes[i] = (byte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16));
            }
            return bytes;
        }

        protected virtual byte[] String2Byte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        protected virtual string Byte2String(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region Test Method

        /*
         * encryptStr:
         *  30373130202020202020202020202020202020202020202020202020202020209EC03
            06B959064551E026A2A740A1B0A

            decryptStr:Bobby

            dynamicEncryptStr:
            D9AE84538EF4653A30FF6E22B4E914A6318D9B00E8C3B1AA3819E87EB12F4D
            4949BC12F6D2E544B4B959D0B3070BE88A

            dynamicDecryptStr:Bobby
         */
        public void TestAesCrypt()
        {
            // 指定密钥对明文进行AES加密
            var encryptStr = Encrypt("Bobby", "0710");
            Console.WriteLine("encryptStr:" + encryptStr);

            // 指定密钥对明文进行AES加密
            var decryptStr = Decrypt(encryptStr, "0710");
            Console.WriteLine("decryptStr:" + decryptStr);

            // 动态生成密钥，并对明文进行AES加密
            var dynamicEncryptStr = Encrypt("Bobby");
            Console.WriteLine("dynamicEncryptStr:" + dynamicEncryptStr);

            // 从密文中解析出密钥，并对密文进行解密
            var dynamicDecryptStr = Decrypt(dynamicEncryptStr);
            Console.WriteLine("dynamicDecryptStr:" + dynamicDecryptStr);

            Console.ReadLine();
        }
        #endregion

    }
}

/*
 * AES
 * 高级加密标准（英语：Advanced Encryption Standard，缩写：AES），
 * 在密码学中又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。
 * 这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。
 * 
 * AES先进加密算法是一向被认为牢不可破的加密算法，
 * 针对这项加密算法的攻击是异常复杂的，
 * 事实上想要完全破解AES花费的时间要以数十亿年计，极大的保证了数据的安全性。
 * 
 * 这里有两个加密、解密方法：
 *  一种是带密钥的加密；
 *  一种是动态加密，就是不需要密钥，密钥被动态生成并且保存在密文中，
 *  解密时先解密密钥，在解密密文。
 */
