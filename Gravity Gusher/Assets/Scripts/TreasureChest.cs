using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject blueFirePrefab;
    public Game_Manager gameM;
    public float speed = 0.005f;
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
            tran.position = new Vector3(tran.position.x, tran.position.y + speed, tran.position.z);
        } else {
            tran.position = new Vector3(tran.position.x, tran.position.y - speed, tran.position.z);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        Dragon dragon = other.GetComponent<Dragon>();
        if(dragon != null) {
            Instantiate(blueFirePrefab, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
            gameM.addScore(3000);
            Invoke("deleteChest", 0.8f);
        }
    }

    private void deleteChest() {
        Destroy(gameObject);
    }
}
