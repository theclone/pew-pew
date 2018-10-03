using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const string SCORE_PREFIX = "Score: ";
    public const string RESTART_BUTTON = "Restart";
    public const string GAME_OVER_MESSAGE = "Game Over!";
	public const string RESTART_MESSAGE = "Press 'R' for Restart";

    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();
            return _instance;
        }

    }

    [SerializeField]
    private GameObject[] hazards;
    [SerializeField]
    private Vector3 spawnValues;
    [SerializeField]
    private int hazardCount;
    [SerializeField]
    private float spawnGap;
    [SerializeField]
    private float startWait;
    [SerializeField]
    private float waveWait;
    [SerializeField]
    private GameObject scoreTextObject;
    [SerializeField]
    private GameObject restartTextObject;
    [SerializeField]
    private GameObject gameOverTextObject;


    private bool gameOver;
    private bool restart;
    private int score;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text restartText;
    private UnityEngine.UI.Text gameOverText;

    void Start()
    {
        score = 0;
        scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
        restartText = restartTextObject.GetComponent<UnityEngine.UI.Text>();
        gameOverText = gameOverTextObject.GetComponent<UnityEngine.UI.Text>();
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetButtonDown(RESTART_BUTTON))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnGap);
            }
            if (gameOver)
            {
                restartText.text = RESTART_MESSAGE;
                restart = true;
                break;
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int toAdd)
    {
        score += toAdd;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = SCORE_PREFIX + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
