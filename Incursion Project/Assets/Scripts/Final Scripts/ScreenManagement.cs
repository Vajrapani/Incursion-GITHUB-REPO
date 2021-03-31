using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenManagement : MonoBehaviour
{
    //Loads the scene with the build index of 1 above the current scene
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads the scene by using its name
    public void LoadSceneByName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
