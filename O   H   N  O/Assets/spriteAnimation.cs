using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class spriteAnimation : MonoBehaviour
{
    public float waitTime;
    float waitedTime;
    public Sprite[] frames;
    int currentFrame;
    SpriteRenderer thisSprite;
    
    [ExecuteInEditMode]
    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrame > frames.Length -1) {
            currentFrame = 0;
        }
        thisSprite.sprite = frames[Mathf.Clamp(currentFrame, 0, frames.Length)];
        if(waitedTime > 0) {
            waitedTime -= Time.deltaTime;
        }
        if(waitedTime <= 0) {
            currentFrame++;
            
            waitedTime = waitTime;
        }
    }
}
