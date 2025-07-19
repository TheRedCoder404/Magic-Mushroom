using System.Collections;
using Marten.Scripts;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private SpawnerPlane spawnerPlane;
    [SerializeField] private GameManager gameManager;
    
    private GameScreens gameScreens;
    private bool waveInProgress;
    
    private void Awake()
    {
        gameScreens = gameManager.GetGameScreens();
        waveInProgress = false;
    }
    
    public void StartNewWave(int amount, int amountOverTime, int timeLimit)
    {
        waveInProgress = true;
        
        for (int i = 0; i < amount; i++)
        {
            spawnerPlane.SpawnAtRandomPosition();
        }
        
        StartCoroutine(SpawnOneInXSeconds(amountOverTime, (((int)(timeLimit * 0.8)) / (amountOverTime + 1))));
        
        gameManager.SetEnemiesAlive(amount);
    }

    private IEnumerator SpawnOneInXSeconds(int amount, int timePer)
    {
        spawnerPlane.SpawnAtRandomPosition();
        yield return new WaitForSeconds(timePer);
        if (amount > 1) StartCoroutine(SpawnOneInXSeconds(amount - 1, timePer));
    }

    public bool IsWaveInProgress()
    {
        return waveInProgress;
    }

    public void EndWave()
    {
        waveInProgress = false;
        gameScreens.OpenMenu(Menu.AfterWaveScreen);
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            enemy.DeathNoCount();
        }
    }
}
