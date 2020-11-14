using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartAttack : MonoBehaviour
{
    public float speed = 35;
    public Rigidbody2D rb;
    private float timer;
    private bool hasHit;

    void Start()
    {
        rb.velocity = transform.right * speed;
        timer = 0;
        hasHit = false;
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer > 1.5f) {
            Destroy(gameObject);
        }

        if(hasHit) {
            transform.localScale += new Vector3(0.01f, 0.01f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        BossEnemy enemy = other.GetComponent<BossEnemy>();

        if(enemy != null) {
            enemy.doDamage(5f);
            hasHit = true;
            Invoke("deleteCloud", 1.5f);
        }
    }

    void deleteCloud() {
        Destroy(gameObject);
    }
}
