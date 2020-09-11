using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    private static int levelUpScore = 3;
    public GameObject winScreen;
    public GameObject deathScreen;
    public GameObject mainMenu;
    public GameObject scoreObject;
    public GameObject startGameCounter;
    public GameObject startGamePanel;
    public GameObject bird;
    public GameObject pipeSpawner;
    public GameObject levelUpScreen;
    public GameObject settingsMenu;
    public static bool isGameStarted = false;
    public GameObject inGameMenu;

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        bird.GetComponent<Rigidbody2D>().Sleep();
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("MenuMusic");
        }
        else
        {
            startGamePanel.SetActive(true);
            isGameStarted = true;
            //FindObjectOfType<AudioManager>().Stop("MenuMusic");
            FindObjectOfType<AudioManager>().Play("InGameMusic");
            Move.speed = 0f;
            Time.timeScale = 1f;
            StartCoroutine(beginGameAfterAnimation());
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStarted)
        {
            if (inGameMenu.activeSelf)
            {
                inGameMenu.SetActive(false);
                Time.timeScale = 1f;
                FindObjectOfType<AudioManager>().Play("InGameMusic");
            }
            else
            {
                inGameMenu.SetActive(true);
                Time.timeScale = 0f;
                FindObjectOfType<AudioManager>().Play("MenuMusic");
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
                    int newLevelUpScore = 2;
                    string newLevelName = "LevelTwo";
                    levelUp(newLevelUpScore, newLevelName);
                }
                break;
            case "LevelTwo":
                if (score == levelUpScore)
                {
                    int newLevelUpScore = 1;
                    string newLevelName = "LevelThree";
                    levelUp(newLevelUpScore, newLevelName);
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
        levelUpScore = 3;
        isGameStarted = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void levelUp(int newLevelUpScore, string newLevelName)
    {
        levelUpScore = newLevelUpScore;
        levelUpScreen.SetActive(true);
        pipeSpawner.SetActive(false);
        bird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bird.GetComponent<Rigidbody2D>().gravityScale = 0f;
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipes");
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
        BirdControl.IsInputEnabled = false;
        StartCoroutine(levelUpAnimation(newLevelName));
    }

    public void playGame()
    {
        mainMenu.SetActive(false);
        scoreObject.SetActive(true);
        startGamePanel.SetActive(true);
        Time.timeScale = 1f;
        Move.speed = 0f;
        FindObjectOfType<AudioManager>().Play("InGameMusic");
        StartCoroutine(beginGameAfterAnimation());
    }

    public void continueGame()
    {
        inGameMenu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("InGameMusic");
        Time.timeScale = 1f;
    }

    public void backToMenu()
    {
        settingsMenu.SetActive(false);
        if (isGameStarted)
        {
            inGameMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    public void showSettingsMenu()
    {
        if (isGameStarted)
        {
            inGameMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }

    IEnumerator beginGameAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(2f);
        Move.speed = 1f;
        bird.GetComponent<Rigidbody2D>().WakeUp();
        pipeSpawner.SetActive(true);
        isGameStarted = true;
    }

    IEnumerator levelUpAnimation(string newLevelName)
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(newLevelName);
    }

}
