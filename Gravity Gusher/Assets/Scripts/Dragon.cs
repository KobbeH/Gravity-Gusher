using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    public Game_Manager manager;
    public CameraController mCam;
    Vector3 _initialPosition;
    bool canLaunch;
    bool poweredUp;
    float timeIdle = 0;
    Animator anim;
    [SerializeField] private Transform respawn;
    [SerializeField] float _launchPower = 250;
    PowerWeapon weapon;


    private void Awake() 
    {
        _initialPosition = transform.position;
        canLaunch = true;
        poweredUp = false;
        GetComponent<LineRenderer>().enabled = false;
        weapon = GetComponent<PowerWeapon>();

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

            //Report to the game manager that the player has used up a launch
            manager.useLaunch();
            //anim.Play("Dragon Idle");
            anim.SetTrigger("Idle");

            //Reset Camera
            mCam.resetCamera();
            mCam.setCanMoveCamera(true);

            //Stop Shooting Power Up Attacks
            weapon.stopShooting();
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
            
            float checkedY = Mathf.Clamp(newPosition.y, 0.3f, _initialPosition.y + 6);
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

        if(poweredUp) {
            weapon.startShooting();
        }
        
        //Set to false so that player can't move Dragon until it respawns
        canLaunch = false;
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
        //Start the dragon walking animation once it hits something
        anim.SetTrigger("Walk");
        weapon.stopShooting();
    }
    

    public void setPoweredUp(bool b) {
        poweredUp = b;
    }



    /*
        Trying to stop dragon from flipping upside down. Still WIP. Leave commented.
        var rotation = transform.eulerAngles;
        if(rotation.z > 170) {
            //rotation.z = limitAngle(rotation.z, 0, 75);
            rotation.z =0;
            transform.eulerAngles = rotation;
        }
    */

    /*
    private static float limitAngle(float angle, float min, float max) {
        //helper function to calculate acceptable dragon rotation value
        if(angle < -360F) {
            angle += 360F;
        }
        if(angle > 360F) {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
    */

}
