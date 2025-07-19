using System;
using Marten.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed, defaultHealth;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject attack, nextWaveScreen, deathScreen;
    [SerializeField] private TMP_Text healthText, waveCountText, waveCounterText;

    private float horizontal, vertical, _currentHealth;

    public float currentHealth
    {
        get { return _currentHealth; }
        set 
        { 
            _currentHealth = value;
            UpdateHealthBar();
            if (_currentHealth <= 0) Death();
        }
    }
    
    private GameManager gameManager;

    private bool movement; 
    public bool isDead;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        movement = true;
        isDead = false;
        
        currentHealth = defaultHealth;
        
        waveCountText.faceColor = Color.green;
        waveCountText.text = gameManager.GetTimeBeforeWave() + "";
        
        if (attack is not null)
        {
            Instantiate(attack, attackTransform.position, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        waveCountText.text = gameManager.GetCountDownTime();

        if (movement)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        if (horizontal != 0 && vertical == 0)
        {
            rigidbody.linearVelocity = new Vector3(horizontal * speed, 0, 0);
        }
        else if (horizontal == 0 && vertical != 0)
        {
            rigidbody.linearVelocity = new Vector3(0, 0, vertical * speed);
        }
        else if (horizontal != 0 && vertical != 0)
        {
            Vector3 vector = new Vector3(horizontal * speed, 0, vertical * speed);
            rigidbody.linearVelocity = vector.normalized * speed;
        }
        else
        {
            rigidbody.linearVelocity = Vector3.zero;
        }
    }
    
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / defaultHealth;
        healthText.text = currentHealth + " / " + defaultHealth;
    }
    
    public void UpdateWaveCounterText(int waveCount)
    {
        waveCounterText.text = "Wave: " + waveCount;
    }
    
    public void ChangeWaveCountColor(Color color)
    {
        waveCountText.faceColor = color;
    }

    public void TakeDamage(float damage, GameObject source = null)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        isDead = true;
        movement = false;
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        deathScreen.SetActive(true);
    }
    
    public void TurnOnNextWaveScreen()
    {
        nextWaveScreen.SetActive(true);
        movement = false;
    }

    public void ContinueButton()
    {
        nextWaveScreen.SetActive(false);
        RefreshHealth();
        gameManager.StartNewWave();
        movement = true;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene((int)Scenes.Level);
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    public void RefreshHealth()
    {
        currentHealth = defaultHealth;
    }
}
