using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerHealthDisplay : MonoBehaviour
{
    public Gradient colorGradient;
    public float lerpSpeed = .4f;
    entity e;
    public entity target;
    public bool useGradient = false;
    public string stat = "health";
    public float maxStatBeforeColor = 600;
    public Color anotherColor = Color.yellow;
    public bool useText;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<entity>();
        if(target != null) {
            e = target;
        }
        originalColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, Mathf.Round(e.getStat(stat))/e.getMaxStat(stat), lerpSpeed);

        
        
        
        if (e.getMaxStat(stat) > maxStatBeforeColor) {
            GetComponent<Image>().color = anotherColor;
        } else {
            GetComponent<Image>().color = originalColor;
        }
        
        if (useGradient) 
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, colorGradient.Evaluate(e.getStat(stat)/e.getMaxStat(stat)), .25f);
    }
}
