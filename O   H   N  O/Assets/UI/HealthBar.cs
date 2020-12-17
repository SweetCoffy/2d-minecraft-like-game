using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Gradient colorGradient;
    public float lerpSpeed = .4f;
    Entity e;
    public Entity target;
    public bool useGradient = false;
    public string stat = "health";
    public float maxStatBeforeColor = 600;
    public Color anotherColor = Color.yellow;
    public bool useText;
    Color originalColor;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        e = GameObject.Find("Player").GetComponent<Entity>();
        if (target != null)
        {
            e = target;
        }
        originalColor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = Mathf.Lerp(img.fillAmount, Mathf.Round(e.GetStat(stat)) / e.GetMaxStat(stat), lerpSpeed);




        if (e.GetMaxStat(stat) > maxStatBeforeColor)
        {
            img.color = anotherColor;
        }
        else
        {
            img.color = originalColor;
        }

        if (useGradient)
            img.color = Color.Lerp(img.color, colorGradient.Evaluate(e.GetStat(stat) / e.GetMaxStat(stat)), .25f);
    }
}
