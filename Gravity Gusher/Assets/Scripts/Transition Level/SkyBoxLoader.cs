using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyBoxLoader : MonoBehaviour
{

    private void Start() {
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        DragLaunchOnly dragon = other.GetComponent<DragLaunchOnly>();
        
        if(dragon != null) {
            SceneManager.LoadScene("Level8");
        }

    }
}
