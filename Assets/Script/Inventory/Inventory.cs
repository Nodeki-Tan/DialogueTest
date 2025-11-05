using System.Collections;
using System.Collections.Generic;
using Assets.Script.Inventory.Items;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    
    public int space = 10;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<Item> items = new List<Item>();

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(Item item)
    {
        // Don't do anything if it's a default item
        if (!item.isDefaultItem)
        {
            // Check if out of space
            if (space - item.size < 0)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);    // Add item to list
			space -= item.size;
			
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    // Remove an item
    public void Remove(Item item)
    {
        items.Remove(item);     // Remove item from list
		space += item.size;
		
        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    // Remove an item
    public void Remove(int itemslot)
    {
        Item item = items[itemslot];

        items.RemoveAt(itemslot);     // Remove item from list
        space += item.size;

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool RequireItem(Item item, int amount)
    {
        bool hasEnough = false;

        int q = amount;
        foreach (Item i in items)
        {
            if (i == item)
            {
                q--;
                if (q <= 0)
                {
                    hasEnough = true;
                    break;
                }
            }
        }

        if (hasEnough)
        {
            foreach (Item i in items)
            {
                if (i == item)
                {
                    items.Remove(i);     // Remove item from list
                    space -= item.size;

                    // Trigger callback
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();

                    amount--;
                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeItems(ItemRequirement[] itemRequirements)
    {
        // Use the item
        // Something might happen

        if (itemRequirements != null)
        {
            foreach (ItemRequirement req in itemRequirements)
            {
                Debug.Log("Requires " + req.quantity + " x " + req.item.name);

                if (!RequireItem(req.item, req.quantity))
                {
                    Debug.Log("Not enough " + req.item.name);
                    return false;
                }
            }

            Debug.Log("Crafted " + name);
            return true;
        }

        Debug.Log("No requirements for " + name);

        return false;
    }


}