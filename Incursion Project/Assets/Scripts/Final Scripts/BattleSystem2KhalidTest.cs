using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

//public enum BATTLE_STATE { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem2KhalidTest : NetworkBehaviour
{
    // set up the players, enemies, battlestations and various other misc. things need for battle.
    public GameObject P1;
    public GameObject enemyPrefab;

    public Transform player1Battlestation;
    public Transform enemyBattlestation;

    Player PlayerInfo;
    Enemy EnemyInfo;

    public Text logText;
    public Text player1Health;
    public Text monsterHealth;
    public Text energy;
    bool clicked;

    public BATTLE_STATE state;
    // end of variable declarations

    void Start() // called before first frame update
    {
        state = BATTLE_STATE.START;
        StartCoroutine(setupBattle());
    }

    IEnumerator setupBattle()
    {
        GameObject PlayerGo = Instantiate(P1, player1Battlestation);
        PlayerInfo = PlayerGo.GetComponent<Player>();

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattlestation);
        EnemyInfo = EnemyGO.GetComponent<Enemy>();


        logText.text = "You are fighting a : " + EnemyInfo.unitName;
        player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
        monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
        energy.text = "ENERGY : " + PlayerInfo.playerEnergy;


        yield return new WaitForSeconds(2f); // waits for 2 seconds
        state = BATTLE_STATE.PLAYERTURN;
        playerTurn();
        
    } 

    IEnumerator EnemyTurn()
    {
        logText.text = "Enemy is attacking!";
        yield return new WaitForSeconds(1f);

        //hits player for 5 damage, update the HP bar
        int damage = Random.Range(5, 8);
        PlayerInfo.playerCurrentHP -= damage;
        player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
        logText.text = "You are hit for " + damage + " damage!";
        yield return new WaitForSeconds(1f);

        if (PlayerInfo.playerCurrentHP <= 0)
        {
            state = BATTLE_STATE.LOST;
            EndBattle();

        }
        else
        {
            state = BATTLE_STATE.PLAYERTURN;
            playerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BATTLE_STATE.WON)
        {
            logText.text = "You have won! ";
            //yield return new WaitForSeconds(3f);

        }
        else if (state == BATTLE_STATE.LOST)
        {
            logText.text = "You have lost! ";
        }
    }
    void enemySelection(){
        logText.text = "Select a Target";

    }

    void playerTurn()
    {
            logText.text = "Pick your cards to damage your enemy!";
            PlayerInfo.playerEnergy = 5;
            energy.text = "ENERGY : " + PlayerInfo.playerEnergy;
        
    }

    public void attackCard1()
    {
        if (state != BATTLE_STATE.PLAYERTURN && clicked == false)
            return;

        //StartCoroutine(PlayerAttack1());
    
    }
    
    public bool clickedEnemy()
    {
        
        if(EnemyInfo.MyTarget != null){
            clicked = true;
            }
        else{
            clicked = false;
        }
        return clicked;
    }


    public IEnumerator UseCard (int energyCost, int damageAmount, int healAmount)
    {
        //The player must have enough energy to use the card
        if(PlayerInfo.playerEnergy >= energyCost)
        {
            //Use the card

            //Use up the energy and update the UI
            PlayerInfo.playerEnergy -= energyCost;
            energy.text = "ENERGY : " + PlayerInfo.playerEnergy;
            //Attack using the card's damage attribute
            StartCoroutine(PlayerAttack1(damageAmount));
            //Heal using the card's healing attribute
            StartCoroutine(PlayerHeal(healAmount));

        }
        //If not then don't use the card 
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator PlayerAttack1(int damageAmount)
    {   
            EnemyInfo.currentHP -= damageAmount;
            monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
            
            logText.text = "You hit the monster for 5 damage!";
            yield return new WaitForSeconds(2f);
        

        //check is enemy is dead and change state based off of that
        if (EnemyInfo.currentHP <= 0)
        {
            state = BATTLE_STATE.WON;
            // end battle function here - transition to story scene TODO
            EndBattle();

        }
        else if (PlayerInfo.playerEnergy <= 0)
        {
            state = BATTLE_STATE.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //New method added for healing
    public IEnumerator PlayerHeal(int healAmount)
    {
        //If the player's health is below their max health they can be healed
        if (PlayerInfo.playerCurrentHP < PlayerInfo.playerMaxHP)
        {
            //Heal the player
            PlayerInfo.playerCurrentHP += healAmount;

            //Constrain player health to max health or below
            if (PlayerInfo.playerCurrentHP > PlayerInfo.playerMaxHP)
            {
                PlayerInfo.playerCurrentHP = PlayerInfo.playerMaxHP;
            }
        }
        yield return new WaitForSeconds(2f);
    }
} // end of BattleSystem2

//Note to Khalid:
/*
 * There should really be one method for using the card that deals with using up energy.
 * This method should then call two separate methods for attacking and healing
 */