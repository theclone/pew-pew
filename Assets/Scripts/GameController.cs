using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	private static GameController _instance;
	public static GameController instance {
		get {
			if (_instance == null) 
				_instance = FindObjectOfType<GameController>();
			return _instance;
		}

	}

    [SerializeField]
    private GameObject hazard;
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

	private int score;
	private UnityEngine.UI.Text scoreText;

    void Start()
    {
		score = 0;
		scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
		UpdateScore();
        StartCoroutine(SpawnWaves());
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
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnGap);
            }
			yield return new WaitForSeconds(waveWait);
        }
    }

	public void AddScore(int toAdd) {
		score += toAdd;
		UpdateScore();
	}

	public void UpdateScore() {
		scoreText.text = "Score: " + score;
	}
}
