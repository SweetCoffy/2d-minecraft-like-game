using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool affectedBySunlight = true;

    public static DaylightThing m;
    public int currBackground = 0;
    public float backgroundTransitionSpeed = 5;


    

    SpriteRenderer s;

    public static float time {get; private set;}
    public static float sunTime {get; private set;}

    void Start() {
        s = GetComponent<SpriteRenderer>();
        m = this;
    }
    
    void Update() {
        time += Time.deltaTime * speed;
        sunTime += Time.deltaTime * speed;
        
        if (time > 1) {
            time = 0;
        }
        
        sunRotationThing.rotation = Quaternion.Euler(0, 0, 360 * sunTime * sunRotationMultiplier);
        Color c = gradient.Evaluate(time);
        s.color = new Color(c.r, c.g, c.b, s.color.a);
        Color target = initialColor * intensityCurve.Evaluate(time);
        if (!affectedBySunlight) target = initialColor;
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, target, 10 * Time.deltaTime);
    }

    public void SetOverworld() {
        initialColor = new Color(1, 1, 1, 1);
        affectedBySunlight = true;
        currBackground = 0;
    }
    public void SetUnderworld() {
        initialColor = new Color(0.988f,0.254f,0.0117f, 1);
        affectedBySunlight = false;
        currBackground = 1;
    }
    public void SetFloatingIslands() {
        initialColor = new Color(1,1,1,1);
        affectedBySunlight = true;
        currBackground = 2;
    }
    public void SetSpace() {
        initialColor = new Color(0.1f,0.1f,0.1f,1);
        affectedBySunlight = false;
        currBackground = 3;
    }
}
