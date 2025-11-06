using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NextButtonController : MonoBehaviour
{
    public static void NextDialogueButton()
    {
        if (DialogueManager.Singleton != null && DialogueManager.Singleton.Current != null)
        {
            DialogueManager.Singleton.DialogueText.StopAllCoroutines();
            DialogueManager.Singleton.DialogueText.StartCoroutine(DialogueManager.Singleton.NextDialogue(0.1f));
        }
    }

}