using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Launcher class for the server program
public class QuestManager
{

    #region Singleton

    private static QuestManager _singleton = new();
    public static QuestManager Singleton
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
                Debug.Log($"{nameof(QuestManager)} instance already exists, destroying duplicate!");
            }
        }
    }

    #endregion

    public void Init()
    {
        Singleton = this;

        questData = new dataCollection(new Dictionary<string, string>(), "questData", "Data", "Saves/");

        Debug.Log("QuestManager created");
    }

    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    public dataCollection questData;

    public void StartQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            // Trigger start events
            if (quest.triggerEvents != null)
            {
                foreach (var evt in quest.triggerEvents)
                {
                    evt.OnStateEnter();
                }
            }
            Debug.Log($"Quest '{quest.name}' started.");
        }
    }

    public void SetQuestData<T>(string key, T value)
    {
        // Implement quest data setting logic here
        questData.SaveVariable(key, value.ToString());
    }

    public bool CheckQuestData<T>(string key, T value)
    {
        // Implement quest data checking logic here
        string storedValue;
        if (questData.data.TryGetValue(key, out storedValue))
        {
            return storedValue.Equals(value.ToString());
        }
        return false;
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest) && quest.IsQuestComplete())
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            // Trigger completion events
            if (quest.completionEvents != null)
            {
                foreach (var evt in quest.completionEvents)
                {
                    evt.OnStateExit();
                }
            }
            Debug.Log($"Quest '{quest.name}' completed.");
        }
    }

    public void CheckQuestCompletion(Quest quest)
    {
        if (activeQuests.Contains(quest) && quest.IsQuestComplete())
        {
            CompleteQuest(quest);
        }
    }

    public bool IsQuestComplete(Quest quest)
    {
        return completedQuests.Contains(quest);
    }

    public bool CheckQuestCompleted(Quest quest)
    {
        return completedQuests.Contains(quest);
    }

    public bool SaveQuests()
    {
        // Implement saving logic here
        dataCollection data = new dataCollection(new Dictionary<string, string>(), "quests", "Data", "Saves/");

        data.SaveVariable("activeCount", activeQuests.Count.ToString());
        foreach (var quest in activeQuests)
        {
            int index = activeQuests.IndexOf(quest);

            data.SaveVariable($"active_{index}", quest.name);
        }

        data.SaveVariable("completedCount", completedQuests.Count.ToString());
        foreach (var quest in completedQuests)
        {
            int index = completedQuests.IndexOf(quest);

            data.SaveVariable($"completed_{index}", quest.name);
        }

        data.SaveVariable("dataCount", questData.data.Count.ToString());
        foreach (var questdata in questData.data)
        {
            data.SaveVariable(questdata.Key, questdata.Value);
        }


        return true;
    }

    public bool LoadQuests()
    {
        dataCollection data = new dataCollection(new Dictionary<string, string>(), "quests", "Data", "Saves/");
        data.LoadFile();
        questData = new dataCollection(new Dictionary<string, string>(), "questData", "Data", "Saves/");

        int activeCount = data.TryGetInt("activeCount", 0);
        for (int i = 0; i < activeCount; i++)
        {
            string questName = data.data["active_" + i];
            var quest = AssetManager.Singleton.getQuestByName(questName);

            if (quest != null)
            {
                activeQuests.Add(quest);
            }
        }

        int completedCount = data.TryGetInt("completedCount", 0);
        for (int i = 0; i < completedCount; i++)
        {
            string questName = data.data["completed_" + i];
            var quest = AssetManager.Singleton.getQuestByName(questName);

            if (quest != null)
            {
                completedQuests.Add(quest);
            }
        }

        int dataCount = data.TryGetInt("dataCount", 0);
        for (int i = 0; i < dataCount; i++)
        {
            string key = "data_" + i;
            if (data.data.ContainsKey(key))
            {
                string value = data.data[key];
                questData.SaveVariable(key, value);
            }
        }

        // Implement loading logic here
        return true;

    }

}
