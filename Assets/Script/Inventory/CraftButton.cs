using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Sits on all CraftButtons. */

public class CraftButton : MonoBehaviour
{

    public Image icon;          // Reference to the Icon image
    public TMP_Text text; // Reference to the craft name text

    public Recipe recipe;  // Current item in the slot

    void Start()
    {
        // Initialize the UI
        icon.sprite = recipe.icon;
        text.text = recipe.name;
    }


    // Called when the remove button is pressed
    public void OnCraftButton()
    {
        if (InventoryManager.Singleton.onRecipeInfoCallback != null)
            InventoryManager.Singleton.onRecipeInfoCallback.Invoke(recipe);
    }



}