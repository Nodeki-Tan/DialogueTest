using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ClosePopupButtonController : MonoBehaviour
{
    public static void ClosePopUp()
    {
        if (PopUpManager.instance != null && PopUpManager.instance.Current != null)
        {
            PopUpManager.instance.ClosePopUp();
        }
    }

}