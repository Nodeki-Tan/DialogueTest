using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/RequirementData")]
public class QuestRequirementData : QuestRequirement
{
    public string correctValue;

    public override bool IsRequirementMet()
    {
        return QuestManager.Singleton.CheckQuestData<string>(requirementName, correctValue);
    }

    public override string TryGetRequirementValue()
    {
        return bool.Parse(QuestManager.Singleton.GetQuestData<string>(requirementName, "false")) ? "Done" : "Not done";
    }

}