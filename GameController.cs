using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text gameOverText;
    public Text restartText;
    public Text winText;

    private bool gameOver;
    private bool restart;
    private bool win;

    public Text scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        gameOver = false;
        restart = false;
        win = false;

        gameOverText.text = "";
        restartText.text = "";
        winText.text = "";

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }

        if (score >= 100)
        {
            winText.text = "You win! Created by Chris Gascoyne";
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
            {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                yield return new WaitForSeconds(startWait);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' to Try Again!";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER! Created by Chris Gascoyne";
        gameOver = true;
    }

}
