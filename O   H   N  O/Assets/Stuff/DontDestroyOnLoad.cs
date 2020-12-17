using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
    }
}
