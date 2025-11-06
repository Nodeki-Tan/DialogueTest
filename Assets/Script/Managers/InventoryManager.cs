using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    #region Singleton

    public static InventoryManager Singleton { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this new instance
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Set this as the singleton instance
            Singleton = this;
            // Optionally, prevent the object from being destroyed on scene load
            DontDestroyOnLoad(gameObject);
        }

        Init();
    }

    #endregion

    public void Init()
    {
        space = maxSpace;

        Debug.Log($"{nameof(InventoryManager)} created");
    }

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnRecipeInfo(Recipe recipe);
    public OnRecipeInfo onRecipeInfoCallback;

    public dataCollection saveData;

    public int maxSpace = 10;
    public int space;  // Amount of slots in inventory

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
            if (i.name == item.name)
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
                if (i.name == item.name)
                {
                    items.Remove(i);     // Remove item from list
                    space += item.size;

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

            //Debug.Log("Crafted " + name);
            return true;
        }

        //Debug.Log("No requirements for " + name);

        return false;
    }

    public bool CheckForItems(ItemRequirement[] itemRequirements)
    {
        if (itemRequirements != null)
        {
            foreach (ItemRequirement req in itemRequirements)
            {
                Debug.Log("Checking for " + req.quantity + " x " + req.item.name);
                int q = req.quantity;
                foreach (Item i in items)
                {
                    if (i.name == req.item.name)
                    {
                        q--;
                        if (q <= 0)
                        {
                            break;
                        }
                    }
                }
                if (q > 0)
                {
                    Debug.Log("Not enough " + req.item.name);
                    return false;
                }
            }
            //Debug.Log("All items available for " + name);
            return true;
        }
        //Debug.Log("No requirements for " + name);
        return false;
    }

    public int GetItemAmountByType(Item itemType)
    {
        int amount = 0;

        foreach(Item item in items)
        {
            if (item.name == itemType.name)
                amount++;
        }

        return amount;
    }

    public bool SerializeInventory()
    {
        // Implement saving logic here

        saveData = new dataCollection(new Dictionary<string, string>(), "inventory", "Data", "Saves/");
        saveData.SaveVariable("itemCount", items.Count.ToString());

        foreach (Item item in items)
        {
            int index = items.IndexOf(item);
            saveData.SaveVariable("item_" + index, item.name);
        }

        //saveData.SaveFile();

        Debug.Log("Inventory saved.");
        return true;
    }

    public bool LoadInventory(dataCollection data) { 

        items.Clear();
        space = 10; // Reset space

        int itemCount = data.TryGetInt("itemCount", 0);

        for (int i = 0; i < itemCount; i++)
        {
            string itemName = data.data["item_" + i];
            var item = AssetManager.Singleton.getByName(itemName);
            if (item != null)
            {
                Add(item);
            }

        }

        Debug.Log("Inventory loaded.");
        return true;
    }


}