using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*

    Anything that could happen, be customized and variable, that interacts with the game world
    or requires to, is an event.

    Can be play an animation, sound, both, execute code.

    Can be used by a player, NPC or gameobj.

    Has 3 steps, Enter Stay and Exit.

*/

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Gameplay/DialogueEndEvent")]
    public class DialogueEndEvent : Event
    {
        public string sceneName = "";

        // Override the base class method to resolve CS0114
        public override void OnStateExit()
        {
            // Here you can add any custom logic you want to execute when the dialogue ends
            // Like for example a scene switch, or a quest update
            Debug.Log("End of dialogue");


            // Sample of switching scene on dialogue end
            SceneManager.LoadScene(sceneName);
        }
    }

}