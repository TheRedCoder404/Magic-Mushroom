using System;
using Marten.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreens : MonoBehaviour
{
    [SerializeField] private GameObject nextWaveScreen, deathScreen, winScreen;
    [SerializeField] private Player player;
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
        player.movement = true;
    }
    
    private void TurnOnNextWaveScreen()
    {
        nextWaveScreen.SetActive(true);
        player.movement = true;
    }
    
    private void TurnOnWinScreen()
    {
        winScreen.SetActive(true);
        player.movement = true;
    }
    
    public void WinContinueButton()
    {
        winScreen.SetActive(false);
        nextWaveScreen.SetActive(true);
    }

    public void ContinueButton()
    {
        nextWaveScreen.SetActive(false);
        player.RefreshHealth();
        gameManager.StartNewWave();
        player.movement = true;
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
