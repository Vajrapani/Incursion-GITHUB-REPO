using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    public bool isPlayer1Current = true;

    public GameObject player1;
    public GameObject player2;
    public GameObject monster;

    public float energy = 0f;
    public float maxEnergy = 0f;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseEnergy(float energyToUse)
    {
        energy -= energyToUse;
        if(energy <= 0f)
        {
            energy = 0f;
        }
    }
}
