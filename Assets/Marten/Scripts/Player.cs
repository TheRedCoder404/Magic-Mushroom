using System.Collections.Generic;
using System.Linq;
using Marten.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private List<GameObject> attacks;
    [SerializeField] private TMP_Text healthText, waveCountText, waveCounterText, shroomText;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;

    private GameScreens gameScreens;

    private float horizontal, vertical;
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameScreens = gameManager.GetGameScreens();
        
        waveCountText.faceColor = Color.green;
        waveCountText.text = gameManager.GetTimeBeforeWave() + "";
        
        if (attacks is not null && attacks.Capacity > 0)
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
            rigidbody.linearVelocity = new Vector3(horizontal * playerStats.speed, 0, 0);
        }
        else if (horizontal == 0 && vertical != 0)
        {
            rigidbody.linearVelocity = new Vector3(0, 0, vertical * playerStats.speed);
        }
        else if (horizontal != 0 && vertical != 0)
        {
            Vector3 vector = new Vector3(horizontal * playerStats.speed, 0, vertical * playerStats.speed);
            rigidbody.linearVelocity = vector.normalized * playerStats.speed;
        }
        else
        {
            rigidbody.linearVelocity = Vector3.zero;
        }

    }
    
    public void AddAttack(GameObject attackPrefab)
    {
        if (attackPrefab is null) return;

        if (transform.childCount > 3)
        {
            for (var i = 3; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name.StartsWith(attackPrefab.name))
                {
                    transform.GetChild(i).gameObject.GetComponent<IPlayerAttack>().prefabCount++;
                    return;
                }
            }
        }
        
        Instantiate(attackPrefab, attackTransform.position, Quaternion.identity, transform);

    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = playerStats.currentHealth / playerStats.maxHealth;
        healthText.text = playerStats.currentHealth + " / " + playerStats.maxHealth;
    }
    
    public void UpdateWaveCounterText(int waveCount)
    {
        waveCounterText.text = "Wave: " + waveCount;
    }
    
    public void UpdateShroomText(int shroomCount)
    {
        shroomText.text = "Shrooms: " + shroomCount;
    }
    
    public void ChangeWaveCountColor(Color color)
    {
        waveCountText.faceColor = color;
    }

    public void UpdateRange()
    {
        if (transform.childCount > 3)
        {
            for (var i = 3; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<IPlayerAttack>(out var attack))
                {
                    attack.UpdateRange();
                }
            }
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
