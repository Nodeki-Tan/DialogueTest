using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Quest Requirement", menuName = "Quests/RequirementItems")]
public class QuestRequirementItemsRequired : QuestRequirement
{

    public ItemRequirement[] itemsRequired;

    public override bool IsRequirementMet()
    {
        return InventoryManager.Singleton.CheckForItems(itemsRequired);
    }

}