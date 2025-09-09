// Services/DataEncryptionService.cs
using System.Security.Cryptography;
using System.Text;

public class DataEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public DataEncryptionService(IConfiguration configuration)
    {
        // IMPORTANT: Keys should be managed securely, e.g., via Azure Key Vault, not hardcoded.
        _key = Encoding.UTF8.GetBytes(configuration["Encryption:Key"]);
        _iv = Encoding.UTF8.GetBytes(configuration["Encryption:IV"]);
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}