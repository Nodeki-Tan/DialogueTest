using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NextButtonController : MonoBehaviour
{
    public static void NextDialogueButton()
    {
        if (DialogueManager.instance != null && DialogueManager.instance.Current != null)
        {
            DialogueManager.instance.DialogueText.StopAllCoroutines();
            DialogueManager.instance.DialogueText.StartCoroutine(DialogueManager.instance.NextDialogue(0.1f));
        }
    }

}
