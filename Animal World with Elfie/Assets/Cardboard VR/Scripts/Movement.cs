using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    private Transform endPos;
    private bool startMoving = false;

    void Update()
    {
        StopCoroutine("StartMoving");
		StartCoroutine("StartMoving");
    }

    IEnumerator StartMoving()
    {
        if(startMoving)
        {
            
            Vector3 startPos = transform.position;

            float percent = 0;

            while (percent <= 1)
            {
                percent += Time.deltaTime;

				//if (Vector3.Distance (transform.position, endPos.position) > 0.5f) 
				transform.position = Vector3.Lerp (startPos, new Vector3 (endPos.position.x, endPos.position.y + 2f, endPos.position.z), percent);

				if (percent >= 0.98f)
					startMoving = false;

                yield return null;
            }
        }
        startMoving = false;
    }

    public void SetEndPos(Transform trans)
    {
        endPos = trans;
        EnableMovement();
    }

    public void EnableMovement()
    {
		if (endPos != null)
			startMoving = true;
    }
}
