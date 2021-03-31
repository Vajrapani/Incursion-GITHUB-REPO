using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SendMessage : NetworkBehaviour
{
    private SceneScript sceneScript;
    // Start is called before the first frame update
    void Awake()
    {
        sceneScript = GameObject.FindObjectOfType<SceneScript>();
    }

    [Command]
    public void changeLogMessage()
    {
        if (sceneScript)
        {
            sceneScript.statusText = "Synced the log";
        }
    }
}
