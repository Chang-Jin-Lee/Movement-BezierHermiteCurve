using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameHelper : MonoBehaviour
{
    static public void QuitGame()
    {
        Application.Quit();
    }
    
    static public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1;
    }

    static public void GameClear(GameObject RestartPannel)
    {
        Time.timeScale = 0;
        RestartPannel.SetActive(true);
    }
}
