using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ENETCare.IMS.Users
{
    public class CryptoPassword
    {
        private const uint SALT_LENGTH = 32;

        private byte[] hash, salt;

        public CryptoPassword(byte[] hash, byte[] salt)
        {
            this.hash = hash;
            this.salt = salt;
        }

        public CryptoPassword(string plainText)
        {
            this.salt = GenerateSalt();
            this.hash = GenerateSaltedHash(plainText, salt);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[SALT_LENGTH];
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(salt);
            return salt;
        }

        public static byte[] GenerateSaltedHash(string plainText, byte[] salt)
        {
            HashAlgorithm hasher = new SHA256Managed();

            // Get the password as an array of UTF32 bytes
            byte[] plaintextBytes = Encoding.UTF32.GetBytes(plainText);

            // Append the password to the array to be hashed
            byte[] passAndSalt = new byte[plaintextBytes.Length + salt.Length];
            int i;
            for (i = 0; i < plaintextBytes.Length; i++)
                passAndSalt[i] = plaintextBytes[i];

            // Append the salt to the array to be hashed
            for (int j = 0; j < passAndSalt.Length; j++, i++)
                passAndSalt[i] = salt[j];

            // Hash the new password-salt pair
            return hasher.ComputeHash(passAndSalt);
        }
    }
}
