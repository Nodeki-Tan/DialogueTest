using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{

    new public string name = "New String";    // Name of the dialogue

    public QuestRequirement[] requirements;  // Requirements to end the quest

    public Event[] triggerEvents = null;   // Events to execute when starting the quest
    public Event[] completionEvents = null; // Events to execute when completing the quest

    public bool IsQuestComplete()
    {
        foreach (QuestRequirement req in requirements)
        {
            if (!req.IsRequirementMet())
                return false;
        }
        return true;
    }

}