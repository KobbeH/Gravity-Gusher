using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject flamePower;
    public float speed;
    public float shootDelay;
    Vector3 upVel;
    Vector3 downVel;
    Vector3 rightVel;
    Vector3 leftVel;
    public float spawnPadding = 1f;


    void Start()
    {
        upVel = new Vector3(0, speed, 0);
        downVel = new Vector3(0, -speed, 0);
        rightVel = new Vector3(speed, 0, 0);
        leftVel = new Vector3(-speed, 0, 0);
    }

    public void startShooting() {
      InvokeRepeating("spawnFour", 0f, shootDelay);
    }

    public void stopShooting() {
      CancelInvoke();
    }

    private void spawnFour() {
        Vector3 upSpawn = new Vector3(transform.position.x, transform.position.y + spawnPadding, transform.position.z);
        GameObject up = Instantiate(flamePower, upSpawn, Quaternion.Euler(0, 0, 90f));
        up.GetComponent<Rigidbody2D>().velocity = upVel;

        Vector3 downSpawn = new Vector3(transform.position.x, transform.position.y - spawnPadding, transform.position.z);
        GameObject down = Instantiate(flamePower, downSpawn, Quaternion.Euler(0, 0, 270f));
        down.GetComponent<Rigidbody2D>().velocity = downVel;

        Vector3 rightSpawn = new Vector3(transform.position.x + spawnPadding, transform.position.y, transform.position.z);
        GameObject right = Instantiate(flamePower, rightSpawn, Quaternion.identity);
        right.GetComponent<Rigidbody2D>().velocity = rightVel;

        Vector3 leftSpawn = new Vector3(transform.position.x - spawnPadding, transform.position.y, transform.position.z);
        GameObject left = Instantiate(flamePower, leftSpawn, Quaternion.Euler(0, 0, 180f));
        left.GetComponent<Rigidbody2D>().velocity = leftVel;
    }
}
