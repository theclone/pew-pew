using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public const string ScorePrefix = "Score: ";
    public const string RestartButton = "Restart";
    public const string GameOverMessage = "Game Over!";
    public const string RestartMessage = "Press 'R' for Restart";
    public const int BombIconSpacing = 30;

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
    [SerializeField]
    private GameObject bombIcon;
    [SerializeField]
    private GameObject gameCanvas;


    private bool gameOver;
    private bool restart;
    private int score;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text restartText;
    private UnityEngine.UI.Text gameOverText;
    private List<GameObject> bombIcons;
    private int currBombIcons;

    void Start()
    {
        Assert.IsNotNull(bombIcon);
        Assert.IsNotNull(gameCanvas);
        Assert.IsNotNull(scoreTextObject);
        Assert.IsNotNull(restartTextObject);
        Assert.IsNotNull(gameOverTextObject);
        score = 0;
        currBombIcons = 0;
        bombIcons = new List<GameObject>();
        scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
        restartText = restartTextObject.GetComponent<UnityEngine.UI.Text>();
        gameOverText = gameOverTextObject.GetComponent<UnityEngine.UI.Text>();
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        UpdateBombIcons();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetButtonDown(RestartButton))
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
                restartText.text = RestartMessage;
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
        scoreText.text = ScorePrefix + score;
    }

    public void UpdateBombIcons()
    {
        int numBombs = PlayerController.Instance.BombCount;

        while (currBombIcons > numBombs)
        {
            Destroy(bombIcons[currBombIcons - 1]);
            bombIcons[currBombIcons - 1] = null;
            currBombIcons -= 1;
        }

        for (; currBombIcons < numBombs; currBombIcons++)
        {
            bombIcons.Add(Instantiate(bombIcon, gameCanvas.transform));
            Vector2 iconOffset = new Vector2(currBombIcons * BombIconSpacing, 0);
            bombIcons[currBombIcons].GetComponent<RectTransform>().anchoredPosition += iconOffset;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
