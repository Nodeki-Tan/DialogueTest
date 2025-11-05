using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisableEnableTrigger : TimedTrigger
{

    public GameObject toDisable, toEnable;

    public override void DoStuff()
    {
        base.DoStuff();

        toDisable.SetActive(false);

        if (SettingsManager.touchControls)
        {

            toEnable.SetActive(true);
        }

    }
}
