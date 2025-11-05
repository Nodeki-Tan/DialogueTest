using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{

    [CreateAssetMenu(menuName = "Cinema/Event/Component/Animation Parameter")]
    public class AnimationParameter : ScriptableObject
    {
        public string ParameterName;

        // reference values for the controller
        public int IntVal;
        public float FloatVal;
        public bool BoolVal;

        public ParamType type;

    }

}