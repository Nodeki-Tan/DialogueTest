using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Launcher class for the server program
public class FileManager
{
            
    #region Singleton

        private static FileManager _singleton = new();
        public static FileManager Singleton
        {
            get => _singleton;
            private set
            {
                if (_singleton == null){
                    _singleton = value;
                    _singleton.Init();
                }
                else if (_singleton != value)
                {
                    Debug.Log($"{nameof(FileManager)} instance already exists, destroying duplicate!");
                }
            }
        }

    public FileManager()
    {
        Singleton = this;
    }

    #endregion

    public void Init()
    {
        Debug.Log($"{nameof(FileManager)} created");
    }

}


public struct dataCollection
{
    public Dictionary<string, string> data;
    public string name;
    public string format;
    public string subfolder;

    public dataCollection( Dictionary<string, string> _data, string _name = "data", string _format = "Data", string _subfolder = "Saves/")
    {
        data = _data;
        name = _name;
        format = _format;
        subfolder = _subfolder;
    }

    public void LoadFile(){
        ProcessFile(IOUtils.LoadDataFile(name, format, subfolder));
    }    

    public void ProcessFile(string[] args){

        data = new Dictionary<string, string>();

        foreach (string argument in args)
        {
            string[] splitted = argument.Split('=');

            if (splitted.Length == 2)
            {
                data[splitted[0]] = splitted[1];
            }
        }  
        
    }

     public void SaveFile() 
    {
        IOUtils.CreateDataFile(data, name, format, subfolder);
    }

    public void SaveVariable(string variableName, string value) 
    {
        data[variableName] = value;
    }

    public int TryGetInt(string variableName, int defaultval = 0) 
    {
        string val = IOUtils.GetArgumentValue(data, variableName);
        int returnVal;

        if (val == "NULL")  
            return defaultval;

        returnVal = int.TryParse(val,  out int _val) ? _val :  defaultval; 

        return returnVal;
    }

    public bool TryGetBool(string variableName, bool defaultval = false) 
    {
        string val = IOUtils.GetArgumentValue(data, variableName);
        bool returnVal;

        if (val == "NULL")  
            return defaultval;

        returnVal = bool.TryParse(val,  out bool _val) ? _val :  defaultval; 

        return returnVal;
    }

    public float TryGetFloat(string variableName, float defaultval = 0.0f) 
    {
        string val = IOUtils.GetArgumentValue(data, variableName);
        float returnVal;

        if (val == "NULL")  
            return defaultval;

        returnVal = float.TryParse(val,  out float _val) ? _val :  defaultval; 

        return returnVal;
    }

    public string TryGetString(string variableName, string defaultval = "Default") 
    {
        string val = IOUtils.GetArgumentValue(data, variableName);
        string returnVal;

        if (val == "NULL")  
            return defaultval;

        returnVal = val; 

        return returnVal;
    }

    public T TryGetValue<T>(string variableName, T defaultval = default) 
    {
        string val = IOUtils.GetArgumentValue(data, variableName);
        T returnVal;

        if (val == "NULL")  
            return defaultval;

        returnVal = GetValue<T>(val);

        return returnVal;
    }    

    public T GetValue<T>(string value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
    }


}