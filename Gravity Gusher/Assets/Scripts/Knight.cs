using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Game_Manager manager;
    bool alive;
    float timeIdle = 0;
    Animator anim;
    AudioSource sound;

    [SerializeField] private GameObject blueFirePrefab;
    
    void Start()
    {
        alive = true;
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        manager.addEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //Start timer if knight is dead
        if(!alive) {
            timeIdle += Time.deltaTime;
        }

        //After a few seconds instantiate the blue fire particles, tell manager an enemy has died, and then destroy the knight
        if(timeIdle > 1.8) {
            Instantiate(blueFirePrefab, transform.position, Quaternion.identity);
            manager.subtractEnemy();
            manager.addScore(500);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Dragon dragon = other.collider.GetComponent<Dragon>();

        //If the object that collided was a dragon, kill the knight
        if(dragon != null) {
            if(alive) {
                die();
            }
    
            return;
        }

        //Check if hit by fire attack
        if(other.collider.GetComponent<FlamePowerup>() != null) {
            if(alive) {
                die();
            }
        }
        
        //If the object that collided was another knight then exit the function and do nothing
        if(other.collider.GetComponent<Knight>() != null) {
            return;
        }

        //Since it's not a dragon or knight, the colliding object must be an obstacle.
        //If the obstacle falls on top of the knight within a certain radius then kill the knight
        if(other.contacts[0].normal.y <-0.5) {
            if(alive) {
                die();
            }
            return;
        }
        
    }

    private void die() {
        //Called when Knight dies. Plays death sound and death animation and sets alive = false
        sound.Play();
        anim.Play("K_Death");
        alive = false;
    }
}
