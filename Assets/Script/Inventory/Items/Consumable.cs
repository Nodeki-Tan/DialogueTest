using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
	
    [SerializeField] public List<ModifierPair> modifiers;
	
    public override void Use()
    {
        base.Use();
		EquipmentManager.instance.Consume(this);
        RemoveFromInventory();                  // Remove it from inventory
    }
}
