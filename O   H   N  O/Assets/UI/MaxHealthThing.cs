using UnityEngine;
using UnityEngine.UI;

public class MaxHealthThing : MonoBehaviour
{
    public string stat = "health";
    public float segmentSize = 40;
    public float maxStat = 600;
    public float healthPerSegmentSize = 20;
    public bool vertical = false;
    RectTransform rect;
    Entity e;
    
    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<Entity>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!vertical) {
            rect.sizeDelta = new Vector2(Mathf.Clamp(e.getMaxStat(stat), 0, maxStat) / healthPerSegmentSize * segmentSize, rect.sizeDelta.y);
        } else {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, Mathf.Clamp(e.getMaxStat(stat), 0, maxStat) / healthPerSegmentSize * segmentSize);
        }
        
    }
}
