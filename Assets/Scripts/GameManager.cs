using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] ballsPrefab;
    public GameObject[] bulletsPrefab;
    public GameObject player;
    public GameObject gameoverText;
    public GameObject gameoverButton;
    public int playerLife = 10;
    public bool isBullet;
    public bool isGameover;

    public AudioClip hitSound;
    public AudioSource playerAudio;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI lifeText;

    private Vector3 ballSpawnPos = new Vector3(23f, 0.5f, 3.6f);
    private float timeCount;
    private int randomBall;
    private int ballCount;
    private int minutes;
    private int seconds;
    private int score = 0;
    private int increment = 0;
    private int ballsInRound = 10;
    private bool isDelay;

    void Update()
    {
        Timer();
        DisplayUI();
        if (!isGameover)
        {
            NewRound();
            SpawnBallWave();
            SpawnBullet();
            CheckHealth();
        }
    }

    void Timer()
    {
        timeCount += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeCount / 60);
        seconds = Mathf.FloorToInt(timeCount % 60);
    }

    void DisplayUI()
    {
        lifeText.text = "Life: " + playerLife;
        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void NewRound()
    {
        ballCount = FindObjectsOfType<PathFollow>().Length;
        if (ballCount == 0)
        {
            ballsInRound = 10 + increment;
            increment++;
        }
    }

    void SpawnBallWave()
    {
        if (ballsInRound > 0 && !isDelay)
        {
            randomBall = Random.Range(0, ballsPrefab.Length);
            Instantiate(ballsPrefab[randomBall], ballSpawnPos, ballsPrefab[randomBall].transform.rotation);
            ballsInRound--;
            isDelay = true;
            StartCoroutine(Delay());
        }
    }

    void SpawnBullet()
    {
        if (!isBullet)
        {
            randomBall = Random.Range(0, bulletsPrefab.Length);
            Instantiate(bulletsPrefab[randomBall], (player.transform.forward * 2) + (Vector3.up * 0.5f), bulletsPrefab[randomBall].transform.rotation);
            isBullet = true;
        }
    }

    void CheckHealth()
    {
        if (playerLife <= 0)
        {
            isGameover = true;
            gameoverText.SetActive(true);
            gameoverButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateScore()
    {
        score += 25;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        isDelay = false;
    }
}
