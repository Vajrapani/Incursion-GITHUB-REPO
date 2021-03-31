using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAttack : MonoBehaviour
{
    public string defaultActorToDamage;
    public int basicDamage = 10;

    public void BasicAttack(string actorTag)
    {
        DoDamage(actorTag, basicDamage);
    }
    void DoDamage(string actorTag, int damage)
    {

        GameObject[] actorsToDamage = GameObject.FindGameObjectsWithTag(actorTag);
        foreach(GameObject actor in actorsToDamage)
        {
            actor.GetComponent<GenericHealth>().TakeDamage(damage);
        }
    }
}
