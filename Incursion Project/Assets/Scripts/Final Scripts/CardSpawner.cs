using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Should be placed on the Cards parent object
public class CardSpawner : MonoBehaviour
{
    public GameObject[] cards;
    private List<int> cardIndecies;
    public Transform[] cardPositions;
    BattleSystem2 battleSystem2;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem2 = FindObjectOfType<BattleSystem2>();

        cardIndecies = new List<int>();
        for(int i = 0; i<cards.Length; i++)
        {
            cardIndecies.Add(i);
        }

        for(int t = 0; t<cardPositions.Length; t++)
        {
            if (cardIndecies.Count > 0)
            {
                int indexOfCardToSpawn = ChooseCardToSpawn();
                GameObject card = Instantiate(cards[indexOfCardToSpawn], cardPositions[t].position, Quaternion.identity, this.gameObject.transform);
            }
            //Button button = card.GetComponent<Button>();
        }
    }

    int ChooseCardToSpawn()
    {
        int indexOfCardToSpawn = cardIndecies[Random.Range(0, cardIndecies.Count)];
        cardIndecies.Remove(indexOfCardToSpawn);
        return indexOfCardToSpawn;
    }
}