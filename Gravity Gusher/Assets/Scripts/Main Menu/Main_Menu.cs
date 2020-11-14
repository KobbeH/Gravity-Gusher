using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Main_Menu : MonoBehaviour
{

  public AudioMixer mixer;
  
  
  private void Update() {
    if(Input.GetKey("2")) {
      SceneManager.LoadScene("Level2");
    }

    if(Input.GetKey("3")) {
      SceneManager.LoadScene("Level3");
    }

    if(Input.GetKey("4")) {
      SceneManager.LoadScene("Level4");
    }

    if(Input.GetKey("5")) {
      SceneManager.LoadScene("Level5");
    }

    if(Input.GetKey("6")) {
      SceneManager.LoadScene("Level6");
    }

    if(Input.GetKey("7")) {
      SceneManager.LoadScene("Level7");
    }

    if(Input.GetKey("8")) {
      SceneManager.LoadScene("Level8");
    }

  }


  public void playGame() {
      SceneManager.LoadScene("Level1");
  }

  public void quitGame() {
      Application.Quit();
  }

  public void setVolume(float volume) {
    mixer.SetFloat("primaryVolume", volume);
  }

  public void setFullScreen(bool isFull) {
    Screen.fullScreen = isFull;
  }

}
