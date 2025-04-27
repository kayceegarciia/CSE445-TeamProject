using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SecurityTools
{
    public class CryptoUtils
    {
        private static readonly string aesKey = "ThisIsA32ByteneftKeyForAES123456"; // 32 chars = 256 bits
        private static readonly string aesIV = "ThisIsA16neftIV!"; // 16 chars = 128 bits

        // SHA-256 Hashing: Converts a plaintext password into a secure hash string
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert hash bytes to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString(); // Return hash as lowercase hex string
            }
        }

        //AES Encryption: Encrypts a string using AES-256 with a fixed key and IV
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(aesKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(aesIV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Flush();
                    csEncrypt.FlushFinalBlock();

                    // Return encrypted string as Base64
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // AES Decryption: Decrypts a Base64-encoded AES-encrypted string
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(aesKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(aesIV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] buffer = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Return decrypted original text
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
