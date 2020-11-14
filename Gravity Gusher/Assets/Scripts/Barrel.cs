using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barrel : MonoBehaviour
{
    public Game_Manager manager;
    Animator _animator;
    bool broken;
    float timeBroken = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (broken) {
            timeBroken += Time.deltaTime;
        }

        //After the barrel has been broken for 0.4 seconds game object is destroyed
        if (timeBroken > 0.4) {
            manager.addScore(50);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Check if hit by fire attack
        if (other.collider.GetComponent<FlamePowerup>() != null)
        {
            _animator.Play("Breaking Barrel");
            GetComponent<AudioSource>().Play();
            broken = true;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the object that hit the barrel is the player sprite
        if (other.CompareTag("Player")) {

            //Play the breaking barrel animation if the player ran into it fast enough
            if(other.GetComponent<Rigidbody2D>().velocity.magnitude > 8f) {
                _animator.Play("Breaking Barrel");
                GetComponent<AudioSource>().Play();
                broken = true;
            }
        }
    }
}
