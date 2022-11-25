using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Helper
{
    public class HashHelper
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public string ConcatString { get; private set; }

        public HashHelper()
        {

        }

        public HashHelper(HashHelper hash)
        {

        }

        public string HashMigsTransactionReference(decimal amount, string merchantId, string orderId, string merchantAccessCode)
        {
            string hashInput = string.Concat(amount.ToString("0.00"), merchantId, orderId, merchantAccessCode);
            string hashedValue = MD5Hash(hashInput);
            return hashedValue;
        }

        public string PerformHash(string password)
        {
            Guid guid = Guid.NewGuid();

            Salt = guid.ToString();

            var passwordAndSaltBytes = Concat(password);
            return ComputeHash(passwordAndSaltBytes);
        }

        public string GetSalt()
        {
            Guid guid = Guid.NewGuid();

            Salt = guid.ToString();

            return Salt;
        }

        public string PerformHash(string salt, string password)
        {
            Salt = salt;
            var passwordAndSaltBytes = Concat(password);
            return ComputeHash(passwordAndSaltBytes);
        }

        public bool Verify(string salt, string hash, string password)
        {
            Salt = salt;
            var passwordAndSaltBytes = Concat(password);
            var hashAttempt = ComputeHash(passwordAndSaltBytes);
            return hash.ToLower() == hashAttempt.ToLower();
        }

        private string ComputeHash(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return HexHelper.ToHex(sha256.ComputeHash(bytes));
            }
        }

        private byte[] Concat(string password)
        {
            ConcatString = string.Concat(password, Salt);

            var passwordBytes = Encoding.UTF8.GetBytes(ConcatString);
            return passwordBytes;
        }

        public string GetMd5Hash(MD5 md5Hash, string input)
        {
            string salt = "s+(_a*";
            string ConcatString = string.Concat(input, salt);

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(ConcatString));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        public string GetMd5HashWitoutSalt(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        /// <summary>
        /// Encrypt the Given string with the Key provided and return the value
        /// </summary>
        /// <param name="md5Has">md5 Hash</param>
        /// <param name="toEncrypt">Token from Reddist to be encrypted and stored in to Cookie</param>
        /// <param name="securityKey">Key to encrypt</param>
        /// <returns>return encrypted value in string</returns>
        public string EncryptTokenUsingMd5(string toEncrypt, string securityKey)
        {
            byte[] data;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            MD5 md5Has = new MD5CryptoServiceProvider();

            data = md5Has.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            md5Has.Dispose();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = data;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Has"></param>
        /// <param name="toDecrypt"></param>
        /// <param name="securityKey"></param>
        /// <returns></returns>
        public string DecryptTokenUsingMd5(string toDecrypt, string securityKey)
        {
            byte[] data;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            MD5 md5Has = new MD5CryptoServiceProvider();

            data = md5Has.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
            md5Has.Dispose();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = data;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        // Verify a hash against a string. 
        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Created for performinig MD5 hash to MOLPay
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Hashed input</returns>
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
