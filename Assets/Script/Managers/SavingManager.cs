using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Event = GameEvents.Event;
using UnityEditor.Rendering;
using System.Linq;

public class SavingManager
{

    #region Singleton

    public static SavingManager instance;

    #endregion

    public SavingManager()
    {
        instance = this;

        Debug.Log("SavingManager created");

        Init();
    }

    public void Init()
    {
        Debug.Log("SavingManager started");
    }

    public List<dataCollection> savedDataList = new List<dataCollection>();
    public dataCollection currentSelectedSave;
    public string saveFileName = "savefile";

    public void SaveGame(string filename)
    {
        // Create new save data collection
        currentSelectedSave = new dataCollection(new Dictionary<string, string>(), filename, "sav", "Saves/");

        // Serialize inventory and quests
        InventoryManager.Singleton.SerializeInventory();
        QuestManager.Singleton.SerializeQuests();

        // Save inventory data
        int inventoryCount = InventoryManager.Singleton.saveData.TryGetInt("itemCount", 0);
        currentSelectedSave.SaveVariable("itemCount", inventoryCount.ToString());
        for (int i = 0; i < inventoryCount; i++)
        {
            var data = InventoryManager.Singleton.saveData.data["item_" + i];
            currentSelectedSave.SaveVariable("item_" + i, data);
        }

        // Save active quests
        int activeQuestCount = QuestManager.Singleton.saveData.TryGetInt("activeCount", 0);
        currentSelectedSave.SaveVariable("activeCount", activeQuestCount.ToString());
        for (int i = 0; i < activeQuestCount; i++)
        {
            var data = QuestManager.Singleton.saveData.data["active_" + i];
            currentSelectedSave.SaveVariable("active_" + i, data);
        }

        // Save completed quests
        int completedQuestCount = QuestManager.Singleton.saveData.TryGetInt("completedCount", 0);
        currentSelectedSave.SaveVariable("completedCount", completedQuestCount.ToString());
        for (int i = 0; i < completedQuestCount; i++)
        {
            var data = QuestManager.Singleton.saveData.data["completed_" + i];
            currentSelectedSave.SaveVariable("completed_" + i, data);
        }

        // Save quest data
        int dataCount = QuestManager.Singleton.saveData.TryGetInt("dataCount", 0);
        currentSelectedSave.SaveVariable("dataCount", dataCount.ToString());

        for (int i = 0; i < dataCount; i++)
        {
            if (QuestManager.Singleton.saveData.data.ContainsKey("dataCount"))
            {
                int index = QuestManager.Singleton.saveData.data.Keys.ToList().IndexOf("dataCount");

                for (int o = index; o < dataCount; o++)
                {
                    string key = QuestManager.Singleton.saveData.data.ElementAt(o).Key;
                    if (QuestManager.Singleton.saveData.data.ContainsKey(key))
                    {
                        var dataValue = QuestManager.Singleton.saveData.data[key];
                        currentSelectedSave.SaveVariable(key, dataValue);
                    } 
                } 
            }
        }

        // Finally, save to file
        currentSelectedSave.SaveFile();
        Debug.Log("Game Saved");

    }

    public void LoadGame(string filename)
    {
        // Create new save data collection and load from file
        currentSelectedSave = new dataCollection(new Dictionary<string, string>(), filename, "sav", "Saves/");
        currentSelectedSave.LoadFile();


        // Load inventory data
        int inventoryCount = currentSelectedSave.TryGetInt("inventoryCount", 0);
        InventoryManager.Singleton.saveData.data.Clear();

        InventoryManager.Singleton.saveData.SaveVariable("itemCount", (inventoryCount - 1).ToString());
        for (int i = 0; i < inventoryCount; i++)
        {
            string itemData = currentSelectedSave.data["item_" + i];
            InventoryManager.Singleton.saveData.SaveVariable("item_" + i, itemData);
        }

        InventoryManager.Singleton.LoadInventory(InventoryManager.Singleton.saveData);


        // Load active quests
        int activeCount = currentSelectedSave.TryGetInt("activeCount", 0);
        QuestManager.Singleton.saveData.data.Clear();

        QuestManager.Singleton.saveData.SaveVariable("activeCount", (activeCount).ToString());
        for (int i = 0; i < activeCount; i++)
        {
            string questData = currentSelectedSave.data["active_" + i];
            QuestManager.Singleton.saveData.SaveVariable("active_" + i, questData);
        }


        int completedCount = currentSelectedSave.TryGetInt("completedCount", 0);
        QuestManager.Singleton.saveData.SaveVariable("completedCount", (completedCount).ToString());
        for (int i = 0; i < completedCount; i++)
        {
            string questData = currentSelectedSave.data["completed_" + i];
            QuestManager.Singleton.saveData.SaveVariable("completed_" + i, questData);
        }

        int dataCount = currentSelectedSave.TryGetInt("dataCount", 0);
        int index = currentSelectedSave.data.Keys.ToList().IndexOf("dataCount");

        for (int o = index; o < dataCount; o++)
        {
            string key = currentSelectedSave.data.ElementAt(o).Key;
            if (currentSelectedSave.data.ContainsKey(key))
            {
                var dataValue = currentSelectedSave.data[key];
                QuestManager.Singleton.saveData.SaveVariable(key, dataValue);
            }
        }

        QuestManager.Singleton.LoadQuests(QuestManager.Singleton.saveData);
        Debug.Log("Game Loaded");

    }


}
