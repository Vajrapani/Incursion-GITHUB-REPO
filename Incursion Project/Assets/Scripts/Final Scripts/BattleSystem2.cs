using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public enum BATTLE_STATE { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem2 : NetworkBehaviour
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
    public bool clicked;
    
    //public int poisonedTurns = 0;

    public HealthBar healthBar; //player healthbar 
    public HealthBar monsterBar;  //monster healthbar

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
         
        //Old code for displaying text of player and monster HP
        //player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
        //monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;

        //Setting MaxHP for both Monster and Player
        healthBar.SetMaxHealth(PlayerInfo.playerMaxHP);
        monsterBar.SetMaxHealth(EnemyInfo.maxHP);
        energy.text = "ENERGY : " + PlayerInfo.playerEnergy;

        //poisonedTurns == 0;


        yield return new WaitForSeconds(2f); // waits for 2 seconds
        state = BATTLE_STATE.PLAYERTURN;
        playerTurn();
        
    } 

    IEnumerator EnemyTurn()
    {
        //if (poisonedTurns > 0)
        //{
            //EnemyInfo.currentHP -= 3;
            //poisonedTurns--;
            //logText.text = "Enemy takes 3 damage from poison!";
            //yield return new WaitForSeconds(1f);
            //if (poisonedTurns == 0)
            //{
                //logText.text = "Poison has worn off.";
                //yield return new WaitForSeconds(1f);
            //}
        //}
        logText.text = "Enemy is attacking!";
        yield return new WaitForSeconds(1f);

        //hits player for 5 damage, update the HP bar
        int damage = Random.Range(5, 8);
        PlayerInfo.playerCurrentHP -= damage;
        //Old code for changing text when player takes damage
        //player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
        //Change HealthBar slider for player "healthbar" when taking damage
        healthBar.SetHealth(PlayerInfo.playerCurrentHP);
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

        StartCoroutine(PlayerAttack1());
    
    }
    
    public bool clickedEnemy()
    {
        //checks if there is a target
        if(EnemyInfo.MyTarget != null){
            clicked = true;
            }
        else{
            clicked = false;
        }
        return clicked;
 
    }


    IEnumerator PlayerAttack1()
    {   //card 1 effects = enemy is hit for 5 damage, player energy is reduced by 2.
        if (PlayerInfo.playerEnergy >= 2 )
        {
            EnemyInfo.currentHP -= 5;
            PlayerInfo.playerEnergy -= 5;
            //Old code for changing text when monster takes damage
            //monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
            //Change HealthBar slider for monster "monsterBar" when taking damage
            monsterBar.SetHealth(EnemyInfo.currentHP);
            energy.text = "ENERGY : " + PlayerInfo.playerEnergy;
            logText.text = "You hit the monster for 5 damage!";
            yield return new WaitForSeconds(2f);
        }

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

    /* IEnumerator PlayerAttackDamageOverTime()
     * {
     *      if (PlayerInfo.playerEnergy >= 3) 
     *      {
     *          PlayerInfo.playerEnergy -= 3;
     *          poisonedTurns = 3;
     *          energy.text = "ENERGY : " + PlayerInfo.playerEnergy;
     *          logText.text = "You poisoned the monster!";
     *          yield return new WaitForSeconds(2f);
     *      }
     * }
     *   
     */
} // end of BattleSystem2
