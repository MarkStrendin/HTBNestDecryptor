using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NestDecryptor
{
    /// <summary>
    /// Converted from found VB code on Nest.
    /// Converted to C# by https://converter.telerik.com/, because screw Visual Basic.
    /// Hard-coded everything is by design.
    /// </summary>
    class Decryptor
    {
        public string Decrypt(string cipherText, string passPhrase, string saltValue, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] cipherTextBytes;
            cipherTextBytes = Convert.FromBase64String(cipherText);

            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);

            byte[] keyBytes;
            keyBytes = password.GetBytes(System.Convert.ToInt32(keySize / (double)8));

            AesCryptoServiceProvider symmetricKey = new AesCryptoServiceProvider();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor;
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            System.IO.MemoryStream memoryStream;
            memoryStream = new System.IO.MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes;
            plainTextBytes = new byte[cipherTextBytes.Length + 1];

            int decryptedByteCount;
            decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string plainText;
            plainText = Encoding.ASCII.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }

        public string DecryptString(string EncryptedString)
        {
            if (string.IsNullOrEmpty(EncryptedString))
                return string.Empty;
            else
                return Decrypt(EncryptedString, "N3st22", "88552299", 2, "464R5DFA5DL6LE28", 256);
        }
    }
}
