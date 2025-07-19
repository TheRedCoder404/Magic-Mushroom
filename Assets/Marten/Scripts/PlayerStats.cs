using Marten.Scripts;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameScreens gameScreens;
    [SerializeField] private Player player;
    
    private float _currentHealth;

    public float MaxHealth => maxHealth;
    public float Speed => speed;

    public float currentHealth
    {
        get { return _currentHealth; }
        set 
        { 
            _currentHealth = value;
            player.UpdateHealthBar();
            if (_currentHealth <= 0) Death();
        }
    }
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage, GameObject instigator = null)
    {
        currentHealth -= damage;
    }

    public void Death()
    {
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        gameScreens.OpenMenu(Menu.DeathScreen);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void CompleteHeal()
    {
        currentHealth = maxHealth;
    }
}
