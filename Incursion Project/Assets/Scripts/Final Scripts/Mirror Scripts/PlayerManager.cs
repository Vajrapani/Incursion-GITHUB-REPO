using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerManager : NetworkBehaviour
{
    public SceneScript sceneScript;
    public BattleV3 battlev3;
    //game object fields
    public GameObject player1;
    public GameObject player2;
    public GameObject enemy;

    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    //cardPosition and card stuff
    public GameObject cardPosition1;
    public GameObject cardPosition2;
    public GameObject cardPosition3;
    public GameObject cardPosition4;
    //public GameObject card1;

    //player info and enemy info fields
    public Player player1_INFO;
    public Player player2_INFO;
    public Enemy enemy_INFO;
   
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server Started.");
        Debug.Log(returnNetID());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Client Started.");
        //Debug.Log(returnNetID());
        sceneScript = FindObjectOfType<SceneScript>();
        battlev3 = FindObjectOfType<BattleV3>();

    }

    public uint returnNetID() // find the netID of the client who calls this. Host = 6, Client = 7
    {
        uint netID = NetworkClient.connection.identity.netId;
        return netID;
    }

   
    // Start is called before the first frame update
    void Start()
    {
    }
   

    [Command]
    public void CmdSetupBattle()
    {
        if (battlev3)
        {
            battlev3.spawn();
            //find player and enemy objects
            player1 = GameObject.Find("player1");
            player2 = GameObject.Find("player2");
            enemy = GameObject.Find("orc");
            //get their player and enemy script components
            player1_INFO = player1.GetComponent<Player>();
            player2_INFO = player2.GetComponent<Player>();
            enemy_INFO = enemy.GetComponent<Enemy>();

            battlev3.spawnCard();
            card1 = GameObject.Find("card1");
            card2 = GameObject.Find("card2");
            card3 = GameObject.Find("card3");
            card4 = GameObject.Find("card4");

            cardPosition1 = GameObject.Find("cardPosition1");
            cardPosition2 = GameObject.Find("cardPosition2");
            cardPosition3 = GameObject.Find("cardPosition3");
            cardPosition4 = GameObject.Find("cardPosition4");

            RpcShowCards(
                card1, cardPosition1,
                card2, cardPosition2,
                card3, cardPosition3,
                card4, cardPosition4);
            //do the RpcShowCards for all 4 spawned in cards
            //reset player current HPs to max HPs, likewise with enemies if respawning


            battlev3.logText = "You are fighting a : " + enemy_INFO.unitName + " ! ";
            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            battlev3.monsterHealth = "Monster HP : " + enemy_INFO.currentHP;
            battlev3.energy = "Player 1 ENERGY : " + player1_INFO.playerEnergy;
        }
    }

    [Command]
    public void CmdPlayer1Turn()
    {
        if (battlev3)
        {
            battlev3.state = STATE.PLAYER1TURN;
            player1 = GameObject.Find("player1");
            player1_INFO = player1.GetComponent<Player>();
            player1_INFO.playerEnergy = 10;

            battlev3.logText = "Player 1 Turn!";
            

        }
    }

    [ClientRpc]
    public void RpcShowCards(
        GameObject card1, GameObject cardPosition1,
        GameObject card2, GameObject cardPosition2,
        GameObject card3, GameObject cardPosition3,
        GameObject card4, GameObject cardPosition4)
    {
        card1.GetComponent<RectTransform>().SetParent(cardPosition1.transform, false);
        card2.GetComponent<RectTransform>().SetParent(cardPosition2.transform, false);
        card3.GetComponent<RectTransform>().SetParent(cardPosition3.transform, false);
        card4.GetComponent<RectTransform>().SetParent(cardPosition4.transform, false);


    }

    //basic attack card method. Takes 10 energy to do 10 damage
    [Command]
    public void CmdAttackCard1() // different players get their different energy values changed.
    {
        //player1 = GameObject.Find("player1");
        //player1_INFO = player1.GetComponent<Player>();

        enemy = GameObject.Find("orc");
        enemy_INFO = enemy.GetComponent<Enemy>();

        if (player1_INFO.playerEnergy >= 10)
        {
            player1 = GameObject.Find("player1");
            player1_INFO = player1.GetComponent<Player>();

            player2 = GameObject.Find("player2");
            player2_INFO = player2.GetComponent<Player>();

            enemy = GameObject.Find("orc");
            enemy_INFO = enemy.GetComponent<Enemy>();

           int damage = Random.Range(10, 20);

            enemy_INFO.currentHP -= damage;
            player1_INFO.playerEnergy -= 10;
            battlev3.logText = "Enemy hit for " + damage + " damage!";
            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.energy = "Player 1 ENERGY : " + player1_INFO.playerEnergy;
            battlev3.monsterHealth = "Monster HP : " + enemy_INFO.currentHP;
        }
        else
        {
            battlev3.logText = "Not enough energy.";
        }

        //check for enemy HP is less than 0
        if (enemy_INFO.currentHP <= 0)
        {
            battlev3.state = STATE.WON;
            battlev3.logText = "You have beaten the enemy!";
            RpcSpawnNewEnemy();
            player1 = GameObject.Find("player1");
            player1_INFO = player1.GetComponent<Player>();

            player2 = GameObject.Find("player2");
            player2_INFO = player2.GetComponent<Player>();

            player1_INFO.playerCurrentHP = 30;
            player2_INFO.playerCurrentHP = 30;

            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            //enemy respawn method here?
            //if enemy respawn, then refill player 1 and 2 health here as well
        }
        //

    }
    //

    [Command]
    public void CmdAttackCard2()
    {
        player2 = GameObject.Find("player2");
        player2_INFO = player2.GetComponent<Player>();

        enemy = GameObject.Find("orc");
        enemy_INFO = enemy.GetComponent<Enemy>();

        if (player2_INFO.playerEnergy >= 10)
        {
            player2 = GameObject.Find("player2");
            player2_INFO = player2.GetComponent<Player>();

            enemy = GameObject.Find("orc");
            enemy_INFO = enemy.GetComponent<Enemy>();
            int damage = Random.Range(10, 20);

            enemy_INFO.currentHP -= damage;
            player2_INFO.playerEnergy -= 10;
            battlev3.logText = "Enemy hit for " + damage + " damage!";
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            battlev3.energy = "Player 2 ENERGY : " + player2_INFO.playerEnergy;
            battlev3.monsterHealth = "Monster HP : " + enemy_INFO.currentHP;

        }
        else
        {
            battlev3.logText = "Not enough energy.";
        }

        //check for enemy HP is less than 0
        if(enemy_INFO.currentHP <= 0)
        {
            battlev3.state = STATE.WON;
            battlev3.logText = "You have beaten the enemy!";
            RpcSpawnNewEnemy();
            player1 = GameObject.Find("player1");
            player1_INFO = player1.GetComponent<Player>();

            player2 = GameObject.Find("player2");
            player2_INFO = player2.GetComponent<Player>();

            player1_INFO.playerCurrentHP = 30;
            player2_INFO.playerCurrentHP = 30;

            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            //enemy respawn method here?
            //if enemy respawn, then refill player 1 and 2 health here as well
        }
        //
    }
    [Command]
    public void CmdHealCard1() // player 1 heal method, 10 energy for 10 hp
    {
        player1 = GameObject.Find("player1");
        player1_INFO = player1.GetComponent<Player>();

        player2 = GameObject.Find("player2");
        player2_INFO = player2.GetComponent<Player>();

        if (player1_INFO.playerEnergy >= 10)
        {
            player1_INFO.playerCurrentHP += 10;
            player2_INFO.playerCurrentHP += 10;

            player1_INFO.playerEnergy -= 10;
            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            battlev3.energy = "Player 1 ENERGY : " + player1_INFO.playerEnergy;
            battlev3.logText = "Players healed!";
        }
        else
        {
            battlev3.logText = "Not enough energy.";
        }
    }

    [Command]
    public void CmdHealCard2() // player 2 heal method, 10 energy for 10 hp
    {
        player1 = GameObject.Find("player1");
        player1_INFO = player1.GetComponent<Player>();

        player2 = GameObject.Find("player2");
        player2_INFO = player2.GetComponent<Player>();

        if (player2_INFO.playerEnergy >= 10)
        {
            player1_INFO.playerCurrentHP += 10;
            player2_INFO.playerCurrentHP += 10;

            player2_INFO.playerEnergy -= 10;
            battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
            battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
            battlev3.energy = "Player 2 ENERGY : " + player2_INFO.playerEnergy;
            battlev3.logText = "Players healed!";
        }
        else
        {
            battlev3.logText = "Not enough energy.";
        }
    }
    [Command]
    public void CmdEndTurn()
    {
        if(battlev3.state == STATE.PLAYER1TURN)
        {
            player2 = GameObject.Find("player2");
            player2_INFO = player2.GetComponent<Player>();
            player2_INFO.playerEnergy = 10;

            battlev3.state = STATE.PLAYER2TURN;
            battlev3.energy = "Player 2 ENERGY : " + player2_INFO.playerEnergy;
            return;

        }

        if (battlev3.state == STATE.PLAYER2TURN)
        {
            battlev3.state = STATE.ENEMYTURN;
            battlev3.logText = "Enemy Turn";
            EnemyTurn();
        }


    }

    
    public void EnemyTurn()
    {
        Debug.Log("Enemy turn method triggered.");
        enemy = GameObject.Find("orc");
        enemy_INFO = enemy.GetComponent<Enemy>();

        player1 = GameObject.Find("player1");
        player1_INFO = player1.GetComponent<Player>();

        player2 = GameObject.Find("player2");
        player2_INFO = player2.GetComponent<Player>();

        if (enemy) // change it up when we have multiple potential enemies
        {
            battlev3.logText = enemy_INFO.name + " is attacking!";
            int damage = Random.Range(10, 20);
            int playerTarget = Random.Range(1, 2);

            switch (playerTarget)
            {
                case 1:
                    player1 = GameObject.Find("player1");
                    player1_INFO = player1.GetComponent<Player>();

                    player1_INFO.playerCurrentHP -= damage;
                    battlev3.logText = "Player 1 hit for " + damage + " damage!";
                    battlev3.player1Health = "Player 1 HP : " + player1_INFO.playerCurrentHP;
                    //CmdPlayer1Turn();
                    
                    break;
                    //return;
                case 2:
                    player2 = GameObject.Find("player2");
                    player2_INFO = player2.GetComponent<Player>();

                    player2_INFO.playerCurrentHP -= damage;
                    battlev3.logText = "Player 2 hit for " + damage + " damage!";
                    battlev3.player2Health = "Player 2 HP : " + player2_INFO.playerCurrentHP;
                    //CmdPlayer1Turn();

                    break;
                    //return;
            }
        } // end of if statement

        // check if both player HPs are less than 0
        if (player1_INFO.playerCurrentHP <= 0 || player2_INFO.playerCurrentHP <= 0)
        {
            battlev3.state = STATE.LOST;
            battlev3.logText = "You have Lost!";
        }
        //

    } // end of enemy turn


    [Command]
    public void CmdSendPlayerMessage2()
    {
        if (battlev3)
        {
            battlev3.logText = "Card Pressed!";
        }
    }

  [ClientRpc]
    public void RpcSpawnNewEnemy()
    {
        if (battlev3)
        {
            battlev3.spawnNewEnemy(enemy);
            enemy = GameObject.Find("orc");
            enemy_INFO = enemy.GetComponent<Enemy>();
            battlev3.monsterHealth = "Monster HP : " + enemy_INFO.currentHP;
        }
    }
}
