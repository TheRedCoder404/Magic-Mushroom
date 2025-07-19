using System;
using Marten.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed, defaultHealth;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject attack;
    [SerializeField] private TMP_Text healthText;
    
    private float horizontal, vertical, currentHealth;

    private void Start()
    {
        currentHealth = defaultHealth;
        healthText.text = currentHealth + " / " + defaultHealth;
        if (attack is not null)
        {
            Instantiate(attack, attackTransform.position, Quaternion.identity, transform);
        }
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
        if (currentHealth <= 0)
        {
            healthBar.fillAmount = 0;
            healthText.text = "0";
            Death();
        }
        else
        {
            healthBar.fillAmount = currentHealth / defaultHealth;
            healthText.text = currentHealth + " / " + defaultHealth;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
