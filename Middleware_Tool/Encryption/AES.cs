using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Middleware_Tool
{
    public static class AES
    {
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_aeskey"></param>
        /// <returns></returns>
        public static string AESDecrypt(this string value, string _aeskey = "[23|*kjenHU~'e;]")
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                byte[] toEncryptArray = Convert.FromBase64String(value);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_aeskey"></param>
        /// <returns></returns>
        public static string AESEncrypt(this string value, string _aeskey = "[23|*kjenHU~'e;]")
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


    }
}
