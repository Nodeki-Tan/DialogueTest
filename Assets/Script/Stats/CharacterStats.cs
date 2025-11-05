using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour
{

    // Default stats, DO NOT DELETE
    // Health
    public Stat maxHealth;
    protected int currentHealth;

    // Damage
    public Stat damage;


    // Extra stats
    public Stat speed;
    public Stat stamina;

    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        maxHealth.BaseValue = 20;
        currentHealth = maxHealth.BaseValue;   

        maxHealth.type = ModifierType.Life;
        damage.type = ModifierType.Damage;

        // Here we setup the stats to each category
        speed.type = ModifierType.Speed;
        stamina.type = ModifierType.Stamina;

    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Unused, could be used anyway
        // Subtract the armor value
        //damage -= armor.GetValue();


        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }

}