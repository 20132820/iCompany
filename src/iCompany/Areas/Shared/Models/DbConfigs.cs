using System;
using System.Security.Cryptography;
using System.Text;

namespace iCompany.Areas.Shared.Models
{
    public class DbConfigs
    {
        private static byte[] key = new byte[] { 80, 82, 84, 86, 88, 90, 91, 93, 95, 97, 99, 71, 73, 75, 77, 79 };

        public bool Encrypt { get; set; }
        public string Type { get; set; }
        public string ConnectionString { get; set; }

        private string decryptConnString;
        public string DecryptConnString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(decryptConnString))
                {
                    if (Encrypt)
                    {
                        using (var aes = Aes.Create())
                        {
                            using (var decryptor = aes.CreateDecryptor(key, key))
                            {
                                var enc = Convert.FromBase64String(ConnectionString);
                                var dec = new byte[enc.Length];
                                decryptor.TransformBlock(enc, 0, enc.Length, dec, 0);
                                decryptConnString = Encoding.UTF8.GetString(dec);

                            }
                        }
                    }
                    else
                    {
                        decryptConnString = ConnectionString;
                    }
                }
                return decryptConnString;
            }
        }

        public void EncryptString(string conn)
        {
            decryptConnString = conn;
            if (Encrypt)
            {
                using (var aes = Aes.Create())
                {
                    using (var encryptor = aes.CreateEncryptor(key, key))
                    {
                        var dec = Encoding.UTF8.GetBytes(decryptConnString);
                        var enc = new byte[dec.Length];
                        encryptor.TransformBlock(dec, 0, dec.Length, enc, 0);
                        ConnectionString = Convert.ToBase64String(enc);

                    }
                }
            }
            else
            {
                ConnectionString = decryptConnString;
            }
        }
    }
}
