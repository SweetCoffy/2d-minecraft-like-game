using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DaylightThing : MonoBehaviour
{
    public Gradient gradient;

    public AnimationCurve intensityCurve;

    public AnimationCurve rotationCurve;

    public Gradient sunColorGradient;

    public Color initialColor;

    public float speed;

    public float sunRotationMultiplier = 1;

    public Transform sunRotationThing;

    public SpriteRenderer sun;

    

    SpriteRenderer s;

    public static float time {get; private set;}
    public static float sunTime {get; private set;}

    void Start() {
        s = GetComponent<SpriteRenderer>();
    }
    
    void Update() {
        time += Time.deltaTime * speed;
        sunTime += Time.deltaTime * speed;
        
        if (time > 1) {
            time = 0;
        }
        
        sunRotationThing.rotation = Quaternion.Euler(0, 0, 360 * sunTime * sunRotationMultiplier);
        s.color = gradient.Evaluate(time);
        RenderSettings.ambientLight = initialColor * intensityCurve.Evaluate(time);
        sun.color = sunColorGradient.Evaluate(time);
    }
}
