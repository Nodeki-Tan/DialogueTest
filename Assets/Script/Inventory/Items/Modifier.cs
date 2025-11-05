using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New modifier", menuName = "Inventory/Modifier")]
public class Modifier : ScriptableObject
{
    // Defines how long this will last, 0 for infinite else timed
    public float EffectDuration;

    public ModifierType type;
    
    public virtual  void Apply(){}

    public virtual  void Tick(){}

}

[System.Serializable]
public class ModifierPair
{
    public Modifier mod;
    public string value;
    
}

public enum ModifierType { Life, Damage, Speed, Stamina }