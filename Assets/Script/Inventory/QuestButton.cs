using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Sits on all CraftButtons. */

public class QuestButton : MonoBehaviour
{

    public Image icon;          // Reference to the Icon image
    public TMP_Text text; // Reference to the craft name text

    public Quest quest;  // Current item in the slot

    void Start()
    {
        // Initialize the UI
        //icon.sprite = quest.icon;
        text.text = quest.name;


    }

    // Called when the remove button is pressed
    public void OnQuestButton()
    {
        if (QuestManager.Singleton.onQuestInfoCallback!= null)
            QuestManager.Singleton.onQuestInfoCallback.Invoke(quest);
    }



}