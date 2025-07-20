using UnityEngine;

public class RotateEntety : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 5;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Debug.Log(x + " / " + y);
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-x, 0, -y));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * spinSpeed);
        ;
    }



}
