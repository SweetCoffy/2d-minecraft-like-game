using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class snap : MonoBehaviour
{
    public Vector3 RoundVector3(Vector3 a) {
        return new Vector3(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z) );
    }
    public Vector3 DivideVector3(Vector3 a, Vector3 b) {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    public Vector3 MultiplyVector3(Vector3 a, Vector3 b) {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public Vector3 snapSize = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = MultiplyVector3(RoundVector3(DivideVector3(transform.position, snapSize)), snapSize);
    }
}
