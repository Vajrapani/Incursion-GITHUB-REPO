using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST};

public class BattleSystem : MonoBehaviour
{
    public GameObject player1;
    public GameObject enemyPrefab;

    public Transform playerBattlestation;
    public Transform enemyBattlestation;

    Player PlayerInfo;
    Enemy EnemyInfo;

    public Text logText;
    public Text player1Health;
    public Text monsterHealth;
    public Text energy;

    public BattleState state;
    //
    
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(setupBattle());
    }

    IEnumerator setupBattle()
    {
        GameObject PlayerGo =  Instantiate(player1, playerBattlestation);
        PlayerInfo = PlayerGo.GetComponent<Player>();

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattlestation);
        EnemyInfo = EnemyGO.GetComponent<Enemy>();

        logText.text = "You are fighting a : " + EnemyInfo.unitName;
        player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
        monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
        energy.text = "PLAYER ENERGY : " + PlayerInfo.playerEnergy;
        

        yield return new WaitForSeconds(2f); // waits for 2 seconds
        state = BattleState.PLAYERTURN;
        playerTurn();

    }

    IEnumerator PlayerAttack1()
    {   //card 1 effects = enemy is hit for 5 damage, player energy is reduced by 2.
        if (PlayerInfo.playerEnergy >= 2)
        {
            EnemyInfo.currentHP -= 5;
            PlayerInfo.playerEnergy -= 2;
            monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
            energy.text = "PLAYER ENERGY : " + PlayerInfo.playerEnergy;
            logText.text = "You hit the monster for 5 damage!";
            yield return new WaitForSeconds(2f);
        }

        //check is enemy is dead and change state based off of that
        if (EnemyInfo.currentHP <= 0 )
        {
            state = BattleState.WON;
            // end battle function here - transition to story scene TODO
            EndBattle();

        } else if(PlayerInfo.playerEnergy <= 0)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerAttack2()
    {   //card 1 effects = enemy is hit for 10 damage, player energy is reduced by 5.
        if (PlayerInfo.playerEnergy >= 5)
        {
            EnemyInfo.currentHP -= 10;
            PlayerInfo.playerEnergy -= 5;
            monsterHealth.text = "Monster HP : " + EnemyInfo.currentHP + " / " + EnemyInfo.maxHP;
            energy.text = "PLAYER ENERGY : " + PlayerInfo.playerEnergy;
            logText.text = "You hit the monster for 10 damage!";
            yield return new WaitForSeconds(2f);
        }

        //check is enemy is dead and change state based off of that
        if (EnemyInfo.currentHP <= 0)
        {
            state = BattleState.WON;
            // end battle function here - transition to story scene TODO
            EndBattle();

        }
        else if(PlayerInfo.playerEnergy <= 0)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal1()
    {   //heal by 5, energy reduced by 3
        if (PlayerInfo.playerEnergy >= 3)
        {
            PlayerInfo.playerCurrentHP += 5;
            PlayerInfo.playerEnergy -= 3;
            player1Health.text = "Player 1 HP : " + PlayerInfo.playerCurrentHP + " / " + PlayerInfo.playerMaxHP;
            energy.text = "PLAYER ENERGY : " + PlayerInfo.playerEnergy;
            logText.text = "You heal for 5 HP!";
            yield return new WaitForSeconds(2f);
        }
        //check is enemy is dead and change state based off of that
        if (EnemyInfo.currentHP <= 0)
        {
            state = BattleState.WON;
            // end battle function here - transition to story scene TODO
            EndBattle();

        }
        else if(PlayerInfo.playerEnergy <= 0)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

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
            state = BattleState.LOST;
            EndBattle();
           
        } else
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
    }

    //checks for enemy click
    private bool clickedEnemy(){
        if(EnemyInfo.MyTarget != null){
            return true;

        }
        else{
            return false;
        }
    }
    void selectedTarget(){
        if(clickedEnemy()!=true){
            logText.text = "Select target";

        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            logText.text = "You have won! ";
            //yield return new WaitForSeconds(3f);
            

        } else if(state == BattleState.LOST)
        {
            logText.text = "You have lost! ";
        }
    }

    void playerTurn()
    {
        logText.text = "Pick your cards to damage your enemy!";
        PlayerInfo.playerEnergy = 5;
        energy.text = "PLAYER ENERGY : " + PlayerInfo.playerEnergy;
    }

    public void attackCard1()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack1());
        
    }

    public void attackCard2()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack2());

    }

    public void healCard1()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal1());

    }
}