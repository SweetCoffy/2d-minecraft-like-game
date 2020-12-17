using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CrafterAnimation : MonoBehaviour
{
    public Vector3 animatePosition;
    SpriteRenderer sr;
    Vector3 originalPosition;
    public int craftingId;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.localPosition;
    }
    void Update()
    {
        transform.localPosition = Vector3.Lerp(originalPosition, animatePosition, transform.parent.GetComponent<Crafter>().getProgressNormalized(craftingId, false));
    }
}
