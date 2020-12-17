using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speed = 7;
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, transform.position.z) + GameObject.Find("Player").transform.position, speed * Time.fixedDeltaTime);
    }
}
