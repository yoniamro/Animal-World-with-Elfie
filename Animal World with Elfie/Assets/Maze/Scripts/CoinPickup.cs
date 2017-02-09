using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPickup : MonoBehaviour
{
    public int coinsNumber;
    public AudioSource coinSFXAudioSource;
         
    public MazePlayer mazePlayer;
    public MazeEnemy mazeEnemy;

    public event System.Action<string> OnAllCoinsPickedUp;

    void Start()
    {
        //mazePlayer = FindObjectOfType<MazePlayer>();
        //mazeEnemy = FindObjectOfType<MazeEnemy>();

        foreach (Transform t in transform)
        {
            coinsNumber++;
        }

        Invoke("RegisterActionToPlayerScript", 1.5f);
    }

    void RegisterActionToPlayerScript()
    {
        if (mazePlayer != null)
        {
            mazePlayer.OnCoinPickup += OnCoinPickup;
        }
    }

    void OnCoinPickup()
    {

        coinsNumber--;
        coinsNumber = Mathf.Clamp(coinsNumber, 0, int.MaxValue);

        coinSFXAudioSource.Play();

        if (coinsNumber <= 0)
        {
            if (SceneManager.GetActiveScene().name != "Maze_Scene_Jessica")
            {
                mazePlayer.GetComponent<Animator>().SetTrigger("CoinsCollected");
            }

            mazeEnemy.gameCompleted = true;
            mazePlayer.collectedCoins = true;

            Invoke("GameComplete", 5);

            Debug.Log("Game Complete");
        }
    }

    void GameComplete()
    {
        if (OnAllCoinsPickedUp != null)
        {
            OnAllCoinsPickedUp("Game Complete!");
        }
    }

    void OnDisable()
    {
        if (mazePlayer != null)
        {
            mazePlayer.OnCoinPickup -= OnCoinPickup;
        }
    }


}
