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
    public string stat = "health";
    public bool useText;
    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<entity>();
        if(target != null) {
            e = target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, Mathf.Round(e.getStat(stat))/e.getMaxStat(stat), lerpSpeed);

        
        
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, colorGradient.Evaluate(e.getStat(stat)), .25f);
    }
}
