using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float moveSpeed = 1;
    public float scrollSpeed = 1;
    public Transform tran;
    private Animator anim;
    private int health;
    private bool alive;
    private bool isFlashing;
    private float flashTimer;
    float momentumTimer;

    void Start()
    {
        health = 5;
        anim = GetComponent<Animator>();
        alive = true;
        isFlashing = false;
        flashTimer = 0;
        momentumTimer = 0f;
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update() {
        momentumTimer += Time.deltaTime;

        //Move player on key press
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && tran.position.y < 5f) {
            rb2d.velocity = new Vector3(scrollSpeed, moveSpeed, 0f);
        }

        if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && tran.position.y < 5f) {
            rb2d.velocity = new Vector3(scrollSpeed, -moveSpeed, 0f);
        }
    
        //Make dragon fall when dead
        if(!alive) {
            rb2d.velocity = new Vector3 (scrollSpeed, -2f, 0f);
            return;
        }

        //Keep player bounded vertically
        if(alive && tran.position.y >= 5f) {
            rb2d.velocity = new Vector3(scrollSpeed, -0.5f, 0f);

        } else if(alive && tran.position.y <= -3.4f) {
            rb2d.velocity = new Vector3 (scrollSpeed, 0.5f, 0f);
        }

        //Stop moving player vertically after they release move key
        if(momentumTimer > 0.08f) {
            rb2d.velocity = new Vector3 (scrollSpeed, 0f, 0f);
            momentumTimer = 0f;
        }

        //Monitor Player Health
        if(health < 1) {
            die();
        }


        //Handle Color FLash when hit
        if(isFlashing) {
            if(flashTimer < 0.9f) {
                flashTimer += Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color32 (239, 17, 30, 255);
            } else {
                flashTimer = 0;
                isFlashing = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        
    }

    //Called by enemy projectile scripts
    public void doDamage() {
        health -= 1;
        isFlashing = true;
    }

    public int getHealth() {
        return health;
    }

    public bool getIsAlive() {
        return alive;
    }

    private void die() {
        anim.SetTrigger("Die");
        alive = false;
        Invoke("hidePlayer", 1f);
    }

    //Disable sprite so it looks like player dies but level still keeps scrolling
    void hidePlayer() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
