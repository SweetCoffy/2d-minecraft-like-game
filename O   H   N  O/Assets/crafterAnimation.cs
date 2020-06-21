using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class crafterAnimation : MonoBehaviour
{
    public Vector3 animatePosition;
    SpriteRenderer sr;
    Vector3 originalPosition;
    public int craftingId;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(originalPosition, animatePosition, transform.parent.GetComponent<Crafter>().getProgressNormalized(craftingId, false));
    }
}
