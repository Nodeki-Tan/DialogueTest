using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class IOUtils
{

    // here ill put helper functions that doesnt need its own class
    // such as basic file loading and saving, or "buffer" making

    const string SEPARATOR = "#_#";

    // Change this to select either standalone C# program or Unity versions

#if UNITY_EDITOR
    static string DATA_PATH = Application.dataPath + "/Data/";
    #elif UNITY_ANDROID && !UNITY_EDITOR
        static string DATA_PATH = Application.persistentDataPath + "/Data/";         
    #else
        static string DATA_PATH = Directory.GetCurrentDirectory() + "/Data/";
    #endif

    static string GENERAL_PATH = DATA_PATH + "Saves/";

    const string GENERAL_EXTENSION = "Data";
    const string SETTINGS_EXTENSION = "config";
    const string TEXT_EXTENSION = "txt";

    public static bool USE_ENCRYPTION = true;

    public static object[] ConstructObjectBuffer(params object[] list)
    {

        return list;

    }

    public static string GetArgumentValue(Dictionary<string, string> args, string arg)
    {

        return args.TryGetValue(arg, out var _val) ? _val.ToString() : "NULL";

    }

    public static bool GetArgumentValueBool(Dictionary<string, string> args, string arg, bool defaultVal = false)
    {

        return args.TryGetValue(arg, out var _val) ? bool.Parse(_val) : defaultVal;

    }

    public static int GetArgumentValueInt(Dictionary<string, string> args, string arg)
    {

        return args.TryGetValue(arg, out var _val) ? int.Parse(_val) : 0;

    }

    public static float GetArgumentValueFloat(Dictionary<string, string> args, string arg)
    {

        return args.TryGetValue(arg, out var _val) ? float.Parse(_val) : 0.0f;

    }

    public static Vector3 GetArgumentVector3(Dictionary<string, string> args, string arg)
    {

        return args.TryGetValue(arg, out var _val) ? StringToVector3(_val) : Vector3.zero;

    }

    public static Vector2 GetArgumentVector2(Dictionary<string, string> args, string arg)
    {

        return args.TryGetValue(arg, out var _val) ? StringToVector2(_val) : Vector2.zero;

    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public static Vector2 StringToVector2(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector2
        Vector2 result = new Vector2(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]));

        return result;
    }

    // creates a universal purpose save file, its generic, for now uses Dat format
    public static void CreateDataFile(Dictionary<string, string> contents, string saveName, string saveFormat, string subfolder)
    {

        if (!Directory.Exists(DATA_PATH + subfolder))
        {
            Directory.CreateDirectory(DATA_PATH + subfolder);
        }

        DirectoryInfo d = new DirectoryInfo(DATA_PATH + subfolder);

        //Console.WriteLine("saving " + saveName + " to file...");

#if UNITY_EDITOR
        Debug.Log("saving " + saveName + " to file...");
#else
            Console.WriteLine("saving " + saveName + " to file...");
#endif

        string value = string.Join(SEPARATOR, contents.Select(x => x.Key + "=" + x.Value).ToArray());

        string path = DATA_PATH + subfolder + saveName + "." + saveFormat;


        // We decide if use encryption or not to save
        if (USE_ENCRYPTION)
            EncryptorHelper.Save(path, value);
        else
            File.WriteAllBytes(path, Encoding.UTF8.GetBytes(value));


        //Console.WriteLine("saved " + saveName + " successfull!");

#if UNITY_EDITOR
        Debug.Log("saved " + saveName + " successfull!");
#else
            Console.WriteLine("saved " + saveName + " successfull!");
#endif

    }

    public static string[] LoadDataFile(string saveName, string saveFormat, string subfolder)
    {

        if (!Directory.Exists(DATA_PATH + subfolder))
        {
            Directory.CreateDirectory(DATA_PATH + subfolder);
        }

        DirectoryInfo d = new DirectoryInfo(DATA_PATH + subfolder);

        //Console.WriteLine("loading " + saveName + " from file...");

#if UNITY_EDITOR
        Debug.Log("loading " + saveName + " from file...");
#else
            Console.WriteLine("loading " + saveName + " from file...");
#endif

        string buffer = "";

        string path = DATA_PATH + subfolder + saveName + "." + saveFormat;

        if (File.Exists(path))
        {
            // We decide if use encryption or not to load
            if (USE_ENCRYPTION)
                buffer = EncryptorHelper.Load(path);
            else
                buffer = Encoding.UTF8.GetString(File.ReadAllBytes(path));

        }
        else
        {
            //Console.WriteLine("loading " + saveName + " failed because file doesnt exist!");

#if UNITY_EDITOR
            Debug.Log("loading " + saveName + " failed because file doesnt exist!");
#else
                Console.WriteLine("loading " + saveName + " failed because file doesnt exist!");
#endif

            return null;
        }

        string[] obj = buffer.Split(SEPARATOR);

        //Console.WriteLine("loaded contents of " + saveName + " printing contents...");

#if UNITY_EDITOR
        Debug.Log("loaded contents of " + saveName + " printing contents...");
#else
            Console.WriteLine("loaded contents of " + saveName + " printing contents...");
#endif

        for (int i = 0; i < obj.Length; i++)
        {
            //Console.WriteLine(obj[i].ToString());

#if UNITY_EDITOR
            Debug.Log(obj[i].ToString());
#else
                Console.WriteLine(obj[i].ToString());
#endif
        }

        //Console.WriteLine("those are the contents of " + saveName + " successfully loaded!");

#if UNITY_EDITOR
        Debug.Log("those are the contents of " + saveName + " successfully loaded!");
#else
            Console.WriteLine("those are the contents of " + saveName + " successfully loaded!");
#endif

        return obj;

    }

}