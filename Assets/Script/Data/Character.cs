using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Character Card", menuName = "Dialogue/Character Card")]
public class Character : ScriptableObject
{

    new public string name = "New String";    // Name of the dialogue

    public Sprite[] sprites = null;


}
public enum EmoteType
{
    Neutral,
    Happy,
    Sad,
    Angry,
    Surprised
}