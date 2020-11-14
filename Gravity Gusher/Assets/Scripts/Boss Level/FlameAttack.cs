using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    private float timer;

    void Start()
    {
        rb.velocity = transform.right * speed;
        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer > 1.5f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        BossEnemy enemy = other.GetComponent<BossEnemy>();

        if(enemy != null) {
            enemy.doDamage(1f);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
    }
}