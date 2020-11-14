using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : MonoBehaviour
{
    [SerializeField] private GameObject blueFirePrefab;
    Transform tran;
    float initialY;
    bool goingUp;
    
    void Start()
    {
        tran = GetComponent<Transform>();
        initialY = tran.position.y;
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tran.position.y > initialY + 1f) {
            goingUp = false;       
        } else if(tran.position.y < initialY) {
            goingUp = true;
        }

        if(goingUp) {
            tran.position = new Vector3(tran.position.x, tran.position.y +0.003f, tran.position.z);
        } else {
            tran.position = new Vector3(tran.position.x, tran.position.y -0.003f, tran.position.z);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        Dragon dragon = other.GetComponent<Dragon>();
        if(dragon != null) {
            dragon.setPoweredUp(true);
        }
        GetComponent<AudioSource>().Play();
        Instantiate(blueFirePrefab, transform.position, Quaternion.identity);
        Invoke("deletePepper", 0.5f);
    }


    private void deletePepper() {
        Destroy(gameObject);
    }
}
