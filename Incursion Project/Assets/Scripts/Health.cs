using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool isDead = false;

    public bool canReduceDamage = false;
    public float damageReduction = 0f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Heal(float healingAmount)
    {
        health += healingAmount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        //For when the player has any damage reduction (dR > 0)
        //and this can just be 0.0 if they have none or can't reduce damage (e.g. Player 1)
        if (canReduceDamage)
        {
            damage -= damageReduction;
        }

        health -= damage;
        if(health <= 0f)
        {
            health = 0f;
            isDead = true;
        }
    }
}
