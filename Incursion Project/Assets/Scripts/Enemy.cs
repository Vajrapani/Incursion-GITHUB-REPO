using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;
    public Transform MyTarget {get; set;}

}
