using System;
using System.Collections;
using Marten.Scripts;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player, enemyPrefab;
    [SerializeField] private GameScreens gameScreens;
    [SerializeField] private WaveController waveController;
    [SerializeField] private int firstWaveSpawnAmount = 5, firstWaveExtraSpawnAmount = 5, timeBeforeWave = 5, firstWaveTimeLimit = 10;
    [SerializeField] private int minNewWaveSpawnAmount = 5, maxNewWaveSpawnAmount = 15;
    [SerializeField] private int minNewWaveExtraSpawnAmount = 5, maxNewWaveExtraSpawnAmount = 50;
    [SerializeField] private int winAfterMinWaves = 20, winAllXWaves = 5;
    [SerializeField] private float newWaveSpawnAmountMultiplier = 1.1f, newWaveExtraSpawnAmountMultiplier = 1.2f;

    private int enemiesAlive, countDownTime, nextWaveSpawnAmount, nextWaveExtraSpawnAmount, nextCountDownTime, _currentWave;

    public int currentWave
    {
        get { return _currentWave; }
        private set
        {
            _currentWave = value; 
            player.GetComponent<Player>().UpdateWaveCounterText(_currentWave);
            if (_currentWave == winAfterMinWaves || ((_currentWave > winAfterMinWaves) && (_currentWave % winAllXWaves == 0)))
            {
                gameScreens.OpenMenu(Menu.WinScreen);
            }
        }
    }
    
    private Color beforeWaveColor = new Color(0, 255, 0);
    private Color duringWaveColor = new Color(255, 0, 0);

    private void Start()
    {
        countDownTime = timeBeforeWave;
        nextCountDownTime = firstWaveTimeLimit;
        StartCoroutine(DecreaseCountDown());
        nextWaveSpawnAmount = firstWaveSpawnAmount;
        nextWaveExtraSpawnAmount = firstWaveExtraSpawnAmount;
        currentWave = 1;
    }

    private IEnumerator DecreaseCountDown()
    {
        yield return new WaitForSeconds(1);
        countDownTime--;
        if (countDownTime <= 0)
        {
            if (waveController.IsWaveInProgress())
            {
                EndWave();
            }
            else
            {
                NewWave();
            }
        } 
        else
        {
            StartCoroutine(DecreaseCountDown());
        }
    }

    private void EndWave()
    {
        player.GetComponent<Player>().ChangeWaveCountColor(beforeWaveColor);
        waveController.EndWave();
    }

    private void NewWave()
    {
        waveController.StartNewWave(nextWaveSpawnAmount, nextWaveExtraSpawnAmount, nextCountDownTime);
        countDownTime = nextCountDownTime;
        player.GetComponent<Player>().ChangeWaveCountColor(duringWaveColor);
        StartCoroutine(DecreaseCountDown());
    }
    
    public void StartNewWave()
    {
        currentWave++;
        nextWaveSpawnAmount = Math.Clamp((int)(nextWaveSpawnAmount * newWaveSpawnAmountMultiplier), minNewWaveSpawnAmount, maxNewWaveSpawnAmount);
        nextWaveExtraSpawnAmount = Math.Clamp((int)(nextWaveExtraSpawnAmount * newWaveExtraSpawnAmountMultiplier), minNewWaveExtraSpawnAmount, maxNewWaveExtraSpawnAmount);
        nextCountDownTime += (int)(nextCountDownTime * 0.1f);
        countDownTime = timeBeforeWave;
        StartCoroutine(DecreaseCountDown());
    }

    public void SetEnemiesAlive(int amount)
    {
        enemiesAlive = amount;
    }
    
    public void EnemyDied()
    {
        enemiesAlive--;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    
    public GameScreens GetGameScreens()
    {
        return gameScreens;
    }
    
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public float GetTimeBeforeWave()
    {
        return timeBeforeWave;
    }

    public string GetCountDownTime()
    {
        return countDownTime.ToString();
    }

    public void PlayerDied()
    {
        throw new NotImplementedException();
    }
}
