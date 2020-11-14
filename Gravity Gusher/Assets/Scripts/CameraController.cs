using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform dragon;

    public float offset = 3.5f;
    public float minY;
    public float maxHorizontal;

    public float panSpeed = 25f;
    public float panPadding = 15f;
    public Transform cam;
    Vector3 originalPosition;
    bool canMoveCamera;

    private void Start() {
        originalPosition = new Vector3(dragon.position.x, cam.position.y, -10f);
        canMoveCamera = true;
        cam.position = new Vector3(dragon.position.x + offset, cam.position.y, -10f);
    }
    
    void Update()
    {
        if(canMoveCamera) {
            //Temporary variable to hold new camera position
            float tempX = cam.position.x;

            //Press d or move mouse to right edge of screen to move camera right
            if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panPadding) {
                tempX += panSpeed * Time.deltaTime;
            }

            //Press a or move mouse to left edge of screen to move camera left
            if(Input.GetKey("a") || Input.mousePosition.x <= panPadding) {
                tempX -= panSpeed * Time.deltaTime;
            }
            //virtualPos = Mathf.Clamp(virtualPos, 0, 1);
            Mathf.Clamp(tempX, -maxHorizontal, maxHorizontal);
            cam.position = new Vector3(tempX, cam.position.y, cam.position.z);

        } else {
            float dragY = dragon.position.y;
            if(dragY < minY) {
                dragY = minY;
            }
            cam.position = new Vector3(dragon.position.x + offset, dragY, cam.position.z);
        }
        
        
    }

    public void resetCamera() {
        //cam.position = new Vector3(originalPosition + offset, 2.5f, -10f);
        cam.position = originalPosition;
    }

    //Allows external objects to set the canMoveCamera bool
    public void setCanMoveCamera(bool boolean) {
        canMoveCamera = boolean;
    }
}
