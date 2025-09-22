using UnityEngine;
using Event = GameEvents.Event;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{

	new public string name = "New String";    // Name of the dialogue
	public Sprite sprite = null;

	public bool autoContinue = true;

	[TextArea(3, 10)]
	public string[] sentences;

	public Dialogue child		= null;

    public Event triggerEvent	= null;					// Optional event to trigger at the start and end of the dialogue
}