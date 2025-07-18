using Marten.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject attack; 
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float maxHealth;
    
    private float currentHealth;

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

    private void Death()
    {
        Destroy(gameObject);
    }
}
