using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats
{

    // Use this for initialization
    void Start()
    {
        EquipmentManager.Singleton.onEquipmentChanged += OnEquipmentChanged;
		EquipmentManager.Singleton.onConsume += OnConsume;
    }
	
	void OnConsume(Consumable item){
		
		//currentHealth += item.healthModifier;
		
	}

    // Called when an item gets equipped/unequipped
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        // Add new modifiers
        if (newItem != null)
        {
            AddModifiers(newItem);
        }

        // Remove old modifiers
        if (oldItem != null)
        {
            RemoveModifiers(oldItem);
        }

    }

    void AddModifiers(Consumable item){


       foreach (var mod in item.modifiers)
        {
            if (mod.mod.type == speed.type)
            {
                // Do stuff
                speed.AddModifier(Int32.Parse(mod.value));
            }    

            if (mod.mod.type == stamina.type)
            {
                // Do stuff
                stamina.AddModifier(Int32.Parse(mod.value));
            }           
        }

    }

    void RemoveModifiers(Consumable item){


       foreach (var mod in item.modifiers)
        {
            if (mod.mod.type == speed.type)
            {
                // Do stuff
                speed.RemoveModifier(Int32.Parse(mod.value));
            }    

            if (mod.mod.type == stamina.type)
            {
                // Do stuff
                stamina.RemoveModifier(Int32.Parse(mod.value));
            }           
        }

    }

    public override void Die()
    {
        base.Die();
        //PlayerManager.instance.KillPlayer();
    }
}