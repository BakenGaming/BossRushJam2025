using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private bool isMainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject healthBar;

    private void OnEnable() 
    {
        if (isMainMenu) InitializeMainMenu();
    }
    private void OnDisable() 
    {

    }
    public void Initialze()
    {
        healthBar.GetComponent<Slider>().value = GameManager.i.GetPlayerGO().GetComponent<IHandler>().GetHealthSystem().GetHealthPercentage();
    }
    private void InitializeMainMenu()
    {
        GetComponent<VolumeSettings>().Initialize();        
        if(!isMainMenu) pauseMenu.SetActive(false);
        else creditsScreen.SetActive(false);
        
        settingsMenu.SetActive(false);
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.i.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        GetComponent<VolumeSettings>().SettingsMenuOpened();
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCreditsScreen()
    {
        creditsScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneController.StartGame();
    }

    public void RestartGame()
    {
        SceneController.StartGame();
    }
    public void BackToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }

}
