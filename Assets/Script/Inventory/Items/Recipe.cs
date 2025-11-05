using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{

    new public string name = "New Recipe";    // Name of the item
    public Sprite icon = null;              // Item icon

    public ItemRequirement[] itemRequirements = null;

    // Called when the item is pressed in the inventory
    public virtual bool Craft()
    {
        // Use the item
        // Something might happen

        return Inventory.instance.TakeItems(itemRequirements);
    }

}


public class ItemRequirement
{
    public Item item;
    public int quantity;
}