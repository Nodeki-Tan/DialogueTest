using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* This object updates the inventory UI. */

public class CraftUI : MonoBehaviour
{

    public Transform recipesParent;   // The parent object of all the items
    public Transform infoParent;   // The parent object of all the items
    public GameObject craftUI;  // The entire UI
    public GameObject recipeButtonPrefab; // The recipe button prefab
    public GameObject craftButton;

    CraftButton[] slots;  // List of all the slots
    Recipe selectedRecipe = null;

    void Start()
    {
        craftUI.SetActive(false);

        craftButton.GetComponent<Button>().interactable = false;

        InventoryManager.Singleton.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback
        InventoryManager.Singleton.onRecipeInfoCallback += ShowCaseCraftInfo;    // Subscribe to the onItemChanged callback

        // Populate our slots array

        for (int i = 0; i < AssetManager.Singleton.recipesDatabase.Length; i++)
        {
            var craftButton = UnityEngine.Object.Instantiate(recipeButtonPrefab, recipesParent); // Fixed CS0176 by qualifying with 'Object'

            craftButton.GetComponent<CraftButton>().recipe = AssetManager.Singleton.recipesDatabase[i];
        }

        slots = recipesParent.GetComponentsInChildren<CraftButton>();
    }

    void Update()
    {
        // Check to see if we should open/close the inventory
        if (Input.GetKeyDown(KeyCode.C))
        {
            var displayText = infoParent.GetComponentInChildren<TMP_Text>();
            displayText.text = "";

            craftUI.SetActive(!craftUI.activeSelf);
        }
    }

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI() { 

        if ( craftUI.activeSelf ) {
            craftButton.GetComponent<Button>().interactable = selectedRecipe.CanCraft();
            ShowCaseCraftInfo(selectedRecipe);
        }

    }

    // Called when the remove button is pressed
    public void OnCraftButton()
    {
        selectedRecipe.Craft();
    }

    public void ShowCaseCraftInfo(Recipe craft)
    {
        selectedRecipe = craft;
        craftButton.GetComponent<Button>().interactable = selectedRecipe.CanCraft();

        var displayText = infoParent.GetComponentInChildren<TMP_Text>();

        if (displayText != null)
        {
            displayText.text = craft.name;
            //displayText.text += Environment.NewLine + craft.description;

            displayText.text += Environment.NewLine + "Requires:" + Environment.NewLine;

            for (int i = 0; i < craft.itemRequirements.Length; i++)
            {

                displayText.text +=
                    Environment.NewLine + craft.itemRequirements[i].item.name +
                    Environment.NewLine + craft.itemRequirements[i].quantity +
                    " / " + InventoryManager.Singleton.GetItemAmountByType(craft.itemRequirements[i].item);

            }
        }
    }

}
