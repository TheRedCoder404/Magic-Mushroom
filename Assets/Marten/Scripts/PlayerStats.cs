using Marten.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float _attackSpeed = 1f;
    [SerializeField] private float _range = 6f;
    [SerializeField] private float resistance = 1f;
    [SerializeField] private float _critChance = 0.1f;
    [SerializeField] private float _critDamage = 1.5f;
    [SerializeField] private float _lifesteal = 0f;
    [SerializeField] private float earning = 1f;
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
    
    public float range
    {
        get { return _range; }
        set
        {
            _range = value;
            player.UpdateRange();
        }
    }
    
    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            player.UpdateDamage();
        }
    }
    
    public float attackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
            player.UpdateAttackSpeed();
        }
    }
    
    public float critChance
    {
        get { return _critChance; }
        set
        {
            _critChance = value;
            player.UpdateCritChance();
        }
    }
    
    public float critDamage
    {
        get { return _critDamage; }
        set
        {
            _critDamage = value;
            player.UpdateCritDamage();
        }
    }
    
    public float lifesteal
    {
        get { return _lifesteal; }
        set
        {
            _lifesteal = value;
            player.UpdateLifesteal();
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
        currentHealth -= damage * (1 / resistance);
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
