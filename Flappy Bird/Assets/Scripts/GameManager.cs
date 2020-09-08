using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    private static int levelUpScore = 10;
    public GameObject winScreen;
    public GameObject deathScreen;
    public GameObject mainMenu;
    public GameObject scoreObject;
    public GameObject startGameCounter;
    public GameObject startGamePanel;
    public GameObject bird;
    public GameObject pipeSpawner;
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        bird.GetComponent<Rigidbody2D>().Sleep();
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu.activeSelf)
            {
                mainMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                mainMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }


    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelOne":
                if (score == levelUpScore)
                {
                    levelUpScore = 15;
                    SceneManager.LoadScene("LevelTwo");
                }
                break;
            case "LevelTwo":
                if (score == levelUpScore)
                {
                    levelUpScore = 20;
                    SceneManager.LoadScene("LevelThree");
                }
                break;
            case "LevelThree":
                if(score == levelUpScore)
                {
                    Time.timeScale = 0f;
                    winScreen.SetActive(true);
                    deathScreen.SetActive(false);

                }
                break;
            default:
                SceneManager.LoadScene("LevelOne");
                break;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("LevelOne");
        levelUpScore = 10;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void playGame()
    {
        mainMenu.SetActive(false);
        scoreObject.SetActive(true);
        startGamePanel.SetActive(true);
        Time.timeScale = 1f;
        Move.speed = 0f;
        StartCoroutine(beginGameAfterAnimation());
    }

    IEnumerator beginGameAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(2f);
        Move.speed = 1f;
        bird.GetComponent<Rigidbody2D>().WakeUp();
        pipeSpawner.SetActive(true);
    }
}
