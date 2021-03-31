using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;


public enum STATE { NULL, START, PLAYER1TURN, PLAYER2TURN, ENEMYTURN, WON, LOST };
public class BattleV3 : NetworkBehaviour
{
    public PlayerManager playermanager;

    //start of SyncVars
    [SyncVar]
    public STATE state;

    public GameObject P1;

    public GameObject P2;

    public GameObject enemy;
    public Enemy enemy_INFO;

    public GameObject enemy2;

    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    public GameObject cardPosition1;
    public GameObject cardPosition2;
    public GameObject cardPosition3;
    public GameObject cardPosition4;

    [SyncVar(hook = nameof(OnLogChanged))]
    public string logText = "Start of Battle";

    [SyncVar(hook = nameof(OnP1HealthChanged))]
    public string player1Health;

    [SyncVar(hook = nameof(OnP2HealthChanged))]
    public string player2Health;

    [SyncVar(hook = nameof(OnMonsterHealthChanged))]
    public string monsterHealth;

    [SyncVar(hook = nameof(OnEnergyChanged))]
    public string energy;

    [SyncVar]
    public int currentPlayerTurn = 1;
    //end of SyncVars

    //start of SyncVar hooks
    void OnLogChanged(string _Old, string _New) { logText_UI.text = logText; }
    void OnP1HealthChanged(string _Old, string _New) { player1Health_UI.text = player1Health; }
    void OnP2HealthChanged(string _Old, string _New) { player2Health_UI.text = player2Health; }
    void OnMonsterHealthChanged(string _Old, string _New) { monsterHealth_UI.text = monsterHealth; }
    void OnEnergyChanged(string _Old, string _New) { energy_UI.text = energy; }

   
    // end of SyncVar hooks

    //public GameObject player1;
    //public GameObject player2;
    //public GameObject enemy;

    // drag and drop objects
    public Text logText_UI;
    public Text player1Health_UI;
    public Text player2Health_UI;
    public Text monsterHealth_UI;
    public Text energy_UI;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
       
    }

    public void spawn()
    {
        GameObject P1_GO = Instantiate(P1, new Vector2(-8, -3), Quaternion.identity);
        P1_GO.name = "player1";
        NetworkServer.Spawn(P1_GO);

        GameObject P2_GO = Instantiate(P2, new Vector2(-5, -3), Quaternion.identity);
        P2_GO.name = "player2";
        NetworkServer.Spawn(P2_GO);

        GameObject enemy_GO = Instantiate(enemy, new Vector2(0, 1), Quaternion.identity);
        enemy_GO.name = "orc";
        NetworkServer.Spawn(enemy_GO);
    }


    public void setupBattle()
    {
        playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        //playermanager = FindObjectOfType<PlayerManager>();
       
        state = STATE.START;
        playermanager.CmdSetupBattle();
        Debug.Log("Net ID : ");
        Debug.Log(playermanager.returnNetID());
        state = STATE.PLAYER1TURN;
        playermanager.CmdPlayer1Turn();
        //attackCard();
        //spawn in card here, player who presses the 'Start Battle' button will have authority over the card.
    }


    //card attack method, this will use a method from PlayerManager that does all the heavy lifting, then do a state transition at the end.
    public void attackCard()
    {
        playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        if(state == STATE.PLAYER1TURN && playermanager.returnNetID() == 9) // if it's player 1's turn and it's player 1 clicking on it (the host.)
        {
            //playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
            playermanager.CmdAttackCard1();
        }
         
        if(state == STATE.PLAYER2TURN && playermanager.returnNetID() == 10) // if it's player 2's turn and it's player 2 clicking on it (the connected client.)
        {
            //playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
            playermanager.CmdAttackCard2();
        }

    }
    // end of attack card method

    public void healCard()
    {
        playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();

        if (state == STATE.PLAYER1TURN && playermanager.returnNetID() == 9) // if it's player 1's turn and it's player 1 clicking on it (the host.)
        {
            playermanager.CmdHealCard1();
            
        }

        if (state == STATE.PLAYER2TURN && playermanager.returnNetID() == 10) // if it's player 2's turn and it's player 2 clicking on it (the connected client.)
        {
            playermanager.CmdHealCard2();
        }
    }

    public void spawnCard()
    {
        GameObject card_GO1 = Instantiate(card1, new Vector2(-3, 1), Quaternion.identity);
        card_GO1.name = "card1";
        NetworkServer.Spawn(card_GO1);

        GameObject card_GO2 = Instantiate(card2, new Vector2(-5, 1), Quaternion.identity);
        card_GO2.name = "card2";
        NetworkServer.Spawn(card_GO2);

        GameObject card_GO3 = Instantiate(card3, new Vector2(-3, -2), Quaternion.identity);
        card_GO3.name = "card3";
        NetworkServer.Spawn(card_GO3);

        GameObject card_GO4 = Instantiate(card4, new Vector2(-5, -2), Quaternion.identity);
        card_GO4.name = "card4";
        NetworkServer.Spawn(card_GO4);
    }

    public void EndTurn()
    {
        if (state == STATE.PLAYER1TURN || state == STATE.PLAYER2TURN)
        {
            playermanager.CmdEndTurn();
            logText = "Next turn!";
        }

        if(state == STATE.ENEMYTURN || state == STATE.WON)
        {
            playermanager.CmdPlayer1Turn();
        }

       /* if(state == STATE.WON)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       */
    } 

    public void spawnNewEnemy(GameObject oldEnemy)
    {
        NetworkServer.Destroy(oldEnemy);
        GameObject enemy_GO = Instantiate(enemy2, new Vector2(0, 1), Quaternion.identity);
        enemy_GO.name = "orc";
        NetworkServer.Spawn(enemy_GO);

    }

    public void destroyEnemy()
    {
        enemy = GameObject.Find("orc");
        enemy_INFO = enemy.GetComponent<Enemy>();
        NetworkServer.Destroy(enemy);
    }
} // end of BattleV3
