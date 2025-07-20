using UnityEngine;

public class RotateEntety : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 5;
    [SerializeField] private Animator animator;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");



        Quaternion rotation = Quaternion.LookRotation(new Vector3(-x, 0, -y));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * spinSpeed);
        

        bool isMoving = x != 0 || y != 0;
        if(animator != null)
            animator.SetBool("IsRunning", isMoving);
    }



}
