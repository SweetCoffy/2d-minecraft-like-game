using UnityEngine;

public class Rotate : MonoBehaviour
{
    Entity e;
    public int itemNeeded = 23;
    float h = 0;
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<Entity>();
    }
    void Update()
    {
        if (h > 0) h -= Time.deltaTime;
    }
    void OnMouseDown()
    {
        if (e.GetSelectedItem() < e.storedItems.Count - 1)
        {
            if (e.storedItems[e.GetSelectedItem()].id == itemNeeded && h <= 0)
            {
                transform.Rotate(0, 0, 90);
                h = 0.3f;
            }
        }
    }
}
