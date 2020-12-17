using UnityEngine;
public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    void FixedUpdate()
    {
        if (!cam)
            cam = Camera.main.gameObject;

        transform.position = cam.transform.position * parallaxEffect;
    }
}
