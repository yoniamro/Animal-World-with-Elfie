using UnityEngine;
using System.Collections;


public class ObjectMovement : MonoBehaviour
{
    public GameObject plane;
    public UnityEngine.AI.NavMeshAgent agent;
    

    [HideInInspector]
    public bool tracked = false;

    //private Plane raycastPlane;
    private Vector3 targetPos;
    private bool firstMovement = true;

    private Plane raycastPlane;

    void Start()
    {

        if (agent != null)
        {
            targetPos = agent.transform.position;
        }

        raycastPlane = new Plane(-Camera.main.transform.forward, Vector3.zero);
    }

    void Update()
    {
        if (tracked)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                   
                    if (touch.tapCount == 2)
                    {
                        if(plane != null)
                        {
                            plane.SetActive(true);
                        }
                        if(agent != null)
                        {
                            agent.enabled = true;
                        }

                       
                        float dist;
                        if (raycastPlane.Raycast(ray, out dist))
                        {
                            targetPos = ray.GetPoint(dist);
                        }

                        firstMovement = false;
                    }
                    else if(touch.tapCount == 1)
                    {
                        if (plane != null)
                        {
                            plane.SetActive(false);
                        }

                        if (agent != null)
                        {
                            agent.enabled = false;
                        }

                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 1 << 31))
                        {
                            agent.gameObject.GetComponent<Animator>().SetTrigger("Sound");
                        }
                    }

                }
            }

            if (agent != null && !firstMovement)
            {
                if (agent.enabled)
                {
                    agent.SetDestination(targetPos);
                    
                    if (agent.gameObject.GetComponent<Animator>() != null)
                    {
                        agent.gameObject.GetComponent<Animator>().SetFloat("Move", agent.velocity.magnitude);
                    }
                }

            }

            
        }

    }

    public void StopMovement()
    {
        firstMovement = true;

        if (agent != null)
        {
            agent.enabled = false;

            if (agent.gameObject.GetComponent<Animator>() != null)
            {
                agent.gameObject.GetComponent<Animator>().SetFloat("Move", 0);
            }
        }

        if (plane != null)
        {
            plane.SetActive(false);
        }
    }

}