using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public Transform player;
    public float offset;

    void Update()
    {
        // Camera follows the player with offset padding
        transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
        
    }
}
