using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompareColors : MonoBehaviour
{
    public float colorThreshold = 0.1f;
    public Animator anim;

    private bool tookFirstColor = false;    
	private Vector3 prevColorValues = Vector3.one;


    public void Compare(Color origColor)
    {
		
        if(!tookFirstColor)
        {
            prevColorValues = new Vector3(origColor.r, origColor.g, origColor.b);
            tookFirstColor = true;
        }


        Vector3 currColorValues = new Vector3(origColor.r, origColor.g, origColor.b);


		if ((currColorValues - prevColorValues).sqrMagnitude > colorThreshold)
		{
			anim.SetBool ("Annoyed", true); 
			prevColorValues = currColorValues;
		}
		else
		{
			anim.SetBool ("Annoyed", false);
		}

		IEnumerator inst = null; 
		inst = GetPreviousColor(origColor);
		StopCoroutine(inst);
		StartCoroutine(inst);

    }

	IEnumerator GetPreviousColor(Color origColor)
	{
		yield return new WaitForSeconds (1f);
		prevColorValues = new Vector3 (origColor.r, origColor.g, origColor.b);
	}

}
