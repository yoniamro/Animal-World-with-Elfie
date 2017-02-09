using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MazeEnemy : MonoBehaviour
{
    public GameObject mazePlayer;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    private bool bustedPlayer = false;

    public event System.Action<string> OnGameOver;

    [HideInInspector]
    public bool gameCompleted = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        //Delayed because Coroutines does not work on inactive objects.
        Invoke("ChasePlayer", 2f);
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, mazePlayer.transform.position) < 5f && !bustedPlayer)
        {
            bustedPlayer = true;

            agent.Stop();
            agent.velocity = Vector3.zero;

            anim.SetFloat("Run", 0);
            anim.SetBool("Attack", true);

            mazePlayer.GetComponent<MazePlayer>().gotBusted = true;

            if (SceneManager.GetActiveScene().name != "Maze_Scene_Jessica")
            {
                mazePlayer.GetComponent<Animator>().SetBool("GotBusted", true);
            }

            Invoke("GameOver", 5);
        }
        else
        {
            anim.SetFloat("Run", agent.velocity.magnitude);
        }

        if(gameCompleted)
        {
            agent.Stop();
        }
    }

    void ChasePlayer()
    {
        StartCoroutine(StartChasing());
    }

    void GameOver()
    {
        if (OnGameOver != null)
        {
            //Time.timeScale = 0;
            OnGameOver("Game Over!");
        }
    }
    
    IEnumerator StartChasing()
    {
        while (!bustedPlayer && !gameCompleted)
        {
            if(mazePlayer != null)
            {
                if(agent != null)
                {
                    agent.SetDestination(mazePlayer.transform.position);
                    //Debug.Log("Inside Coroutine");
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }	
}
