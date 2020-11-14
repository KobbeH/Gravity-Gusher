using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1;
    public float scrollSpeed = 1;
    public Transform tran;
    private Animator anim;
    private int health;
    private bool alive;
    private bool isFlashing;
    private float flashTimer;

    void Start()
    {
        health = 5;
        anim = GetComponent<Animator>();
        alive = true;
        isFlashing = false;
        flashTimer = 0;
    }

    
    void Update() {
        Vector3 tempVector = tran.position;

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            tempVector += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            tempVector += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            tempVector += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            tempVector += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        }

        //Auto Scroll
        float checkedY = Mathf.Clamp(tempVector.y, -4, 5);
        
        tempVector = new Vector3(tempVector.x + scrollSpeed, checkedY, tempVector.z);
        
        if(alive) {
            tran.position = tempVector;
        } else {
            tran.position = new Vector3(tempVector.x + scrollSpeed, checkedY - 0.018f, 0f);
        }

        if(health < 1) {
            die();
        }

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

    void hidePlayer() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
