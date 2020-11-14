using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    public Player2 player;
    public BossEnemy boss;
    public HealthBar healthBar;
    public GameObject replayButton;
    public GameObject menuButton;
    public AudioSource alarm;
    public AudioSource growl1;
    public AudioSource growl2;
    public AudioSource growlDeath;
    public GameObject storyText;
    public GameObject controlsText;
    public GameObject warningText;
    public GameObject victoryText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject alertTriangle;
    private float gameTimer;
    private bool inIntro;
    int levelNumber;
    
    void Start()
    {
        gameTimer = 0f;
        inIntro = true;
        InvokeRepeating("flashAlert", 0f, 0.7f);
        levelNumber = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        healthBar.SetSize(boss.getHealth());
        showHearts(player.getHealth());

        //Bottom is reached first
        if(inIntro) {

            if(gameTimer > 28f) {
               CancelInvoke("flashAlert");
               alertTriangle.SetActive(false);
               warningText.gameObject.SetActive(false);
               growl2.gameObject.SetActive(true);

               healthBar.gameObject.SetActive(true);
               boss.gameObject.SetActive(true);
               inIntro = false;
            } else if(gameTimer > 21f) {
                alarm.gameObject.SetActive(true);

            } else if(gameTimer > 16f) {
                controlsText.gameObject.SetActive(false);

            } else if(gameTimer > 10f) {
                storyText.gameObject.SetActive(false);
                controlsText.gameObject.SetActive(true);
                growl1.gameObject.SetActive(true);
            }
        }

        if(boss.getHealth() <= 0f && gameTimer > 40f) {
            levelComplete();
        }

        if(player.getHealth() < 1) {
            Invoke("gameOver", 3f);
        }
    }

    private void showHearts(int i) {
        switch (i) {
            
            case 0:
            heart1.gameObject.SetActive(false);
            return;

            case 1:
            heart2.gameObject.SetActive(false);
            return;

            case 2:
            heart3.gameObject.SetActive(false);
            return;

            case 3:
            heart4.gameObject.SetActive(false);
            return;
            
            case 4:
            heart5.gameObject.SetActive(false);
            return;

            default:
            return;
        }
    }


    private void flashAlert() {

        if(gameTimer > 21f && alertTriangle.activeSelf) {
            alertTriangle.SetActive(false);
            warningText.gameObject.SetActive(false);
        } else if(gameTimer > 21f) {
            alertTriangle.SetActive(true);
            warningText.gameObject.SetActive(true);
        }
    }

    private void levelComplete() {
        healthBar.gameObject.SetActive(false);
        growlDeath.gameObject.SetActive(true);

        victoryText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        string score_string = "High Score: " + "  " + Game_Manager.totalScore;
        scoreText.text = score_string;

        menuButton.gameObject.SetActive(true);
    }

    private void gameOver() {
        replayButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

}
