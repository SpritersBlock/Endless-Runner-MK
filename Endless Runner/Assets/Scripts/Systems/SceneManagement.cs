using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //In a full game, a scene manager would be used to move from scene to scene.
    //This project just uses this script to reset the scene, though.
    //(This script is called "SceneManagement" to avoid a conflict with Unity's SceneManager)

    public static SceneManagement instance;

    private void Awake()
    {
        // vvv Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        // ^^^ Singleton
    }

    public void GoToScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
