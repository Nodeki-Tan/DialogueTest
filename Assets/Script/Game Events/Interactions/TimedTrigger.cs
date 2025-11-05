using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTrigger : MonoBehaviour
{
    public float secondsDown = 1;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(Countdown(secondsDown));
    }

    public virtual void DoStuff()
    {
        // Whatever you want to happen when the countdown finishes
    }

    IEnumerator Countdown(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter -= 0.1f;
        }
        DoStuff();
    }

}
