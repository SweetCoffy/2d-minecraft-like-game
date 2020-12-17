using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public static Image main;

    // Update is called once per frame
    void Update()
    {
        main = GetComponent<Image>();
    }
}
