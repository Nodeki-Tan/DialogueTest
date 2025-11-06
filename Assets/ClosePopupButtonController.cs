using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ClosePopupButtonController : MonoBehaviour
{
    public static void ClosePopUp()
    {
        if (PopUpManager.Singleton != null && PopUpManager.Singleton.Current != null)
        {
            PopUpManager.Singleton.ClosePopUp();
        }
    }

}