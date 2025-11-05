using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Cinema/Event/Component/Animation Trigger")]
    public class AnimationTrigger : ScriptableObject
    {
        public AnimationParameter[] animationParameters;

        public void Execute(){

            GameManager machine = GameObject.FindWithTag("gameManager").GetComponent<GameManager>();

            Animator animator = machine.GetComponent<Animator>();

            foreach (var param in animationParameters)
            {
                
                switch (param.type)
                {
                    case ParamType.Integer:
                        animator.SetInteger(param.ParameterName, param.IntVal);
                    break;

                    case ParamType.Float:
                        animator.SetFloat(param.ParameterName, param.FloatVal);
                    break;
                    
                    case ParamType.Trigger:
                        animator.SetTrigger(param.ParameterName);
                    break;

                    case ParamType.Boolean:
                        animator.SetBool(param.ParameterName, param.BoolVal);
                    break;
                    
                    default:
                    break;
                }
            }

            
        }

    }

    public enum ParamType{

        Integer,
        Float,
        Trigger,
        Boolean

    }

}