using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTextureSimple : MonoBehaviour 
{
    //public int materialIndex = 0;

	void Start ()
    {
		/*
        GetComponent<SkinnedMeshRenderer>().material.EnableKeyword("_MainTex");

	    if (RenderTextureCamera.CameraOutputTexture)
        {
            GetComponent<SkinnedMeshRenderer>().materials[materialIndex].SetTexture("_MainTex", RenderTextureCamera.CameraOutputTexture);
        }

	    else StartCoroutine(WaitForTexture());
*/
	}
	/*
    private IEnumerator WaitForTexture() 
    {

		yield return new WaitForSeconds (0.1f);

		if (RenderTextureCamera.CameraOutputTexture)
        {
            GetComponent<SkinnedMeshRenderer>().materials[materialIndex].SetTexture("_MainTex", RenderTextureCamera.CameraOutputTexture);
        }
		else StartCoroutine(WaitForTexture());

	}
*/
}