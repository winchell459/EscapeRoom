using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtility
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("loading scene " + currentIndex + 1);
        SceneManager.LoadScene((currentIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
