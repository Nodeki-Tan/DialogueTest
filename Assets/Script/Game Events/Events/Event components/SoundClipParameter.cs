using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEvents
{

    [CreateAssetMenu(menuName = "Cinema/Event/Component/SoundClip Parameter")]
    public class SoundClipParameter : ScriptableObject
    {

        public AudioClip clip;
        public float chanceToPlay = 50;
        public float audioRadius = 100;

        public bool bypassTime = false;
        public bool bypassAudioPlay = false;

        public float maxWaitAfterPlay = 2;
        public float minWaitAfterPlay = 4;
        
    }

}