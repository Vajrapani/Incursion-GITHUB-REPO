using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCard : MonoBehaviour
{
    public int healAmount = 0;
    public int damageAmount = 0;
    public int energyCost = 0;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("MainGameManager").GetComponent<GameManager>();
    }

    public void Action()
    {
        Health player1Health = gameManager.player1.GetComponent<Health>();
        Attack player1Attack = gameManager.player1.GetComponent<Attack>();

        Health monsterHealth = gameManager.monster.GetComponent<Health>();

        //Heal if player 1 or reduce damage if player 2
        if (gameManager.isPlayer1Current)
        {
            player1Health.Heal(healAmount);
            player1Attack.DoDamage(monsterHealth, damageAmount);
        }
        //Direct damage to monster if player 1 but damage over time if player 2
    }
}
