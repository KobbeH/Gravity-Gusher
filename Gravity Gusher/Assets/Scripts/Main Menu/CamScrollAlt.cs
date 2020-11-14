using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScrollAlt : MonoBehaviour
{
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x + scrollSpeed, transform.position.y, 0);
        transform.position = newPos;
    }
}
