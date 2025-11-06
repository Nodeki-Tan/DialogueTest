using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* This object updates the inventory UI. */

public class QuestUI : MonoBehaviour
{

    public Transform activeQuestsParent;   // The parent object of all the items
    public Transform completedQuestsParent;   // The parent object of all the items
    public Transform questInfoParent;

    public GameObject questUI;  // The entire UI
    public GameObject questDisplayPrefab; // The recipe button prefab

    QuestButton[] activeSlots;  // List of all the slots
    QuestButton[] completedSlots;  // List of all the slots

    void Start()
    {
        questUI.SetActive(false);

        QuestManager.Singleton.onQuestChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback
        QuestManager.Singleton.onQuestInfoCallback += ShowCaseQuestInfo;    // Subscribe to the onItemChanged callback
        PopulateSlots();

    }

    void Update()
    {
        // Check to see if we should open/close the inventory
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var displayText = questInfoParent.GetComponentInChildren<TMP_Text>();
            displayText.text = "";

            questUI.SetActive(!questUI.activeSelf);
        }
    }

    void PopulateSlots() 
    {
        var displayText = questInfoParent.GetComponentInChildren<TMP_Text>();
        displayText.text = "";

        foreach (Transform child in activeQuestsParent) { Destroy(child.gameObject); }

        foreach (Transform child in completedQuestsParent) { Destroy(child.gameObject); }

        // Populate our slots array

        for (int i = 0; i < QuestManager.Singleton.activeQuests.Count; i++)
        {
            var questButton = UnityEngine.Object.Instantiate(questDisplayPrefab, activeQuestsParent); // Fixed CS0176 by qualifying with 'Object'

            questButton.GetComponent<QuestButton>().quest = QuestManager.Singleton.activeQuests[i];
        }

        activeSlots = activeQuestsParent.GetComponentsInChildren<QuestButton>();


        // Populate our slots array

        for (int i = 0; i < QuestManager.Singleton.completedQuests.Count; i++)
        {
            var questButton = UnityEngine.Object.Instantiate(questDisplayPrefab, completedQuestsParent); // Fixed CS0176 by qualifying with 'Object'

            questButton.GetComponent<QuestButton>().quest = QuestManager.Singleton.completedQuests[i];
        }

        completedSlots = completedQuestsParent.GetComponentsInChildren<QuestButton>();

    }    

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI(Quest quest, bool completed)
    {
        PopulateSlots();
    }

    public void ShowCaseQuestInfo(Quest quest)
    {
        var displayText = questInfoParent.GetComponentInChildren<TMP_Text>();

        if (displayText != null)
        {
            displayText.text = quest.name;
            displayText.text += Environment.NewLine + quest.description;

            displayText.text += Environment.NewLine + "Requires:" + Environment.NewLine;

            for (int i = 0; i < quest.requirements.Length; i++)
            {

                displayText.text +=
                    Environment.NewLine + quest.requirements[i].requirementName +
                    "   " + quest.requirements[i].TryGetRequirementValue() +
                    Environment.NewLine + quest.requirements[i].requirementDescription +
                    Environment.NewLine;

            }
        }
    }

}
 