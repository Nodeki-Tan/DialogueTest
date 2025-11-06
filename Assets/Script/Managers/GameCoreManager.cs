using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameCoreManager
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void init()
    {
        Debug.Log("Gamecore started"); // this initializes the game

        _ = new AssetManager();
        _ = new DialogueManager();
        _ = new PopUpManager();
        _ = new FileManager();
        _ = new SettingsManager();
        _ = new GameManager();
        _ = new EquipmentManager();
        _ = new QuestManager();
        //_ = new PatreonManager();


        //SettingsManager.loadGameData();

    }



}