using System.Collections.Generic;
using UnityEngine;
public class Background : MonoBehaviour
{
    SpriteRenderer s;
    public int backgroundIndex;
    private float _alpha = 1;
    public bool useArray = false;
    public List<int> backgroundIndexes;
    public float alpha
    {
        get
        {
            return _alpha;
        }
        set
        {
            float childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                SpriteRenderer r = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (r == null) continue;
                Color newColor = new Color(r.color.r, r.color.g, r.color.b, value);
                r.color = newColor;
            }
            _alpha = value;
            if (s != null)
            {
                Color newColor = new Color(s.color.r, s.color.g, s.color.b, value);
                s.color = newColor;
            }
        }
    }
    void Start()
    {
        s = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if ((DaylightThing.m.currBackground == backgroundIndex && !useArray) || (backgroundIndexes.Contains(DaylightThing.m.currBackground) && useArray))
        {
            alpha = Mathf.Lerp(alpha, 1, DaylightThing.m.backgroundTransitionSpeed * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, 0, DaylightThing.m.backgroundTransitionSpeed * Time.deltaTime);
        }
    }
}
