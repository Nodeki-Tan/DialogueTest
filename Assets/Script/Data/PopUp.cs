using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New PopUp", menuName = "Events/PopUp")]
public class PopUp : ScriptableObject
{

    new public string name = "New String";    // Name of the popup

    public Sprite sprite;
    public string text;

    public Event[] triggerEvents;              // Events triggered when the popup starts

}

