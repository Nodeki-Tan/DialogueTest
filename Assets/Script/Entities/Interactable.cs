using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour
{

    public float interactionRadius = 0.75f;               // How close do we need to be to interact?
    public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

    bool isFocus = false;   // Is this interactable currently being focused?

    public string Name; // Name to show in case needed

    public Interaction[] interaction;

    public void Awake()
    {
        if(interactionTransform == null)
        {
            interactionTransform = gameObject.transform;
        }
        
    }

    public virtual void Interact()
    {
        if (isFocus) { 
            // This method is meant to be overwritten
            // here we interact with all the interactions, order matters!!!
            for (int i = 0; i < interaction.Length; i++)
            {
                interaction[i].DoSomething();
            }
            //Debug.Log("Interacting with " + transform.name);
        }
    }

    public virtual void Update()
    {
        checkInteraction();
    }

    void checkInteraction()
    {
        // If we are currently being focused
        // and we haven't already interacted with the object
        //if (isFocus && PlayerManager.instance.input.GetButtonDown(PlayerManager.instance.input.AButton))
        //{
        // Interact with the object
        //    Interact();

        //}

        // UNUSED FOR NOW //

        var player = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(interactionTransform.position, player.transform.position) <= interactionRadius)
        {
            OnFocused();
        }
        else
        { 
            OnDefocused();
        }

}

    // Called when the object starts being focused
    public virtual void OnFocused()
    {
        isFocus = true;
    }

    // Called when the object is no longer focused
    public virtual void OnDefocused()
    {
        isFocus = false;
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnFocused();
        }
    }

    void OnTriggerExit(Collider other)
    {
        OnDefocused();
    }
    */

    // Draw our radius in the editor
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
    }

}