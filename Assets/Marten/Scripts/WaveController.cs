using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private SpawnerPlane spawnerPlane;
    [SerializeField] private GameManager gameManager;
    
    private bool waveInProgress;
    
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
        StartCoroutine(SpawnOneInXSeconds(amount - 1, timePer));
    }

    public bool IsWaveInProgress()
    {
        return waveInProgress;
    }

    public void EndWave()
    {
        waveInProgress = false;
        gameManager.GetPlayer().GetComponent<Player>().TurnOnNextWaveScreen();
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            enemy.DeathNoCount();
        }
    }
}
