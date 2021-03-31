using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private BattleSystem2 battleSystem2;
    private BattleSystem2KhalidTest battleSystem2KhalidTest;
    private string cardName;
    private string description;
    public int healAmount = 0;
    public int damageAmount = 0;
    public int energyCost = 0;

    void Start()
    {
        battleSystem2 = FindObjectOfType<BattleSystem2>();
        battleSystem2KhalidTest = FindObjectOfType<BattleSystem2KhalidTest>();
    }

    public void attackCard1()
    {
        battleSystem2.attackCard1();
    }

     void TakeAction()
    {
        //Call PlayerAttack1 from battleSystem2 using the Card's parameters
        //StartCoroutine(battleSystem2KhalidTest.PlayerAttack1(this.energyCost, this.damageAmount));

        //Call a healing method in battleSystem2

    }
}
