using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rotationInt = (int)Mathf.Round(Random.Range(0, 3));
        if (rotationInt == 1)
        {
            transform.Rotate(0, 0, 90);
        }
        else if (rotationInt == 2)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (rotationInt == 3)
        {
            transform.Rotate(0, 0, 270);
        }
    }

}
