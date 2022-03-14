using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float backgroundRange = -7f;
       // Start is called before the first frame update
       void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < backgroundRange)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
        transform.Translate(-Time.deltaTime, 0, 0);
    }
}
