using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    //Provide the build index of the scene to load
    public void LoadSceneByIndex(int sceneBuildIndex)
    {
        Debug.Log("Loading scene: " + SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
