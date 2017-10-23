using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.EncryptionHelper
{
    /// <summary> 
    /// RSA加密解密及RSA签名和验证
    /// </summary> 
    public class RsaCryption
    {
        public RsaCryption() { }

        #region RSA 加密解密 

        #region RSA 的密钥产生 
        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RsaKey(out string xmlKeys, out string xmlPublicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region RSA的加密函数 

        // ############################################################################## 
        // RSA 方式加密 
        // 说明KEY必须是XML的行式,返回的是字符串 
        // 在有一点需要说明！！该加密方式有 长度 限制的！！ 
        // ############################################################################## 
        // RSA的加密函数  string
        public string RSAEncrypt(string xmlPublicKey, string mStrEncryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            var plainTextBArray = (new UnicodeEncoding()).GetBytes(mStrEncryptString);
            var cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            var result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }

        // RSA的加密函数 byte[]
        public string RSAEncrypt(string xmlPublicKey, byte[] encryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            var cypherTextBArray = rsa.Encrypt(encryptString, false);
            var result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }
        #endregion

        #region RSA的解密函数 

        // RSA的解密函数  string
        public string RSADecrypt(string xmlPrivateKey, string mStrDecryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            var plainTextBArray = Convert.FromBase64String(mStrDecryptString);
            var dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            var result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;
        }

        // RSA的解密函数  byte
        public string RSADecrypt(string xmlPrivateKey, byte[] decryptString)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            var dypherTextBArray = rsa.Decrypt(decryptString, false);
            var result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;
        }
        #endregion

        #endregion

        #region RSA数字签名 

        #region 获取Hash描述表 
        // 获取Hash描述表 
        public bool GetHash(string mStrSource, ref byte[] hashData)
        {
            // 从字符串中取得Hash描述 
            var md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            var buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            if (md5 != null) hashData = md5.ComputeHash(buffer);

            return true;
        }

        // 获取Hash描述表 
        public bool GetHash(string mStrSource, ref string strHashData)
        {
            if (strHashData == null) throw new ArgumentNullException(nameof(strHashData));

            // 从字符串中取得Hash描述 
            var md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            var buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            if (md5 != null)
            {
                var hashData = md5.ComputeHash(buffer);
                strHashData = Convert.ToBase64String(hashData);
            }
            return true;

        }

        // 获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] hashData)
        {
            if (hashData == null) throw new ArgumentNullException(nameof(hashData));

            // 从文件中取得Hash描述 
            var md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            if (md5 != null) hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        // 获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            if (strHashData == null) throw new ArgumentNullException(nameof(strHashData));

            // 从文件中取得Hash描述 
            var md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            if (md5 != null)
            {
                var hashData = md5.ComputeHash(objFile);
                objFile.Close();

                strHashData = Convert.ToBase64String(hashData);
            }

            return true;

        }
        #endregion

        #region RSA签名 
        // RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref byte[] encryptedSignatureData)
        {
            if (encryptedSignatureData == null) throw new ArgumentNullException(nameof(encryptedSignatureData));

            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            // 设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            // 执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;
        }

        // RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            if (mStrEncryptedSignatureData == null) throw new ArgumentNullException(nameof(mStrEncryptedSignatureData));

            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            // 设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            // 执行签名 
            var encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;

        }

        // RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref byte[] encryptedSignatureData)
        {
            if (encryptedSignatureData == null) throw new ArgumentNullException(nameof(encryptedSignatureData));

            var hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            // 设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            // 执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;

        }

        // RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            if (mStrEncryptedSignatureData == null) throw new ArgumentNullException(nameof(mStrEncryptedSignatureData));

            var hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            // 设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            // 执行签名 
            var encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;

        }
        #endregion

        #region RSA 签名验证 

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, byte[] deformatterData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            // 指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, byte[] deformatterData)
        {
            var hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            // 指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, string pStrDeformatterData)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            // 指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            var deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, string pStrDeformatterData)
        {
            var hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            // 指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            var deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion

        #endregion

        #region Test Method
        public void TestRsaCryption()
        {
            // RSA 的密钥产生 产生私钥 和公钥
            var xmlPrivateKeys = "";
            var xmlPublicKey = "";
            RsaKey(out xmlPrivateKeys, out xmlPublicKey);

            Console.WriteLine("xmlPrivateKeys:" + xmlPrivateKeys);
            Console.WriteLine("xmlPublicKey:" + xmlPublicKey);

            // 公钥加密
            var rsaEncryptStr = RSAEncrypt(xmlPublicKey, "Bobby");
            Console.WriteLine("rsaEncryptStr:" + rsaEncryptStr);

            // 私钥解密
            var rsaDecryptStr = RSADecrypt(xmlPrivateKeys, rsaEncryptStr);
            Console.WriteLine("rsaDecryptStr:" + rsaDecryptStr);

            // 获取Hash描述表
            var strHashData = "";
            var mStrSource = "Data to Sign!Data to Sign!Data to Sign!";
            GetHash(mStrSource, ref strHashData);

            // QXe45q0J3pX9miFW8HoDgg==
            Console.WriteLine("strHashData:" + strHashData);

            /*
             * RSA签名，对数据签名；
             * 1、设置签名的算法为MD5；
             * 2、私钥签名 xmlPrivateKeys
             */
            var pStrKeyPrivate = xmlPrivateKeys;
            var mStrHashbyteSignature = strHashData;
            var mStrEncryptedSignatureData = "";

            SignatureFormatter(pStrKeyPrivate, mStrHashbyteSignature, ref mStrEncryptedSignatureData);

            /*
             * mStrEncryptedSignatureData:
             *  vG29pjv7sQRyA6+XTSmMniRQOzLmMoDAUK+FW83gey7x6ulAjHAPL
                9hWxCA3DKvvP40CBORXoSXUnwv/Ii/Dx866U/ySbAE8DeBdH6oqk3PGaCjak78gIR7g3KFI8HuaT2lPd
                0W/PtlnZG/Y8lJDLZwpnk/fCszDshxz7ZdpXoA=
             */
            Console.WriteLine("mStrEncryptedSignatureData:" + mStrEncryptedSignatureData);

            // RSA 签名验证
            var pStrKeyPublic = xmlPublicKey;
            var pStrHashbyteDeformatter = strHashData;
            var pStrDeformatterData = mStrEncryptedSignatureData;

            /* 
             * RSA 签名验证，验证签名
             * 1、公钥 xmlPublicKey
             * 2、指定解密的时候HASH算法为MD5 
             */
            if (SignatureDeformatter(pStrKeyPublic, pStrHashbyteDeformatter, pStrDeformatterData))
            {
                Console.WriteLine("验证签名OK.");
            }
            else
            {
                Console.WriteLine("签名不匹配!");
            }

        }
        #endregion

    }
}


/*
 * RSA是第一个既能用于数据加密也能用于数字签名的算法。
 * 它易于理解和操作，也很流行。
 * 算法的名字以发明者的名字命名：Ron Rivest, Adi Shamir 和Leonard Adleman。
 * 
 * 但RSA的安全性一直未能得到理论上的证明。
 * 它经历了各种攻击，至今未被完全攻破。
 * 今天只有短的RSA钥匙才可能被强力方式解破。
 * 
 * 到2008年为止，世界上还没有任何可靠的攻击RSA算法的方式。
 * 只要其钥匙的长度足够长，用RSA加密的信息实际上是不能被解破的。
 * 但在分布式计算和量子计算机理论日趋成熟的今天，RSA加密安全性受到了挑战。
 */
