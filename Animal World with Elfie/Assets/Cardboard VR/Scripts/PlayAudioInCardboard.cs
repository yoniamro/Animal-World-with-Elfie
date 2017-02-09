using UnityEngine;
using System.Collections;

public class PlayAudioInCardboard : MonoBehaviour 
{
    [Space(10)]
    public AudioClip[] englishClips;
    [Space(10)]
    public AudioClip[] chineseClips;
    //[Space(10)]
   // public AudioClip[] spanishClips;
    [Space(10)]
    public AudioClip[] russianClips;
	[Space(10)]
	public AudioClip[] kazakhClips;


    [Space(10)]
	public AudioSource source;
	public Animator anim;

    [Space(10)]
    [Header("Lara's Animator...")]
    public Animator characterAnimator;

	private int indexNumber;
    private string currentChoosenLanguage = "Russian";

    void Start()
    {
		currentChoosenLanguage = PlayerPrefs.GetString("Language", "Russian");
    }

    public void PlayClipOnClick()
	{
		indexNumber++;

        switch (currentChoosenLanguage)
        {

            case "English":

                if (indexNumber > englishClips.Length)
                    indexNumber = 1;

                if (indexNumber - 1 == 0)
                {
                    anim.SetTrigger("Sound");
                }

                source.clip = englishClips[indexNumber - 1];
                break;

            case "Chinese":

                if (indexNumber > chineseClips.Length)
                    indexNumber = 1;

                if (indexNumber - 1 == 0)
                {
                    anim.SetTrigger("Sound");
                }

                source.clip = chineseClips[indexNumber - 1];
                break;

//            case "Spanish":
//
//                if (indexNumber > spanishClips.Length)
//                    indexNumber = 1;
//
//                if (indexNumber - 1 == 0)
//                {
//                    anim.SetTrigger("Sound");
//                }
//
//                source.clip = spanishClips[indexNumber - 1];
//                break;

            case "Russian":

                if (indexNumber > russianClips.Length)
                    indexNumber = 1;

				if (indexNumber - 1 == 0)
				{
					anim.SetTrigger("Sound");
				}

                source.clip = russianClips[indexNumber - 1];
                break;

		    case "Kazakh":

				if (indexNumber > kazakhClips.Length)
					indexNumber = 1;

				if (indexNumber - 1 == 0)
				{
					anim.SetTrigger("Sound");
				}

				source.clip = kazakhClips[indexNumber - 1];
				break;

            default:

                if (indexNumber > englishClips.Length)
                    indexNumber = 1;

                if (indexNumber - 1 == 0)
                {
                    anim.SetTrigger("Sound");
                }

                source.clip = englishClips[indexNumber - 1];
                break;
        }

        source.Play ();

		if (source.isPlaying && (indexNumber - 1) != 0) 
		{
			if (characterAnimator != null) 
			{
				
				characterAnimator.SetTrigger ("Explain");
			}
		}
	}
}
