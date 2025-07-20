using System;
using Marten.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreens : MonoBehaviour
{
    [SerializeField] private GameObject nextWaveScreen, deathScreen, winScreen;
    [SerializeField] private NextWaveScreen _nextWaveScreen;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameManager gameManager;
    
    public void OpenMenu(Menu menu)
    {
        Time.timeScale = 0;
        switch (menu)
        {
            case Menu.DeathScreen:
                TurnOnDeathScreen();
                break;
            case Menu.AfterWaveScreen:
                TurnOnNextWaveScreen();
                break;
            case Menu.WinScreen:
                TurnOnWinScreen();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(menu), menu, null);
        }
    }
    
    private void TurnOnDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    
    private void TurnOnNextWaveScreen()
    {
        _nextWaveScreen.RerollItems();
        _nextWaveScreen.ResetRerolls();
        _nextWaveScreen.CheckRerollPurchaseablity();
        nextWaveScreen.SetActive(true);
    }
    
    private void TurnOnWinScreen()
    {
        winScreen.SetActive(true);
    }
    
    public void WinContinueButton()
    {
        winScreen.SetActive(false);
        nextWaveScreen.SetActive(true);
    }

    public void ContinueButton()
    {
        nextWaveScreen.SetActive(false);
        playerInventory.CalculateNewStats();
        playerStats.CompleteHeal();
        gameManager.StartNewWave();
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene((int)Scenes.Level);
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }
}
