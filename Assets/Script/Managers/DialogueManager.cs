using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Event = GameEvents.Event;

public class DialogueManager
{

    #region Singleton

    public static DialogueManager instance;

    public DialogueManager()
    {
        instance = this;

        Debug.Log("DialogueManager created");

        Init();
    }

    #endregion

    public float typeSpeed = 0.05f;
    public float changeAutoSentenceSpeed = 1f;
    public float changeAutoDialogSpeed = 3f;
    public int dialogueChildSelection = 0;


    private Queue<string> Strings;
    private Dialogue current;
    private TMP_Text dialogueText;
    private TMP_Text nameText;
    private Image characterSprite;

    public Dialogue Current { get => current; set => current = value; }
    public TMP_Text DialogueText { get => dialogueText; set => dialogueText = value; }

    public void Init()
    {
        Strings = new Queue<string>();


        Debug.Log("DialogueManager started");
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        Debug.Log("Starting dialogue " + _dialogue.name);

        dialogueText = GameObject.FindGameObjectWithTag("DialogueDisplay").GetComponent<TMP_Text>();

        nameText = GameObject.FindGameObjectWithTag("NameDisplay").GetComponent<TMP_Text>();
        nameText.SetText(_dialogue.name);

        characterSprite = GameObject.FindGameObjectWithTag("SpriteDisplay").GetComponent<Image>();
        characterSprite.sprite = _dialogue.character.sprites[(int)_dialogue.emote];

        if (_dialogue.triggerEvents != null) 
        {
            foreach (Event e in _dialogue.triggerEvents)
            {
                e.OnStateEnter();
            }
        }

        Strings.Clear();
        current = _dialogue;

        foreach (string _item in _dialogue.sentences)
        {
            Strings.Enqueue(_item);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Strings.Count == 0)
        {
            EndDialogue();

            return;
        }


        string _sentence = Strings.Dequeue();
        Debug.Log("sentence");

        dialogueText.StopAllCoroutines();
        dialogueText.StartCoroutine(TypeSentence(_sentence));

    }

    IEnumerator TypeSentence(string _sentence)
    {

        dialogueText.SetText("");

        foreach (char _letter in _sentence.ToCharArray())
        {
            dialogueText.text += _letter;

            yield return new WaitForSeconds(typeSpeed);
        }

        if (current.autoContinue)
        {
            dialogueText.StopAllCoroutines();
            dialogueText.StartCoroutine(NextDialogue(changeAutoSentenceSpeed));
        }

    }

    public IEnumerator NextDialogue(float _speed)
    {

        if (Strings.Count >= 1)
        {
            yield return new WaitForSeconds(_speed);

            DisplayNextSentence();
        }
        else
        {
            if (current.child.Length != 0)
            {
                yield return new WaitForSeconds(_speed);

                StartDialogue(current.child[current.choiceDialogue ? dialogueChildSelection : 0]);
            }
            else
            {
                EndDialogue();
            }
        }

    }

    void EndDialogue()
    {

        if (current.triggerEvents != null)
        {
            foreach (Event e in current.triggerEvents)
            {
                e.OnStateExit();
            }
        }

        if (current.child.Length != 0)
        {
            StartDialogue(current.child[current.choiceDialogue ? dialogueChildSelection : 0]);
        }
        else
        {
            dialogueText.text = "";
        }
    }

}