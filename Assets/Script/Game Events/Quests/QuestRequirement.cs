using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/Requirement")]
public class QuestRequirement : ScriptableObject
{

    public bool IsRequirementMet()
    {
        return false;
    }

}