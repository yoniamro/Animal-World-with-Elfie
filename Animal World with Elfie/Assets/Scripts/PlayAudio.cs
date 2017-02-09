using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour 
{
	public AudioClip animalSound;
	[Space(10)]
	public AudioClip[] animalName;
	[Space(10)]
    [Header("English...")]
	public AudioClip[] englishClips;
	[Space(10)]
	[Header("Chinese...")]
    public AudioClip[] chineseClips;
	[Header("Russian...")]
	public AudioClip[] russianClips;
	[Header("Kazakh...")]
	public AudioClip[] kazakhClips;
   
    [Space(10)]
	public AudioSource source;
	public Animator anim;
	
    [HideInInspector]
    public int indexNumber;
    [HideInInspector]
    public int maxArrayLength;
    [HideInInspector]
    public float currentClipLength;


    [Space(10)]
    public UserSettings userSettings;

	private string currentChoosenLanguage = "Russian";


    void Start()
    {
		currentChoosenLanguage = PlayerPrefs.GetString("Language", "Russian");

        switch(currentChoosenLanguage)
        {
            case "English":
                maxArrayLength = englishClips.Length;
                break;
            case "Chinese":
                maxArrayLength = chineseClips.Length;
                break;
	    	case "Russian":
			    maxArrayLength = russianClips.Length;
		    	break;
			case "Kazakh":
				maxArrayLength = kazakhClips.Length;
				break;
            default:
                maxArrayLength = englishClips.Length;
                break;
        }

    }

    public void PlayInfoClipOnClick()
	{
		//if (!source.isPlaying) 
		//{
		indexNumber++;

        switch (currentChoosenLanguage)
        {
            case "English":

                if (indexNumber > englishClips.Length)
                    indexNumber = 1;

                source.clip = englishClips[indexNumber - 1];

                currentClipLength = source.clip.length;
                break;

            case "Chinese":

                if (indexNumber > chineseClips.Length)
                    indexNumber = 1;

                source.clip = chineseClips[indexNumber - 1];

                currentClipLength = source.clip.length;
                break;

		    case "Russian":

				if (indexNumber > russianClips.Length)
					indexNumber = 1;

				source.clip = russianClips[indexNumber - 1];

				currentClipLength = source.clip.length;
				break;

		   case "Kazakh":

				if (indexNumber > kazakhClips.Length)
					indexNumber = 1;

				source.clip = kazakhClips[indexNumber - 1];

				currentClipLength = source.clip.length;
				break;

            default:

                if (indexNumber > englishClips.Length)
                    indexNumber = 1;

                source.clip = englishClips[indexNumber - 1];
                currentClipLength = source.clip.length;
                break;
        }

		if (userSettings != null)
        {		
			userSettings.FadeBGMusicVolume (currentClipLength + 0.5f);
		}

        source.Play();
        //}

    }

	public void PlayAnimalNameClipOnClick()
	{
        //if (!source.isPlaying)
        //{
        switch (currentChoosenLanguage)
        {
            case "English":
                source.clip = animalName[0];
                currentClipLength = source.clip.length;
                break;

            case "Chinese":

                source.clip = animalName[1];
                currentClipLength = source.clip.length;
                break;

            case "Russian":

                source.clip = animalName[2];
                currentClipLength = source.clip.length;
                break;

	        case "Kazakh":

		        source.clip = animalName[3];
		        currentClipLength = source.clip.length;
				break;

            default:

                source.clip = animalName[0];
                currentClipLength = source.clip.length;
                break;
        }

		if (userSettings != null) {
			
			userSettings.FadeBGMusicVolume(currentClipLength + 0.5f);
		}
		
		source.Play ();
        //}
    }

	public void PlayAnimalSoundClipOnClick()
	{
        //if (!source.isPlaying)
        //{
        source.clip = animalSound;
        currentClipLength = source.clip.length;

		if (userSettings != null)
        {
			userSettings.FadeBGMusicVolume (currentClipLength + 0.5f);
		}

        source.Play();
        
        anim.SetTrigger("Sound");
    }

    public AudioSource GetAudioSource()
    {
        return source;
    }

	
}
