using System;
using Marten.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject attack, shroom;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyCreator enemyCreator;
    
    private float currentHealth;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        SetupVisuals();
    }

    private void SetupVisuals()
    {
        enemyCreator.Create(transform);
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        if (attack is not null)
        {
            Instantiate(attack, attackTransform.position, Quaternion.identity, transform);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage, GameObject source = null)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Death();
    }

    public void Death()
    {
        gameManager.EnemyDied();
        Instantiate(shroom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DeathNoCount()
    {
        Destroy(gameObject);
    }
}
