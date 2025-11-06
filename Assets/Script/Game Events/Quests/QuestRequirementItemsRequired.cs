using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/RequirementItems")]
public class QuestRequirementItemsRequired : ScriptableObject
{

    public ItemRequirement[] itemsRequired;

    public bool IsRequirementMet()
    {
        return Inventory.instance.CheckForItems(itemsRequired);
    }

}