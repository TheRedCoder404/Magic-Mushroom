using Marten.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject attack;
    [SerializeField] private TMP_Text healthText, waveCountText, waveCounterText;
    [SerializeField] private PlayerStats playerStats;

    private GameScreens gameScreens;

    private float horizontal, vertical;

    
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameScreens = gameManager.GetGameScreens();
        
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

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 && vertical == 0)
        {
            rigidbody.linearVelocity = new Vector3(horizontal * playerStats.Speed, 0, 0);
        }
        else if (horizontal == 0 && vertical != 0)
        {
            rigidbody.linearVelocity = new Vector3(0, 0, vertical * playerStats.Speed);
        }
        else if (horizontal != 0 && vertical != 0)
        {
            Vector3 vector = new Vector3(horizontal * playerStats.Speed, 0, vertical * playerStats.Speed);
            rigidbody.linearVelocity = vector.normalized * playerStats.Speed;
        }
        else
        {
            rigidbody.linearVelocity = Vector3.zero;
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = playerStats.currentHealth / playerStats.MaxHealth;
        healthText.text = playerStats.currentHealth + " / " + playerStats.MaxHealth;
    }
    
    public void UpdateWaveCounterText(int waveCount)
    {
        waveCounterText.text = "Wave: " + waveCount;
    }
    
    public void ChangeWaveCountColor(Color color)
    {
        waveCountText.faceColor = color;
    }
}
