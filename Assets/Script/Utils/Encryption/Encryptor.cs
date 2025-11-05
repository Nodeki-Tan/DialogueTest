using System.Data.SqlTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

/// <summary> The class that provides encryption. </summary>
public static class Encryptor
{
    private static readonly string keyString = "30 192 34 149 21 46 249 203 233 24 21 152 226 218 169 215 104 43 18 180 104 19 12 20 37 3 7 223 58 70 222 98";
    private static readonly byte[] key = GetBytes(keyString);
    private static Aes aes = Aes.Create();
    

    // Taken from https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aescryptoserviceprovider?view=netframework-4.7.2
    public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new System.ArgumentNullException(nameof(plainText));
        if (Key == null || Key.Length <= 0)
            throw new System.ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new System.ArgumentNullException(nameof(IV));
        byte[] encrypted;

        // Create an AesCryptoServiceProvider object
        // with the specified key and IV.
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

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
    
    public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new System.ArgumentNullException(nameof(cipherText));
        if (Key == null || Key.Length <= 0)
            throw new System.ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new System.ArgumentNullException(nameof(IV));

        // Declare the string used to hold
        // the decrypted text.
        string plaintext;

        // Create an AesCryptoServiceProvider object
        // with the specified key and IV.
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

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

    public static byte[] GetBytes(string pData)
    {
        string[] encrypted = pData.Split(char.Parse(" "));
        byte[] bytes = new byte[encrypted.Length];
        int len = encrypted.Length;

        for (int i = 0; i < len; ++i)
        { bytes[i] = byte.Parse(encrypted[i]); }
        return bytes;
    }

    public static string GetString(byte[] pData)
    {
        string str = "";
        int len = pData.Length;
        for (int i = 0; i < len; ++i)
        {
            str += "" + pData[i];
            if (i < len - 1) str += " ";
        }
        return str;
    }

    /// <summary> Saves Data into encrypted memory. </summary>
    /// <param name="Data">The Data to save as a long string.</param>
    /// <param name="Path">The Path to save Data into.</param>
    public static void Save(string Data, string Path)
    { File.WriteAllBytes(Path, EncryptStringToBytes_Aes(Data, aes.Key, aes.IV)); }

    /// <summary> Returns a Data object from encrypted memory, if it exists. </summary>
    /// <param name="Path">The Path to load Data.</param>
    /// <returns></returns>
    public static string Load(string Path)
    { return DecryptStringFromBytes_Aes(File.ReadAllBytes(Path), aes.Key, aes.IV); }

}
