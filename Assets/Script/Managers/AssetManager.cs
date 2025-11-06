using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Launcher class for the server program
public class AssetManager
{

    #region Singleton

    private static AssetManager _singleton = new();
    public static AssetManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
                _singleton.Init();
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(AssetManager)} instance already exists, destroying duplicate!");
            }
        }
    }

    public AssetManager()
    {
        Singleton = this;
    }

    #endregion

    public void Init()
    {
        LoadItems();
        LoadRecipes();

        LoadQuests();

        Debug.Log("AssetManager created");
    }


    public Item[] itemsDatabase;
    public Quest[] questsDatabase;
    public Recipe[] recipesDatabase;

    void LoadItems()
    {
        itemsDatabase = Resources.LoadAll<Item>("Items/");
        Debug.Log($"Loaded {itemsDatabase.Length} items.");
    }

    public Item getByName(string name)
    {
        foreach (Item item in itemsDatabase)
        {
            if (item.name == name)
            {
                return ScriptableObject.Instantiate(item);
            }
        }
        return null;
    }

    void LoadQuests()
    {
        questsDatabase = Resources.LoadAll<Quest>("Quests/");
        Debug.Log($"Loaded {questsDatabase.Length} quests.");
    }   

    public Quest getQuestByName(string name)
    {
        foreach (Quest quest in questsDatabase)
        {
            if (quest.name == name)
            {
                return ScriptableObject.Instantiate(quest);
            }
        }
        return null;
    }

    void LoadRecipes()
    {
        recipesDatabase = Resources.LoadAll<Recipe>("Recipes/");
        Debug.Log($"Loaded {recipesDatabase.Length} recipes.");
    }

    public Recipe getRecipeByName(string name)
    {
        foreach (Recipe recipe in recipesDatabase)
        {
            if (recipe.name == name)
            {
                return ScriptableObject.Instantiate(recipe);
            }
        }
        return null;
    }



}

