using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameManager gameManager;

    private GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameManager.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = player.transform.position - this.transform.position;
        rigidbody.linearVelocity = playerDirection.normalized * speed;
    }
}
