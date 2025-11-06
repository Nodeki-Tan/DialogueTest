using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Event = GameEvents.Event;
using UnityEditor.Rendering;

public class PopUpManager
{

    #region Singleton

    public static PopUpManager instance;

    public PopUpManager()
    {
        instance = this;

        Debug.Log("PopUpManager created");

        Init();
    }

    #endregion

    private PopUp current;
    private GameObject popupObject;
    private TMP_Text popupText;
    private Image popupSprite;

    public PopUp Current { get => current; set => current = value; }
    public TMP_Text PopupText { get => popupText; set => popupText = value; }

    public void Init()
    {
        Debug.Log("PopUpManager started");
    }

    public void OpenPopUp(PopUp _popup)
    {
        popupText.text = ""; 

        Debug.Log("Starting dialogue " + _popup.name);

        popupObject = GameObject.FindGameObjectWithTag("PopUpDisplay");

        popupText = GameObject.FindGameObjectWithTag("PopUpTextDisplay").GetComponent<TMP_Text>();
        popupSprite = GameObject.FindGameObjectWithTag("PopUpSpriteDisplay").GetComponent<Image>();
        popupSprite.sprite = _popup.sprite;

        if (_popup.triggerEvents != null)
        {
            foreach (Event e in _popup.triggerEvents)
            {
                e.OnStateEnter();
            }
        }

        current = _popup;
        popupText.text = current.text;

        popupObject.SetActive(true);
    }

    public void ClosePopUp()
    {

        if (current.triggerEvents != null)
        {
            foreach (Event e in current.triggerEvents)
            {
                e.OnStateExit();
            }
        }

        popupObject.SetActive(false);

    }

}