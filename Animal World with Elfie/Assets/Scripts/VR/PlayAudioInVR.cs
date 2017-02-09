using UnityEngine;
using System.Collections;

public class PlayAudioInVR : MonoBehaviour 
{   

	public AudioClip animalSound;
	[Space(10)]
	public AudioClip[] animalName;
	[Space(10)]
	public AudioClip[] englishClips;
	[Space(10)]
	public AudioClip[] chineseClips;

	[Space(10)]
	public AudioClip[] russianClips;

	[Space(10)]
	public AudioClip[] kazakhClips;

    [Space(10)]
	public AudioSource source;
	public Animator anim;

	private int indexNumber;
	private string currentChoosenLanguage = "Russian";


    void Start()
    {
		currentChoosenLanguage = PlayerPrefs.GetString("Language", "Russian");
    }

    public void PlayInfoClipOnClick()
	{
		indexNumber++;

        switch (currentChoosenLanguage)
        {

            case "English":

				if (indexNumber > englishClips.Length)
				    indexNumber = 1;

				source.clip = englishClips[indexNumber - 1];
				break;

            case "Chinese":

                if (indexNumber > chineseClips.Length)
                    indexNumber = 1;

                source.clip = chineseClips[indexNumber - 1];
                break;

	     	case "Russian":

			    if (indexNumber > russianClips.Length)
				 	indexNumber = 1;

			    source.clip = russianClips[indexNumber - 1];
			    break;
		     
			case "Kazakh":

				if (indexNumber > kazakhClips.Length)
					indexNumber = 1;

				source.clip = kazakhClips[indexNumber - 1];
				break;


            default:

                if (indexNumber > englishClips.Length)
                    indexNumber = 1;

                source.clip = englishClips[indexNumber - 1];
                break;
        }

        source.Play();

    }

    public void PlayAnimalNameClipOnClick()
	{

        switch (currentChoosenLanguage)
        {
            case "English":
                source.clip = animalName[0];
                break;

            case "Chinese":
                source.clip = animalName[1];
                break;

		    case "Russian":
			    source.clip = animalName[2];
			    break;

			case "Kazakh":
				source.clip = animalName[3];
				break;

            default:
                source.clip = animalName[0];
                break;
        }

        source.Play();
    }

	public void PlayAnimalSoundClipOnClick()
	{
        if (!source.isPlaying)
        {
            source.clip = animalSound;
            source.Play();
        }
        anim.SetTrigger("Sound");
    }


		
}
