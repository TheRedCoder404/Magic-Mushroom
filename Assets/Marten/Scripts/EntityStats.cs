using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private float maxHealth, attackDmg;
    [SerializeField] private Rigidbody rigidbody;
    
    private float health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
