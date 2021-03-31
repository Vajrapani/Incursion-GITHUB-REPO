using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenericHealth : MonoBehaviour
{
    protected int health = 0;
    public int maxHealth = 100;
    protected bool isDead = false;

    public TextMeshProUGUI healthTextObject;
    protected string healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText = healthTextObject.text;
        healthTextObject.text = healthText + health.ToString();
    }

    public void TakeDamage (int damage)
    {
        if (!isDead)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }
            healthTextObject.text = healthText + health.ToString();
        }
        else
        {
            Debug.Log(gameObject.name + " is already dead!");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
