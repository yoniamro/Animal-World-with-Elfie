using UnityEngine;
using System.Collections;

public class LaraMovement : MonoBehaviour 
{
    public Transform target;
    public float minRange;
    public float maxRange;
    public float smoothSpeed = 5f;


    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;


    private Vector3 previousPosition;
    private Vector3 newTargetPos;
    private float curSpeed;

    private bool madeRandomPos;
	private bool smoothLookAt = false;


    void Start()
    {
        
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        previousPosition = target.position;

        StartCoroutine(FollowPlayer());
    }

    void Update()
    {
		if(smoothLookAt)
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position), Time.deltaTime * 5f);
		
    }

    IEnumerator FollowPlayer()
    {
        float refreshRate = 0.25f;

        while(target != null)
        {
            if(agent != null)
            {
                if(!madeRandomPos)
                {
					newTargetPos = GenerateNewTargetPosition(ref newTargetPos);

                    
                    madeRandomPos = true;
                }

				if(Vector3.Distance (previousPosition, target.position) > 1.5f)
                {
                    madeRandomPos = false;
                    previousPosition = target.position;
                }


                agent.SetDestination(newTargetPos);

                //Vector3 curMove = transform.position - previousPosition;
                //curSpeed = curMove.magnitude / Time.deltaTime;
                //previousPosition = transform.position;
                
                if (agent.remainingDistance <= maxRange && agent.remainingDistance >= minRange)
                {
                    agent.speed = Mathf.Lerp(agent.speed, 6f, Time.deltaTime * smoothSpeed);
                }
                else if(agent.remainingDistance < minRange && agent.remainingDistance >= agent.stoppingDistance)
                {
                    agent.speed = Mathf.Lerp(agent.speed, 1.5f, Time.deltaTime * smoothSpeed);
                }

                curSpeed = agent.velocity.magnitude;
				//Debug.Log (curSpeed);
                anim.SetFloat("Blend", curSpeed);

                if(curSpeed == 0)
                {
					smoothLookAt = true;
//					transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }

            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

//	IEnumerator SmoothLookAt()
//	{
//		float percent = 0;
//
//
//		Quaternion lookRotation = Quaternion.LookRotation (target.position - transform.position);
//		
//		if (smoothLookAt)
//		{
//			
//			while (percent <= 1) 
//			{
//				percent += Time.deltaTime * 2f;
//
//				transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, percent);
//
//				if (percent >= 0.95f) 
//				{
//					percent = 1;
//					transform.LookAt (new Vector3 (target.position.x, transform.position.y, target.position.z));
//					break;
//				}
//
//				yield return null;
//			}
//		}
//
//
//		smoothLookAt = false;
//
//
//	}


	Vector3 GenerateNewTargetPosition(ref Vector3 newTargetPos)
    {

		//TODO: Clamp the random generated value between 2 thresholds

        newTargetPos = Random.insideUnitSphere * 5;
        newTargetPos.x += target.position.x;
        newTargetPos.y = target.position.y;
        newTargetPos.z += target.position.z;


		if (Vector3.Distance (newTargetPos, target.position) < 3f)
		{
			//Debug.Log ("Distance: " + Vector3.Distance (newTargetPos, target.position));
			return GenerateNewTargetPosition (ref newTargetPos);

		} 
		else 
		{
			smoothLookAt = false;	
			return newTargetPos;
		}




    }
}
