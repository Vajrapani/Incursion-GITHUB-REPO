using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardBehaviour : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void findBattleV3()
    {
        //PlayerManager playermanager = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        GameObject battlev3 = GameObject.Find("BattleV3");
        BattleV3 script = battlev3.GetComponent<BattleV3>();
        script.attackCard();
    }

    public void findBattleV3_Heal()
    {
        GameObject battlev3 = GameObject.Find("BattleV3");
        BattleV3 script = battlev3.GetComponent<BattleV3>();
        script.healCard();
    }
}
