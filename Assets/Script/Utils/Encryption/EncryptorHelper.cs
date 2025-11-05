using System.Security.Cryptography;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


/// <summary> This class helps with the fucntions provided in the encryptor class </summary>
public static class EncryptorHelper
{

    /// <summary> Makes a new AES Key and returns it as a string. </summary>
    public static string GetNewKey()
    {
        // Creates a new AES and then a new Key to use
        Aes AES = Aes.Create();
        AES.GenerateKey();

        string Key = Encryptor.GetString(AES.Key);

#if UNITY_EDITOR
        Debug.Log("Generated key is [ " + Key + " ]");
#else
            Console.WriteLine("Generated key is [ " + Key + " ]");
#endif

        return Key;
    }

    /// <summary> Checks if a certain file has been saved already. </summary>
    /// <param name="path">The Path of the file, such as 'Albert'.</param>
    public static bool Exists(string path)
    {
        string dataPath = path;
        return File.Exists(dataPath);
    }


    /// <summary> Deletes a save file if it exists. </summary>
    /// <param name="path">The Path of the file, such as 'Albert'.</param>
    /// <returns></returns>
    public static void DeleteData(string path)
    {
        string dataPath = path;

        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("The given save file '" + path + "' does not exist.");
#else
                Console.WriteLine("The given save file '" + path + "' does not exist.");
#endif
        }

    }

    /// <summary> Saves Data into encrypted memory. </summary>
    /// <param name="data">The Data to save, such as 'Albert', or 0, 1.5, false, and so on.</param>
    /// <param name="path">The Path to save Data into, such as 'Player Data/Albert'.</param>
    public static void Save(string path, string data)
    { Encryptor.Save(data, path); }

    /// <summary> Returns a Data object from encrypted memory, if it exists. </summary>
    /// <param name="path">The Path to load Data from, such as 'Player Data/Albert'.</param>
    /// <returns></returns>
    public static string Load(string path)
    { return Encryptor.Load(path); }

}