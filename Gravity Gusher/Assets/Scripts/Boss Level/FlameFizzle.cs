using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFizzle : MonoBehaviour
{
    private float timer;
    public Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb.velocity = transform.right * speed;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1f) {
            Destroy(gameObject);
        }
    }
}
