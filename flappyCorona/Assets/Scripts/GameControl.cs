using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject adPanel;
    public bool isPaused = false;
    private float lastScale = 0;

    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject WarningText;
    public GameObject MaskWarning;
    public GameObject WashWarning;
    public GameObject Bird;
    public GameObject HPBar;
    public Button WashButton;
    public Text CountdownText;
    public Text ScoreText;
    public float HealAmount = 30;
    public bool isGameOver = false;

    public float scrollSpeed = -1.5f;

    private int score = 0;
    public int maskCount = 0;
    private float timePassed = 0;
    private bool isCountdown = false;
    private float countdownTime = 3.0f;

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

    private void Start()
    {
        adPanel.SetActive(false);
        WashButton.onClick.AddListener(washHands);
    }

    void Update()
    {
        if (isGameOver == true && Input.GetMouseButtonDown(0)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (isGameOver != true && isPaused != true)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 5.0f)
            {
                WashButton.gameObject.SetActive(true);
                CountdownText.gameObject.SetActive(true);
                isCountdown = true;
                timePassed = 0f;
            }
            if (isCountdown)
            {
                float timeLeft = countdownTime - timePassed + 1;
                float normalizedTimeLeft = Mathf.Floor(timeLeft);
                CountdownText.text = "Time left: " + normalizedTimeLeft.ToString();
            }
            if (isCountdown && timePassed > countdownTime)
            {
                //BirdDied();
                pauseGame();

                timePassed = 0f;
                WashButton.gameObject.SetActive(false);
            }
        }
    }

    public void pauseGame()
    {
        Rigidbody2D birdRB = Bird.GetComponent<Rigidbody2D>();
        birdRB.velocity = Vector2.zero;
        birdRB.angularVelocity = 0;
        adPanel.SetActive(true);
        CountdownText.gameObject.SetActive(false);
        WashButton.gameObject.SetActive(false);
        isPaused = true;
        lastScale = birdRB.gravityScale;
        birdRB.gravityScale = 0;
    }

    public void resumeGame()
    {
        isCountdown = false;
        timePassed = 0f;
        adPanel.SetActive(false);
        isPaused = false;
        Bird.GetComponent<Rigidbody2D>().gravityScale = lastScale;
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
        isPaused = false;
        WashButton.gameObject.SetActive(false);
        CountdownText.gameObject.SetActive(false);
        adPanel.SetActive(false);
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
        WashButton.gameObject.SetActive(false);
        CountdownText.gameObject.SetActive(false);
    }
}
