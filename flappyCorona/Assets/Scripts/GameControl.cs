using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject WarningText;
    public GameObject MaskWarning;
    public GameObject WashWarning;
    public GameObject Bird;
    public GameObject HPBar;
    public GameObject WashButton;
    public Text CountdownText;
    public Text ScoreText;
    public float HealAmount = 30;
    public bool isGameOver = false;
    public float scrollSpeed = -1.5f;
    private int score = 0;
    public int maskCount = 0;
    private float timePassed = 0;
    private bool isCountdown = false;

     void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this) 
        {
            Destroy(gameObject);
        }
    }

     void Update()
    {
        if (isGameOver == true && Input.GetMouseButtonDown(0)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (isGameOver != true)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 5.0f)
            {
                WashButton.SetActive(true);
                CountdownText.gameObject.SetActive(true);
                isCountdown = true;
                timePassed = 0f;
            }
            if (isCountdown)
            {
                float timeLeft = 3.0f - timePassed;
                float normalizedTimeLeft = Mathf.Floor(timeLeft);
                CountdownText.text = "Time left: " + normalizedTimeLeft.ToString();
            }
            if (isCountdown && timePassed > 3)
            {
                BirdDied();
                timePassed = 0f;
                WashButton.SetActive(false);
            }
        }
    }

    public void BirdScored()
    {
        if (isGameOver)
        {
            return;
        }
        score++;
        if (score == 1)
        {
            MaskWarning.SetActive(false);
            WashWarning.SetActive(true);
        }
        if (score % 10 == 9)
        {
            WashWarning.SetActive(false);
            WarningText.SetActive(true);
        } else {
            WarningText.SetActive(false);
        }
        if (score % 10 == 0)
        {
            Bird.GetComponent<Rigidbody2D>().gravityScale *= -1;
            Bird.GetComponent<Bird>().upForce *= -1;
        }
        ScoreText.text = "Score: " + score.ToString();
        scrollSpeed -= 0.5f;
        GetComponent<ColumnPool>().spawnRate *= 0.95f;
    }

    public void BirdDied() 
    {
        gameOverText.SetActive(true);
        HPBar.SetActive(false);
        isGameOver = true; 
    }

    public void BirdPickMask()
    {
        maskCount++;
        HPBar.GetComponent<HealthBar>().Heal(HealAmount);
    }

    public void washHands() 
    {
        isCountdown = false;
        timePassed = 0f;
        WashButton.SetActive(false);
        CountdownText.gameObject.SetActive(false);
    }
}
