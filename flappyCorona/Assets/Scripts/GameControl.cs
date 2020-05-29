using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject Screamer;
    public GameObject Zipper;
    public GameObject Washer;

    public GameObject musicBox;
    public GameObject adPanel;
    public bool isPaused = false;
    private float lastScale = 0;
    private bool isAdWatched = false;

    private bool isUnclip = false;
    private float unclipCountdown = 0f;
    private float unclipCountdownMax = 2f;

    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject WarningText;
    public GameObject MaskWarning;
    public GameObject WashWarning;
    public GameObject ClickText;
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
        if (isUnclip)
        {
            unclipCountdown += Time.deltaTime;
            if (unclipCountdown > unclipCountdownMax)
            {
                backToNormal();
                unclipCountdown = 0;
            }
        }
        if (isAdWatched == true && Input.GetMouseButtonDown(0))
        {
            isAdWatched = false;
            ClickText.SetActive(false);
            startFromLastPos();
        }
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
        musicBox.GetComponent<AudioSource>().Stop();
        isAdWatched = false;
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
        isAdWatched = true;
        adPanel.SetActive(false);
        ClickText.SetActive(true);
    }

    public void startFromLastPos()
    {
        musicBox.GetComponent<AudioSource>().Play();
        isCountdown = false;
        timePassed = 0f;
        isPaused = false;
        Bird.GetComponent<Rigidbody2D>().gravityScale = lastScale;
        makeUnclipable();
    }

    public void makeUnclipable()
    {
        isUnclip = true;
        Bird.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        Bird.GetComponent<PolygonCollider2D>().enabled = false;
    }

    public void backToNormal()
    {
        isUnclip = false;
        Bird.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        Bird.GetComponent<PolygonCollider2D>().enabled = true;
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
            float gravityScale = Bird.GetComponent<Rigidbody2D>().gravityScale;
            if (gravityScale == 1f)
            {
                Bird.GetComponent<Rigidbody2D>().gravityScale = -1f;
            }
            else {
                Bird.GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
            float upForce = Bird.GetComponent<Bird>().upForce;
            if (upForce > 0)
            {
                Bird.GetComponent<Bird>().upForce = -200f;
            } else {
                Bird.GetComponent<Bird>().upForce = 200f;
            }
            Bird.GetComponent<Bird>().transform.localScale = new Vector3(0.05f, -Bird.GetComponent<Bird>().transform.localScale.y, 0.05f);
        }
        ScoreText.text = "Score: " + score.ToString();
        scrollSpeed -= 0.5f;
        GetComponent<ColumnPool>().spawnRate *= 0.95f;
    }

    public void BirdDied() 
    {
        Screamer.GetComponent<AudioSource>().Play();
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
        Zipper.GetComponent<AudioSource>().Play();
        maskCount++;
        HPBar.GetComponent<HealthBar>().Heal(HealAmount);
    }

    public void washHands() 
    {
        Washer.GetComponent<AudioSource>().Play();
        isCountdown = false;
        timePassed = 0f;
        WashButton.gameObject.SetActive(false);
        CountdownText.gameObject.SetActive(false);
    }
}
