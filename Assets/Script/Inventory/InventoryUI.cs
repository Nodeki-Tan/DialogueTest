using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI

    public GameObject slotButtonPrefab; // The recipe button prefab

    InventorySlot[] slots;  // List of all the slots

    void Start()
    {
        inventoryUI.SetActive(false);

        InventoryManager.Singleton.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

        // Populate our slots array

        for (int i = 0; i < InventoryManager.Singleton.items.Count; i++)
        {
            var slot = UnityEngine.Object.Instantiate(slotButtonPrefab, itemsParent);
        }

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        
        // Check to see if we should open/close the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        
    }

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI()
    {
        if (inventoryUI.activeSelf)
        {

            foreach (Transform child in itemsParent) { Destroy(child.gameObject); }

            // Populate our slots array
            for (int i = 0; i < InventoryManager.Singleton.items.Count; i++)
            {
                var slot = UnityEngine.Object.Instantiate(slotButtonPrefab, itemsParent);
            }

            slots = itemsParent.GetComponentsInChildren<InventorySlot>();

            // Loop through all the slots
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < InventoryManager.Singleton.items.Count)  // If there is an item to add
                {
                    slots[i].AddItem(InventoryManager.Singleton.items[i]);   // Add it
                }
                else
                {
                    // Otherwise clear the slot
                    slots[i].ClearSlot();
                }
            }
        }
    }
}