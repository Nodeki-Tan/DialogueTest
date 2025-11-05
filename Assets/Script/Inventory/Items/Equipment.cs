using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Consumable
{

    public EquipmentSlot equipSlot; // Slot to store equipment in

    // When pressed in inventory
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);  // Equip it
        RemoveFromInventory();                  // Remove it from inventory
    }

}

public enum EquipmentSlot { Right, Left, Head, Chest, Legs, Feet }
