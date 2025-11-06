using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Helper class to allow for easily instantiating Gameobjs. */

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager Singleton { get; private set; }

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
        Debug.Log($"{nameof(GameManager)} started");
    }


    public AudioSource mainSource;
    public float audioTimerCounter = 0.0f;


}