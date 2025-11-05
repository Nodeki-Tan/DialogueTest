using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager
{

    #region Singleton

    public static SettingsManager instance;

    public SettingsManager()
    {
        instance = this;

        Debug.Log("SettingsManager created");

        Init();
    }

    #endregion


    void Init()
    {
        audioMixer = Resources.Load("Master") as AudioMixer;

        settings = new dataCollection(new(),
        "config", "config", "config/");
    }

    public AudioMixer audioMixer;
    //public AudioSource Click;

    public static float masterSound, musicSound, SFXSound, voiceSound;

    public static int qualityLevel;

    public static float mouseX, mouseY;

    public static int nightSaved;

    public static bool touchControls;

    public static dataCollection settings;

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);

        masterSound = volume;

        settings.SaveVariable("masterSound", masterSound.ToString());
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);

        musicSound = volume;

        settings.SaveVariable("musicSound", musicSound.ToString());
    }

    public void setSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);

        SFXSound = volume;

        settings.SaveVariable("SFXSound", SFXSound.ToString());
    }

    public void setVoiceVolume(float volume)
    {
        audioMixer.SetFloat("Voice", volume);

        voiceSound = volume;

        settings.SaveVariable("voiceSound", voiceSound.ToString());
    }

    public void saveGame()
    {
        settings.SaveFile();

        Debug.Log("created settings file");
    }

    public void loadGame()
    {        
        settings.LoadFile();

        Debug.Log("loaded settings file");
    }


    public void qhd()
    {
        Screen.SetResolution(960, 540, true);
    }
    public void hd()
    {
        Screen.SetResolution(1280, 720, true);
    }
    public void fhd()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void fhd2()
    {
        Screen.SetResolution(2560, 1440, true);
    }

    public void Low()
    {
        QualitySettings.SetQualityLevel(0);

        qualityLevel = 0;

        settings.SaveVariable("qualityLevel", qualityLevel.ToString());
    }
    public void Med()
    {
        QualitySettings.SetQualityLevel(1);

        qualityLevel = 1;

        settings.SaveVariable("qualityLevel", qualityLevel.ToString());
    }
    public void High()
    {
        QualitySettings.SetQualityLevel(2);

        qualityLevel = 2;

        settings.SaveVariable("qualityLevel", qualityLevel.ToString());
    }
    public void Ultra()
    {
        QualitySettings.SetQualityLevel(3);

        qualityLevel = 3;

        settings.SaveVariable("qualityLevel", qualityLevel.ToString());
    }

    public static void loadGameData()
    {
        instance.loadGame();

        if (settings.data.Keys.Count <= 0)
            return;

        masterSound =   settings.TryGetValue  <float>  ("masterSound");
        musicSound =    settings.TryGetValue  <float>  ("musicSound");
        SFXSound =      settings.TryGetValue  <float>  ("SFXSound");
        voiceSound =    settings.TryGetValue  <float>  ("voiceSound");

        qualityLevel =  settings.TryGetValue  <int>  ("qualityLevel");

        mouseX =    settings.TryGetValue  <float>  ("mouseX");
        mouseY =    settings.TryGetValue  <float>  ("mouseY");

        nightSaved =    settings.TryGetValue  <int>  ("nightSaved");

        //touchControls = settings.TryGetValue  <bool>  ("touchControls");

        instance.setMasterVolume(masterSound);
        instance.setMusicVolume(musicSound);
        instance.setSFXVolume(SFXSound);
        instance.setVoiceVolume(voiceSound);

        QualitySettings.SetQualityLevel(qualityLevel);

        Debug.Log("Settings applied!");
    }

}
