using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

    Anything that could happen, be customized and variable, that interacts with the game world
    or requires to, is an event.

    Can be play an animation, sound, both, execute code.

    Can be used by a player, NPC or gameobj.

    Has 3 steps, Enter Stay and Exit.

*/

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Gameplay/Event")]
    public class Event : ScriptableObject
    {

        // Executed whenever the Event has been called
        public virtual void OnStateEnter() { }

        // Executed whenever the Event is running
        public virtual void OnStateStay() { }

        // Executed once after exiting the event
        public virtual void OnStateExit() { }


    }

}