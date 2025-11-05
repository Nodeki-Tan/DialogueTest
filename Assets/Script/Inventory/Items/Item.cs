
using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;              // Item icon
    public bool isDefaultItem = false;      // Is the item default wear?
	public Vector2 size = Vector2.one;
	
    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
	
	public void Drop(){
		GameObject drop = Instantiate(new GameObject(name), GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
		
		drop.AddComponent<Rigidbody2D>();
		drop.AddComponent<CircleCollider2D>();
		drop.AddComponent<SpriteRenderer>();
		drop.AddComponent<ItemPickup>();
		
		drop.GetComponent<ItemPickup>().item = this;
		drop.GetComponent<ItemPickup>().interactionTransform = drop.transform;
		drop.GetComponent<CircleCollider2D>().radius = 0.25f;
		drop.GetComponent<SpriteRenderer>().sprite = icon;
		drop.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
		drop.GetComponent<SpriteRenderer>().sortingOrder = 0;
		drop.GetComponent<Rigidbody2D>().freezeRotation = true;
		
	}

}