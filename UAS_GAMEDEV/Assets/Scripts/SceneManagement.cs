using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    private void Awake()
    {
        instance = this;
    }

    public void loadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void exitApp()
    {
        Application.Quit();
    }
}
