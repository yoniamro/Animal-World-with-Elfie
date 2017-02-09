using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour {

	[Header("English...")]
	public Sprite[] englishUI;
	[Header("Russian...")]
	public Sprite[] russianUI;
	[Header("Kazakh...")]
	public Sprite[] kazakhUI;
//	[Header("Chinese...")]
//	public Sprite[] chineseUI;


	public GameObject[] UIComponents;

	private string currentChoosenLanguage = "Russian";
	private Image myImageComponent;
	private Button[] button;

	void Start ()
	{

		currentChoosenLanguage = PlayerPrefs.GetString("Language", "Russian");

		ChangeUISprite (currentChoosenLanguage);

	
	}


	public void ChangeUISprite(string languageName)
	{

		switch (languageName) 
		{
			case "English":

				for (int i = 0; i < UIComponents.Length; i++)
				{

					myImageComponent = UIComponents [i].GetComponent<Image>();
					myImageComponent.sprite = englishUI[i];


				}
				break;

			case "Russian":

				for (int i = 0; i < UIComponents.Length; i++)
				{

					myImageComponent = UIComponents [i].GetComponent<Image>();
					myImageComponent.sprite = russianUI[i];

				}
				break;

			case "Kazakh":

				for (int i = 0; i < UIComponents.Length; i++) 
				{

					myImageComponent = UIComponents [i].GetComponent<Image>();
					myImageComponent.sprite = kazakhUI[i];

				}
				break;
//		    case "Chinese":
//
//			     for (int i = 0; i < UIComponents.Length; i++) 
//			    {
//
//				   myImageComponent = UIComponents [i].GetComponent<Image>();
//				   myImageComponent.sprite = chineseUI[i];
//
//			    }
//			    break;
			  
		}

	}




}
