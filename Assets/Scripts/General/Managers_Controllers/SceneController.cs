using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    [SerializeField] private static string mainMenu = "MainMenu";
    [SerializeField] private static string gameScene = "GameScene";
    [SerializeField] private static string tutorialScene = "Tutorial";

    public static void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public static void PlayTutorial()
    {
        SceneManager.LoadScene(tutorialScene);
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public static void ExitGame()
    {
        Debug.Log("GAME EXIT");
        Application.Quit();
    }
}