using System;
using System.Runtime.Serialization;
using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{

    new public string name = "New Recipe";    // Name of the item
    public Sprite icon = null;              // Item icon

    public Item resultItem = null;
    public ItemRequirement[] itemRequirements = null;

    // Called when the item is pressed in the inventory
    public virtual bool Craft()
    {
        // Use the item
        // Something might happen
        if (InventoryManager.Singleton.TakeItems(itemRequirements))
        {
            InventoryManager.Singleton.Add(AssetManager.Singleton.getByName(resultItem.name));

            return true;
        }

        return false;
    }

    public virtual bool CanCraft()
    {
        return InventoryManager.Singleton.CheckForItems(itemRequirements);
    }

}

// With this corrected line:
[Serializable]
public struct ItemRequirement
{
    public Item item;
    public int quantity;

    public ItemRequirement(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}