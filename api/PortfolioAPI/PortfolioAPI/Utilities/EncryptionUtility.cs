using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PortfolioAPI.Utilities
{
    public static class EncryptionUtility
    {
        private const string KEY = "cc9939b1e6de8a1bbc3e5556612a5562";
        private const string IV = "700cb79dcd5c44b7";

        public static byte[] Encrypt(string plainText)
        {
            byte[] encrypted;
            using (var aes = Aes.Create())
            {
                var key = GetKey();
                aes.Key = Encoding.ASCII.GetBytes(key.Key);
                aes.IV = Encoding.ASCII.GetBytes(key.Value);

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        encrypted = ms.ToArray();
                    }
                }

            }

            return encrypted;
        }

        public static string Decrypt(byte[] encryptedText)
        {
            string plainText = null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.ASCII.GetBytes(KEY);
                aes.IV = Encoding.ASCII.GetBytes(IV);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }

        private static string GetRandomIV()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 16);
        }

        private static KeyValuePair<string, string> GetKey()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var settings = builder.Build().GetSection("Api").Get<AppSettings>();

            return new KeyValuePair<string, string>(settings.Encryption.Key, settings.Encryption.Iv);
        }
    }
}
