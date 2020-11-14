using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePowerup : MonoBehaviour
{
    private float timer;

    void Start()
    {
        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer > 2.5f) {
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
    
    /*
    void OnTriggerEnter2D(Collider2D other) {
        
        if(enemy != null) {
            enemy.doDamage(1f);
            Destroy(gameObject);
        } 
    }
    */
}
