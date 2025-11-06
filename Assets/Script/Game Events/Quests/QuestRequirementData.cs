using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/RequirementData")]
public class QuestRequirementData : ScriptableObject
{

    public string requirementName;
    public string correctValue;

    public bool IsRequirementMet()
    {
        return QuestManager.Singleton.CheckQuestData<string>(requirementName, correctValue);
    }

}