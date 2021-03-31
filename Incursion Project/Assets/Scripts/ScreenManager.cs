using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenManager : MonoBehaviour
{
    public void ContinueToNextScene()
    {
        Debug.Log("registered");
        int sceneIndexToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if(sceneIndexToLoad < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndexToLoad);
        } else
        {
            Debug.Log("No more screens left!");
        }
        
    }
}
