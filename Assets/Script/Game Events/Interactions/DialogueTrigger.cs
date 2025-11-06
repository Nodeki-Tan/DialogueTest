using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interaction
{
    public Dialogue dialogue;

    void Start()
    {
        DoSomething();
    }

    public override void DoSomething()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Singleton.StartDialogue(dialogue);
    }
}
