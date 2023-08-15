using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Encryption
{
    public static class EncryptionUtilities
    {
        private const int Keysize = 128;

        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            var saltStringBytes = GenerateRandomEntropy();
            var ivStringBytes = GenerateRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.BlockSize = Keysize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.Zeros;
                    using (var key = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, key, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes);
                                cryptoStream.FlushFinalBlock();
                                var cryptTextBytes = saltStringBytes;
                                cryptTextBytes = cryptTextBytes.Concat(ivStringBytes).ToArray();
                                cryptTextBytes = cryptTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cryptTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cryptText, string passPhrase)
        {
            var cryptTextBytesWithSaltAndIv = Convert.FromBase64String(cryptText);
            var saltStringBytes = cryptTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cryptTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cryptTextBytes = cryptTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cryptTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.BlockSize = Keysize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.Zeros;
                    using (var key = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cryptTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, key, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cryptTextBytes.Length];

                                cryptoStream.Read(plainTextBytes);

                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes).TrimEnd('\0');
                            }
                        }
                    }
                }
            }
        }

        public static byte[] GenerateRandomEntropy()
        {
            var randomBytes = new byte[Keysize / 8];
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
