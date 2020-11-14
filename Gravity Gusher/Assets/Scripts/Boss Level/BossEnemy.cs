using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public Transform player;
    public HealthBar healthBar;
    public float maxHealth = 200;
    public float distFromPlayer;
    public float minY;
    public float maxY;
    public float flyingSpeed;
    public Transform firePoint;
    public GameObject blueFireBallPrefab;
    public GameObject pinkFireBallPrefab;
    private Animator anim;
    private float targetHeight;
    private float movementTimer;
    private float flashTimer;
    private bool isFlashing;
    private float health;
    private bool alive;

    void Start()
    {
        InvokeRepeating("randomTargetY", 2.0f, 3.0f);
        InvokeRepeating("randomShot", 2.0f, 1.5f);
        movementTimer = 0;
        flashTimer = 0;
        health = maxHealth;
        alive = true;
        anim = GetComponent<Animator>();
    }

    
    void Update() {
        movementTimer += Time.deltaTime;
        float temp = transform.position.y;

        //Move Boss to target height
        if(Mathf.Abs(transform.position.y - targetHeight) < 0.4f) {
            //do nothing
        } else if(transform.position.y < targetHeight) { 
         //else if(transform.position.y < targetHeight && movementTimer > 0.1) {
            temp += flyingSpeed;
            movementTimer = 0;
        } else if(transform.position.y > targetHeight) {
            temp -= flyingSpeed;
            movementTimer = 0;
        }

        float checkedY = Mathf.Clamp(temp, minY, maxY);

        if(alive) {
            transform.position = new Vector3(player.position.x + distFromPlayer, checkedY, 0f);
        } else {
            transform.position = new Vector3(player.position.x + distFromPlayer, transform.position.y - 0.018f, 0f);
        }

        //Flash when hit
        if(isFlashing) {
            if(flashTimer < 0.2) {
                flashTimer += Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color(0f, 185f, 142f);
            } else {
                flashTimer = 0;
                isFlashing = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        if(alive && health <= 0f) {
            die();
        }

    }

    private void randomTargetY() {

        while(true) {
            float temp = Random.Range(minY, maxY);

            if(Mathf.Abs(targetHeight - temp) > 2.0f) {
                targetHeight = temp;
                return;
            }
        }
    }

    private void randomShot() {
        float ans = Random.Range(0f, 1f);

        if(ans > 0.5f && alive) {
            anim.SetTrigger("Shoot");
            Invoke("Shoot", 1.5f);
        }
    }
    void Shoot()
     {
         if(Random.Range(0f, 1f) > 0.5f) {
             Instantiate(blueFireBallPrefab, firePoint.position, firePoint.rotation);
         } else {
             Instantiate(pinkFireBallPrefab, firePoint.position, firePoint.rotation);
         }
     }

    public float getHealth() {
        return health/maxHealth;
    }

    public void doDamage(float dmg) {
        health -= dmg;
        isFlashing = true;
        healthBar.setFlashing();
    }


     private void die() {
        anim.SetTrigger("Die");
        alive = false;
        Invoke("deleteDragon", 1.7f);
     }

     private void deleteDragon() {
         Destroy(this);
     }

    
}
