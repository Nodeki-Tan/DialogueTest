using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{

    public GameEvents.Event eventToTrigger;

    public virtual void DoSomething()
    {
        eventToTrigger.OnStateEnter();

        eventToTrigger.OnStateStay();

        eventToTrigger.OnStateExit();
    }

}
