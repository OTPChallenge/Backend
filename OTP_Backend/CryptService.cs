using System.Security.Cryptography;

namespace OTP_Backend
{
    public class CryptService
    {

        public static byte[] Encrypt(byte[] plainBytes, byte[] key)
        {
            byte[] encryptedBytes = null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }

            return encryptedBytes;
        }

        public static byte[] Decrypt(byte[] cipherBytes, byte[] key)
        {
            byte[] decryptedBytes = null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                }
            }

            return decryptedBytes;
        }
    }
}
