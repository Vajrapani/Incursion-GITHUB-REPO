using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    [SerializeField]
    private Enemy player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gets layermask value of clickable layer
        //Debug.Log(LayerMask.GetMask("Clickable"));
        ClickTarget();
    }
    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,256);

            if (hit.collider !=null)
            {
                //detects the targets position
                player.MyTarget = hit.transform;
                print("clicked");
            }
            else{
               // player.MyTarget = null;
            }
        }
    }
}
