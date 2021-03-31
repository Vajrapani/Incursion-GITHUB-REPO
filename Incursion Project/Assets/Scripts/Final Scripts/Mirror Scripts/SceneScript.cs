using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SceneScript : NetworkBehaviour
{
    public Text logText;
    public PlayerManager playermanager;
    // Start is called before the first frame update
    void Start()
    {
    }

    [SyncVar(hook = nameof(OnNameChanged))]
    public string statusText = "Default message.";

    void OnNameChanged(string _Old, string _New)
    {
        logText.text = statusText;
    }
    public void ButtonSendMessage()
    {
     
        playermanager = FindObjectOfType<PlayerManager>();
        playermanager.CmdSendPlayerMessage2();
        
    }


}
