using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Who to damage, via their specific instance of the Health script, and by how much
    public void DoDamage(Health characterToDamage, float damage)
    {
        characterToDamage.TakeDamage(damage);
    }
}
