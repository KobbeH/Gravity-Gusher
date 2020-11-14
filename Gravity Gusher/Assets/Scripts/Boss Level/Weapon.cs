using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject flameAtkPrefab;
    public GameObject fartAtkPrefab;
    public AudioClip fart1;
    public AudioClip fart2;
    public AudioClip fart3;
    public AudioClip fart4;
    private AudioSource sound;
    private Animator animator;
    private float shootTimer;
    private bool altFireMode;

    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        shootTimer = 0;
        altFireMode = false;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && shootTimer > 0.2f && GetComponent<Player2>().getIsAlive()) {
            animator.SetTrigger("OnPlayerAttack");
            Shoot();
            shootTimer = 0f;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            if(altFireMode) {
                altFireMode = false;
            } else {
                altFireMode = true;
            }
        }
    }

    void Shoot()
     {
        int num = Random.Range(1, 5);

        if(altFireMode) {
            Instantiate(fartAtkPrefab, firePoint.position, firePoint.rotation);
            switch(num){
                case 1:
                sound.PlayOneShot(fart1);
                return;
                
                case 2:
                sound.PlayOneShot(fart2);
                return;

                case 3:
                sound.PlayOneShot(fart3);
                return;

                default:
                sound.PlayOneShot(fart4);
                return;
            }

        } else {
            Instantiate(flameAtkPrefab, firePoint.position, firePoint.rotation);
        }
     }
}
