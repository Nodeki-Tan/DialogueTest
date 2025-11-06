using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// Launcher class for the server program
public class QuestManager : MonoBehaviour
{

    #region Singleton

    public static QuestManager Singleton { get; private set; }

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
        questData = new dataCollection(new Dictionary<string, string>(), "questData", "Data", "Saves/");

        Debug.Log($"{nameof(QuestManager)} created");
    }


    public dataCollection saveData;

    // Callback which is triggered when
    // an quests gets added/removed.
    public delegate void OnQuestChanged(Quest quest, bool completed);
    public OnQuestChanged onQuestChangedCallback;

    public delegate void OnQuestInfo(Quest quest);
    public OnQuestInfo onQuestInfoCallback;

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

            if (onQuestChangedCallback != null)
                onQuestChangedCallback.Invoke(quest, false);

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

    public T GetQuestData<T>(string key, T defaultval = default)
    {
        T returnVal;

        string storedData;
        if (questData.data.TryGetValue(key, out storedData))
        {
            returnVal = GetValue<T>(storedData);

            return returnVal;
        }
        else
        {
            return defaultval;
        }
    }

    public T GetValue<T>(string value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
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

            if (onQuestChangedCallback != null)
                onQuestChangedCallback.Invoke(quest, true);

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

    public bool SerializeQuests()
    {
        // Implement saving logic here
        saveData = new dataCollection(new Dictionary<string, string>(), "quests", "Data", "Saves/");

        saveData.SaveVariable("activeCount", activeQuests.Count.ToString());
        foreach (var quest in activeQuests)
        {
            int index = activeQuests.IndexOf(quest);

            saveData.SaveVariable($"active_{index}", quest.name);
        }

        saveData.SaveVariable("completedCount", completedQuests.Count.ToString());
        foreach (var quest in completedQuests)
        {
            int index = completedQuests.IndexOf(quest);

            saveData.SaveVariable($"completed_{index}", quest.name);
        }

        saveData.SaveVariable("dataCount", questData.data.Count.ToString());
        foreach (var questdata in questData.data)
        {
            saveData.SaveVariable(questdata.Key, questdata.Value);
        }

        //saveData.SaveFile();

        return true;
    }

    public bool LoadQuests(dataCollection data)
    {

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

        if (data.data.ContainsKey("dataCount"))
        {

            // With this corrected line:
            int index = data.data.Keys.ToList().IndexOf("dataCount");

            for (int i = index; i < dataCount; i++)
            {
                string key = data.data.ElementAt(i).Key;
                if (data.data.ContainsKey(key))
                {
                    string value = data.data[key];
                    questData.SaveVariable(key, value);
                }
            }

        }



        // Implement loading logic here
        return true;

    }

}
