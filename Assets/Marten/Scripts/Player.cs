using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody rigidbody;
    
    private float horizontal, vertical;
    private float horVelocity, verVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontal = 0; 
        verVelocity = 0;
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
}
