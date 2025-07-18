using System;
using Marten.Scripts;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed, defaultHealth;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    
    private float horizontal, vertical, currentHealth;

    private void Start()
    {
        currentHealth = defaultHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 && vertical == 0)
        {
            rigidbody.linearVelocity = new Vector3(horizontal * speed, 0, 0);
        } else if (horizontal == 0 && vertical != 0) 
        {
            rigidbody.linearVelocity = new Vector3(0, 0, vertical * speed);
        } else if (horizontal != 0 && vertical != 0)
        {
            Vector3 vector = new Vector3(horizontal * speed, 0, vertical * speed);
            rigidbody.linearVelocity = vector.normalized * speed;
        }
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
