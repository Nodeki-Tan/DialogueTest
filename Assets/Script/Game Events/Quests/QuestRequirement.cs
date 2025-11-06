using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/Requirement")]
public class QuestRequirement : ScriptableObject
{
    public string requirementName;
    public string requirementDescription;

    public virtual bool IsRequirementMet()
    {
        return false;
    }

    public virtual string TryGetRequirementValue()
    {
        return IsRequirementMet().ToString();
    }

}