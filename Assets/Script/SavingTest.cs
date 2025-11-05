using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Launcher class for the server program
public class SavingTest : MonoBehaviour
{

    public void Start()
    {
        dataCollection data = new dataCollection(new(),
        "myData", "save", "MyData/saves/data/");


        // Test variables
        int testInt = 1717;
        bool testBool = true;
        float testFloat = 0.1999f;
        string testString = "hello world!";

        // How to save values
        data.SaveVariable("test", "0000");
        data.SaveVariable("testInt", testInt.ToString());
        data.SaveVariable("testBool", testBool.ToString());
        data.SaveVariable("testFloat", testFloat.ToString());
        data.SaveVariable("testString", testString);

        data.SaveVariable("testInt", (999).ToString());

        // Save and load file
        data.SaveFile();        
        data.LoadFile();

        

        // Dummy test for "non existant values", will print the default value!
        Debug.Log(data.TryGetValue<string>("tesfadawdat2", "0"));

        // How to get values
        Debug.Log(data.TryGetValue  <string>  ("testString")    );
        Debug.Log(data.TryGetValue  <float>   ("testFloat")     );
        Debug.Log(data.TryGetValue  <bool>    ("testBool")      );
        Debug.Log(data.TryGetValue  <int>     ("testInt")      );

        // End of program
    }

}