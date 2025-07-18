using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField] 
    private GameManager gameManager;

    private GameObject player;
    private float horizontal, vertical;
    private float horVelocity, verVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontal = 0; 
        verVelocity = 0;
        
        player = gameManager.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        float angleToPlayer = Vector3.Angle(this.transform.position, player.transform.position);
        Vector3 playerDirection = player.transform.position - this.transform.position;
        
        rigidbody.linearVelocity = playerDirection.normalized * speed;
    }
}
