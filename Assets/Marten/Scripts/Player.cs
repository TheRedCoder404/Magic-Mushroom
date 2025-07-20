using Marten.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject[] attacks;
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
        
        if (attacks is not null && attacks.Length > 0)
        {
            foreach (var attack in attacks)
            {
                Instantiate(attack, attackTransform.position, Quaternion.identity, transform);
            }
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
        healthBar.fillAmount = playerStats.GetCurrentHealth() / playerStats.MaxHealth;
        healthText.text = playerStats.GetCurrentHealth() + " / " + playerStats.MaxHealth;
    }
    
    public void UpdateWaveCounterText(int waveCount)
    {
        waveCounterText.text = "Wave: " + waveCount;
    }
    
    public void ChangeWaveCountColor(Color color)
    {
        waveCountText.faceColor = color;
    }

    public void UpdateRange()
    {
        foreach (var attack in attacks)
        {
            attack.GetComponent<IPlayerAttack>().UpdateRange();
        }
    }

    public void UpdateDamage()
    {
        // nothing to update
    }

    public void UpdateAttackSpeed()
    {
        // nothing to update
    }

    public void UpdateCritChance()
    {
        // nothing to update
    }

    public void UpdateCritDamage()
    {
        // nothing to update
    }

    public void UpdateLifesteal()
    {
        // nothing to update
    }
}
