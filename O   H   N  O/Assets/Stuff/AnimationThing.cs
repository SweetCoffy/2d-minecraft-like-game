using UnityEngine;

public class AnimationThing : MonoBehaviour
{
    public float speed = 0.2f;
    public Vector3 originalScale = Vector3.one * 0.5f;
    public float multiplier = 0.2f;
    public float rotationSpeed = 0.3f;
    public float rotationMultiplier = 12.5f;
    public Quaternion testQuaternion;

    void Update() {
        transform.localScale = originalScale + (Vector3.one * Mathf.Sin(Time.time * speed) * multiplier);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * rotationSpeed) * rotationMultiplier);
    }
}
