using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsGUI : MonoBehaviour 
{
	[Space(10)]
	public GameObject infoImage;
	public GameObject backButton;
	public GameObject nextButton;
	public GameObject startButton;

    [Space(10)]
    public GameObject[] takeRestScreenObjects;
    //[Space(10)]
    //public GameObject loadingImage;

	private int infoScreenNo = 0;
	private Sprite[] infoScreens;

    [Space(20)]
    [Header("GUI Based On Langauge")]
    public Sprite[] englishInfoSprites;
    public Sprite[] englishButtonSprites;
    [Space(10)]
    public Sprite[] russianInfoSprites;
    public Sprite[] russianButtonSprites;
    [Space(10)]
    public Sprite[] kazakhInfoSprites;
    public Sprite[] kazakhButtonSprites;

    [Space(20)]
    [Header("GUI Take rest")]
    public Sprite[] takeRestScreenEnglishSprites;
    public Sprite[] takeRestScreenRussianSprites;
    public Sprite[] takeRestScreenKazakhSprites;

    //[Space(10)]
    //public Sprite[] loadingSprites;


	void Start()
	{
		infoScreenNo = 0;
		infoImage.SetActive (true);
		backButton.SetActive (true);
		nextButton.SetActive (true);
		startButton.SetActive (true);

		if(SceneManager.GetActiveScene().name == "ColoringScene" || SceneManager.GetActiveScene().name == "ColoringSceneMR")
		{
			nextButton.SetActive (false);
		}


        string currentChoosenLanguage = PlayerPrefs.GetString("Language", "Russian");

        switch(currentChoosenLanguage)
        {
            case "English":

                infoScreens = new Sprite[englishInfoSprites.Length];

                for (int i = 0; i < infoScreens.Length; i++)
                {
                    infoScreens[i] = englishInfoSprites[i];
                }

                infoImage.GetComponent<Image>().sprite = englishInfoSprites[0];

                backButton.GetComponent<Image>().sprite = englishButtonSprites[0];
                nextButton.GetComponent<Image>().sprite = englishButtonSprites[1];
                startButton.GetComponent<Image>().sprite = englishButtonSprites[2];


                for (int j = 0; j < takeRestScreenObjects.Length; j++)
                {
                    takeRestScreenObjects[j].GetComponent<Image>().sprite = takeRestScreenEnglishSprites[j];
                }

                //loadingImage.GetComponent<Image>().sprite = loadingSprites[0];

                break;

            case "Russian":

                infoScreens = new Sprite[russianInfoSprites.Length];

                for (int i = 0; i < infoScreens.Length; i++)
                {
                    infoScreens[i] = russianInfoSprites[i];
                }

                infoImage.GetComponent<Image>().sprite = russianInfoSprites[0];

                backButton.GetComponent<Image>().sprite = russianButtonSprites[0];
                nextButton.GetComponent<Image>().sprite = russianButtonSprites[1];
                startButton.GetComponent<Image>().sprite = russianButtonSprites[2];


                for (int j = 0; j < takeRestScreenObjects.Length; j++)
                {
                    takeRestScreenObjects[j].GetComponent<Image>().sprite = takeRestScreenRussianSprites[j];
                }

                //loadingImage.GetComponent<Image>().sprite = loadingSprites[1];

                break;

            case "Kazakh":

                infoScreens = new Sprite[kazakhInfoSprites.Length];

                for (int i = 0; i < infoScreens.Length; i++)
                {
                    infoScreens[i] = kazakhInfoSprites[i];
                }

                infoImage.GetComponent<Image>().sprite = kazakhInfoSprites[0];

                backButton.GetComponent<Image>().sprite = kazakhButtonSprites[0];
                nextButton.GetComponent<Image>().sprite = kazakhButtonSprites[1];
                startButton.GetComponent<Image>().sprite = kazakhButtonSprites[2];


                for (int j = 0; j < takeRestScreenObjects.Length; j++)
                {
                    takeRestScreenObjects[j].GetComponent<Image>().sprite = takeRestScreenKazakhSprites[j];
                }

                //loadingImage.GetComponent<Image>().sprite = loadingSprites[2];

                break;

        }
	}

	public void AdvanceToNextScreen()
	{
		infoScreenNo++;

		if(infoScreenNo >= infoScreens.Length - 1)
		{
			nextButton.SetActive (false);
		}

		if(infoScreenNo >= infoScreens.Length)
		{
			infoScreenNo = infoScreens.Length;
		}

		if(infoScreenNo < infoScreens.Length)
		{
			infoImage.GetComponent<Image>().sprite = infoScreens [infoScreenNo];
		}
	}

	public void BackToPreviousScreen()
	{
		infoScreenNo--;


		if(infoScreenNo < 0)
		{
			SceneManager.LoadScene (0);
		}

		if(infoScreenNo < infoScreens.Length && infoScreenNo >= 0)
		{
			nextButton.SetActive (true);
			infoImage.GetComponent<Image> ().sprite = infoScreens [infoScreenNo];
		}

	}


	public void AdvanceToSceneDirectly()
	{
		infoImage.SetActive (false);
		backButton.SetActive (false);
		nextButton.SetActive (false);
		startButton.SetActive (false);
	}
}
