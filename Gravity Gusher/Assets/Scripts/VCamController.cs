using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamController : MonoBehaviour
{
    public float panSpeed = 1f;
    public float panPadding = 15f;
    public CinemachineVirtualCamera vCam;
    float originalPosition;
    bool canMoveCamera;
    

    private void Start() {
        originalPosition = vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;
        canMoveCamera = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(canMoveCamera) {
            //Temporary variable to hold new camera position
            float virtualPos = vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;

            //Press d or move mouse to right edge of screen to move camera right
            if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panPadding) {
                virtualPos -= panSpeed * Time.deltaTime;
            }

            //Press a or move mouse to left edge of screen to move camera left
            if(Input.GetKey("a") || Input.mousePosition.x <= panPadding) {
                virtualPos += panSpeed * Time.deltaTime;
            }

            //The Virtual Camera framing only goes from 0 to 1 so limit the new Position to between those values
            virtualPos = Mathf.Clamp(virtualPos, 0, 1);
            vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = virtualPos;
        }
    }

    //Resets the camera to its default framing
    public void resetCamera() {
        vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = originalPosition;
    }

    //Allows external objects to set the canMoveCamera bool
    public void setCanMoveCamera(bool boolean) {
        canMoveCamera = boolean;
    }
}
