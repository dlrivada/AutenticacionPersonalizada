using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutenticacionPersonalizada.Utilities
{
    public static class SeguridadUtilities
    {
        public static string GetSha1(string texto)
        {
            SHA1 sha = SHA1.Create();
            UTF8Encoding encoding = new UTF8Encoding();
            StringBuilder builder = new StringBuilder();
            byte[] datos = sha.ComputeHash(encoding.GetBytes(texto));
            foreach (byte t in datos)
                builder.AppendFormat("{0:x2}", t);
            return builder.ToString();
        }

        public static string Cifrar(string contenido, string clave)
        {
            try
            {
                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization 
                // vector (IV).
                byte[] encrypted;
                using (RijndaelManaged cripto = new RijndaelManaged())
                {
                    cripto.Key = Convert.FromBase64String(clave);

                    cripto.IV = Convert.FromBase64String(ConfigurationManager.AppSettings["miSalt"]);
                    // Encrypt the string to an array of bytes.
                    encrypted = EncryptStringToBytes(contenido, cripto.Key, cripto.IV);
                }
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }

        }

        public static string Descifrar(string contenido, string clave)
        {
            try
            {
                string descifrado;
                using (RijndaelManaged cripto = new RijndaelManaged())
                {
                    cripto.Key = Convert.FromBase64String(clave);

                    cripto.IV = Convert.FromBase64String(ConfigurationManager.AppSettings["miSalt"]); 

                    descifrado = DecryptStringFromBytes(Convert.FromBase64String(contenido), cripto.Key, cripto.IV);
                }
                return descifrado;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}

