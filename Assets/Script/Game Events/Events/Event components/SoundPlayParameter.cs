using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Cinema/Event/Component/SoundPlay Parameter")]
    public class SoundPlayParameter : ScriptableObject
    {

        public SoundClipParameter[] clips;

        public PlayType type;

        public void Execute(){

            GameManager machine = GameObject.FindWithTag("gameManager").GetComponent<GameManager>();

            AudioSource source = machine.GetComponent<AudioSource>();
            var possibility = Random.Range(0.0f, 100.0f);
            var choosen = Random.Range(0, clips.Length);

            var cliptoplay = clips[choosen];

            machine.audioTimerCounter -= Time.deltaTime * 0.5f;


            if(!cliptoplay.bypassTime) if (machine.audioTimerCounter > 0) return;

            switch (type)
            {
                case PlayType.Random:

                    if(source.isPlaying == false || cliptoplay.bypassAudioPlay){

                        if(possibility <= cliptoplay.chanceToPlay){

                            source.clip = cliptoplay.clip;
                            source.maxDistance = cliptoplay.audioRadius;
                            source.Play();

                            machine.audioTimerCounter = Random.Range(cliptoplay.minWaitAfterPlay, cliptoplay.maxWaitAfterPlay);

                        } else {

                            machine.audioTimerCounter = Random.Range(cliptoplay.minWaitAfterPlay, cliptoplay.maxWaitAfterPlay);

                        }
                        
                    }

                break;

                case PlayType.Interval:
                    
                break;

                case PlayType.OneShot:

                    if(source.isPlaying == false || cliptoplay.bypassAudioPlay){

                        if(possibility <= cliptoplay.chanceToPlay){

                            source.clip = cliptoplay.clip;
                            source.maxDistance = cliptoplay.audioRadius;
                            source.Play();

                            machine.audioTimerCounter = Random.Range(cliptoplay.minWaitAfterPlay, cliptoplay.maxWaitAfterPlay);

                        } else {
                            
                            machine.audioTimerCounter = Random.Range(cliptoplay.minWaitAfterPlay, cliptoplay.maxWaitAfterPlay);

                        }

                    }

                    
                break;

                case PlayType.Constant:
                    
                break;
                
                default:
                break;
            }
        }

    }

    public enum PlayType{

        Random,
        Interval,
        OneShot,
        Constant

    }

}