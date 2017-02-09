using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeUI : MonoBehaviour
{

    public GameObject gameOverUI;
    public Text statusText;

    private CoinPickup coinPickUpScript;
    private MazeEnemy mazeEnemy;

    void Start()
    {
        mazeEnemy = FindObjectOfType<MazeEnemy>();
        coinPickUpScript = FindObjectOfType<CoinPickup>();
        
        if(coinPickUpScript != null)
        {
            coinPickUpScript.OnAllCoinsPickedUp += OnGameOver;
        }

        if(mazeEnemy != null)
        {
            mazeEnemy.OnGameOver += OnGameOver;
        }
    }

    void OnGameOver(string gameStatus)
    {
        gameOverUI.SetActive(true);
        statusText.text = gameStatus;
    }

	public void RestartMaze()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDisable()
    {
        Time.timeScale = 1;

        if (coinPickUpScript != null)
        {
            coinPickUpScript.OnAllCoinsPickedUp -= OnGameOver;
        }

        if (mazeEnemy != null)
        {
            mazeEnemy.OnGameOver -= OnGameOver;
        }
    }
}
