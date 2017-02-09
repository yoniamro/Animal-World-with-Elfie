using UnityEngine;
using System.Collections;

public class GetPosition : MonoBehaviour 
{
    private Movement movement;

    void Start()
    {
        movement = GameObject.FindObjectOfType<Cardboard>().GetComponent<Movement>();
        if(movement == null)
        {
            Debug.Log("ERROR: No Movement Script attached!");
        }
    }

    public void SetTargetPosition()
    {
        movement.SetEndPos(transform);
    }
	
}
