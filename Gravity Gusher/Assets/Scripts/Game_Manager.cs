using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public TextMeshProUGUI levelIndicator;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI launchesLeftText;
    public TextMeshProUGUI levelScoreText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI directionsText;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public AudioSource wonLevelSound;
    public AudioSource lostLevelSound;
    float gameTime = 0;
    int enemiesLeft = 0;
    int launchesLeft;
    int levelNumber;
    int levelScore;
    public static int totalScore = 0;
    bool levelActive;

    void Start()
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex;
        levelIndicator.text = "Level " + levelNumber;
        Time.timeScale = 1;
        levelScore = 0;
        levelActive = true;

        //Show directions text if it's the first level
        if(levelNumber==1) {
            directionsText.gameObject.SetActive(true);
        }

        //The getMaxAttempts returns the number of launches based on the levelNumber
        launchesLeft = getMaxAttempts();
    }

    // Update is called once per frame
    void Update()
    {   
        //Check if all enemies are defeated
        if(enemiesLeft == 0 && levelActive) {
            levelComplete();
        
        //Check if Player has used up all their launches
        } else if(launchesLeft == -1 && levelActive) {
            gameOver();

        //If neither of the above then keep showing enemies and launches left
        } else {
            enemyText.text = "Enemies Remaining: " + enemiesLeft;
            launchesLeftText.text = "Launches Remaining: " + launchesLeft;
            levelScoreText.text = "Score: " + levelScore;
        }

        //Track game time
        gameTime += Time.deltaTime;

        //Stop showing the directions on first level after 4 seconds
        if(gameTime > 5) {
            directionsText.gameObject.SetActive(false);
        }
    }

    public void addEnemy() {
        //Called by every enemy when they wake up
        enemiesLeft += 1;
    }

    public void subtractEnemy() {
        //Called by every enemy when they are defeated
        enemiesLeft -=1;
    }

    public void addScore(int sc) {
        levelScore += sc;
    }

    public void useLaunch() {
        //Called by dragon every time player launches
        launchesLeft--;
    }

    public int getMaxAttempts() {
        //Case is the level number, returns the max launch attempts
        switch(levelNumber) {
            case 1:
            return 5;
            case 2:
            return 5;
            case 3:
            return 6;
            case 4:
            return 6;
            case 5:
            return 6;
            case 6:
            return 7;
            default:
            return 10;
        }
    }

    public void levelComplete() {
        //Stop showing enemies left. Display level complete, Replay button, and nextLevelButton
        levelActive = false;
        levelIndicator.gameObject.SetActive(false);
        enemyText.gameObject.SetActive(false);
        launchesLeftText.gameObject.SetActive(false);
        levelScoreText.gameObject.SetActive(false);
        
        wonLevelSound.gameObject.SetActive(true);
        victoryMenu.SetActive(true);
        showStars(launchesLeft);

        Time.timeScale = 0;
        totalScore += levelScore;
        totalScoreText.text = "Total Score: " + totalScore;
    }

    public void showStars(int score) {
        //3 stars are active by default. This functions checks the score and disables stars 2 or 3 if score is too low.
        if(score >= 3) {
            return;

        } else if(score >= 2 && GameObject.Find("Star3") != null) {
            GameObject.Find("Star3").gameObject.SetActive(false);

        } else if(GameObject.Find("Star3") != null && GameObject.Find("Star2") != null) {
            GameObject.Find("Star3").gameObject.SetActive(false);
            GameObject.Find("Star2").gameObject.SetActive(false);
        }
    }

    public void gameOver() {
        //Stop showing enemies left and launches left. Display game over image and replay button.
        levelActive = false;
        levelIndicator.gameObject.SetActive(false);
        levelScoreText.gameObject.SetActive(false);
        enemyText.gameObject.SetActive(false);
        launchesLeftText.gameObject.SetActive(false);

        lostLevelSound.gameObject.SetActive(true);
        gameOverMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void restartGame() {
        //This function is referenced by the replay button in scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel() {
        //This function is referenced by the Next Level Button in scene
        levelNumber++;
        string nextLevelTitle = "Level" + levelNumber;
        SceneManager.LoadScene(nextLevelTitle);
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
