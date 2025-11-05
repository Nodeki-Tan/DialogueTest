using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    This object defines an animation trigger event, aka the conditions to make an animation play

*/

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Cinema/Event/Component/Animation Trigger")]
    public class AnimationTriggerEvent : Event
    {

        public AnimationTrigger enterParam;
        public AnimationTrigger stayParam;
        public AnimationTrigger exitParam;    


        // Executed whenever the Event has been called
        public override void OnStateEnter() { 

            if (enterParam != null)
                enterParam.Execute();

        }

        // Executed whenever the Event is running
        public override void OnStateStay() {

            if (stayParam != null)
                stayParam.Execute();

        }

        // Executed once after exiting the event
        public override void OnStateExit() {

            if (exitParam != null)
                exitParam.Execute();

        }
        
    }
    
}