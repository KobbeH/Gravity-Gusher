using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLaunchOnly : MonoBehaviour
{
    
    public CameraController mCam;
    Vector3 _initialPosition;
    bool canLaunch;
    float timeIdle = 0;
    Animator anim;
    [SerializeField] private Transform respawn;
    [SerializeField] float _launchPower = 250;
    
    private void Awake() 
    {
        _initialPosition = transform.position;
        canLaunch = true;
        GetComponent<LineRenderer>().enabled = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    
    private void Update() 
    {
        //display launch line if it's enabled
        GetComponent<LineRenderer>().SetPosition(0, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);


        //Check if dragon has launched and stopped moving
        if(!canLaunch && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.2) {
            timeIdle += Time.deltaTime;
        }


        //After Dragon has been still for ~2.1 seconds
        if(timeIdle > 2.2)   {
            
            //Respawn Dragon
            transform.position = respawn.transform.position;
            transform.rotation = Quaternion.identity;
            canLaunch = true;
            timeIdle = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;

            //anim.Play("Dragon Idle");
            anim.SetTrigger("Idle");

            //Reset Camera
            mCam.resetCamera();
            mCam.setCanMoveCamera(true);
        }

    }

    private void OnMouseDown()
    {
        if(canLaunch) {
            //Highlight dragon green and show launch trajectory
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<LineRenderer>().enabled = true;
            anim.enabled = true;
            anim.SetTrigger("Idle");
        }
        
    }

    private void OnMouseDrag() 
    {
        if(canLaunch) {
            //Move dragon with mouse cursor
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float checkedY = Mathf.Clamp(newPosition.y, 5f, _initialPosition.y + 6);
            newPosition.y = checkedY;

            float checkedX = Mathf.Clamp(newPosition.x, _initialPosition.x - 10, _initialPosition.x + 10);
            newPosition.x = checkedX;
        
            transform.position = new Vector3(newPosition.x, newPosition.y);
        }
        
    }

    private void OnMouseUp()
    {
        if(canLaunch) {
            GetComponent<SpriteRenderer>().color = Color.white;

            //Add launch force to the dragon based on how far it is from its start position
            Vector2 forceDirection = (_initialPosition - transform.position) * _launchPower;
            GetComponent<Rigidbody2D>().AddForce(forceDirection);
            GetComponent<Rigidbody2D>().gravityScale = 1.2f;
            
            //Turn off the Line trajectory indicator
            GetComponent<LineRenderer>().enabled = false;

            //Reset camera frame and disable player moving it
            mCam.setCanMoveCamera(false);
        }
        
        //Set to false so that player can't move Dragon until it respawns
        canLaunch = false;
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
        //Start the dragon walking animation once it hits something
        anim.SetTrigger("Walk");
    }

}
