using UnityEngine;
using System.Collections;

public class AudioPlayState : MonoBehaviour
{
	public AudioSource audioSource;

	private SkinnedMeshRenderer mesh;

	void Start()
	{
		mesh = GetComponent<SkinnedMeshRenderer> ();	
	}

	void Update()
	{
		if (mesh.enabled)
		{
			if (!audioSource.isPlaying)
				audioSource.Play ();
		}
		else if (!mesh.enabled)
			audioSource.Stop ();
	}

}
