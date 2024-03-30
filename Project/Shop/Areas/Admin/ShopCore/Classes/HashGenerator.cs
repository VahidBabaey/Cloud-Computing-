using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopCore.Classes
{
   public class HashGenerator
    {
        public static string MD5EndCoding(string password)
        {
            byte[] MainByte;
            byte[] EncodeByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            MainByte = ASCIIEncoding.Default.GetBytes(password);
            EncodeByte = md5.ComputeHash(MainByte);
            return BitConverter.ToString(EncodeByte);

        }
    }
}
