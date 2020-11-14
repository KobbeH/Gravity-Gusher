using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1;
    public Rigidbody2D rb;
    //public GameObject impactEffect;

    private float timer;

    void Start()
    {
        rb.velocity = transform.right * speed;
        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer > 2f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Player2 player = other.GetComponent<Player2>();

        if(player != null) {
            player.doDamage();
            Destroy(gameObject);
        }

        if(other.GetComponent<FlameAttack>() != null) {
            return;
        }

        if(other.GetComponent<FartAttack>() != null) {
            return;
        }

        Destroy(gameObject);
        
    }
}
