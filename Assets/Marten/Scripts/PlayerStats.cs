using System;
using Marten.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] public float defaultMaxHealth = 100f;
    [SerializeField] public float defaultDamage = 1f;
    [SerializeField] public float defaultSpeed = 12f;
    [SerializeField] public float defaultAttackSpeed = 1f;
    [SerializeField] public float defaultRange = 1f;
    [SerializeField] public float defaultResistance = 1f;
    [SerializeField] public float defaultCritChance = 0.1f;
    [SerializeField] public float defaultCritDamage = 1.5f;
    [SerializeField] public float defaultLifesteal = 0f;
    [SerializeField] public float defaultEarning = 1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameScreens gameScreens;
    [SerializeField] private Player player;
    
    private float _maxHealth;
    private float _currentHealth;
    private float _damage;
    private float _speed;
    private float _attackSpeed;
    private float _range;
    private float _resistance;
    private float _critChance;
    private float _critDamage;
    private float _lifesteal;
    private float _earning;
    private int _shrooms;

    public float maxHealth
    {
        get { return _maxHealth; }
        set 
        { 
            _maxHealth = value;
            player.UpdateHealthBar();
        }
    }

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
    
    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            player.UpdateDamage();
        }
    }
    
    public float speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
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
    
    public float range
    {
        get { return _range; }
        set
        {
            _range = value;
            player.UpdateRange();
        }
    }
    
    public float resistance
    {
        get { return _resistance; }
        set
        {
            _resistance = value;
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
    
    public float earning
    {
        get { return _earning; }
        set
        {
            _earning = value;
        }
    }
    
    public int shrooms
    {
        get { return _shrooms; }
        set
        {
            _shrooms = value;
            player.UpdateShroomText(_shrooms);
        }
    }

    private void Start()
    {
        _maxHealth = defaultMaxHealth;
        _currentHealth = defaultMaxHealth;
        _damage = defaultDamage;
        _speed = defaultSpeed;
        _attackSpeed = defaultAttackSpeed;
        _range = defaultRange;
        _resistance = defaultResistance;
        _critChance = defaultCritChance;
        _critDamage = defaultCritDamage;
        _lifesteal = defaultLifesteal;
        _earning = defaultEarning;
        shrooms = 0;
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
    
    public void EarnShroom(float amount)
    {
        shrooms += (int)(amount * earning);
    }
}
