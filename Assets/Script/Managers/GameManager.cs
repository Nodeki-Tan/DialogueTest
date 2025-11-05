using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Helper class to allow for easily instantiating Gameobjs. */

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion


    public AudioSource mainSource;
    public float audioTimerCounter = 0.0f;


}