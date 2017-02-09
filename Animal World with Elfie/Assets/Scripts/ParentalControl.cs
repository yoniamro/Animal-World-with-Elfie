using UnityEngine;
using System.Collections;

public class ParentalControl : MonoBehaviour
{
    [Header("Warning Message..")]
    public GameObject warningMenu;

    private float timerValue;

    void Start()
    {
        timerValue = PlayerPrefs.GetInt("COUNTERVALRUNTIME", 30 * 60);
    }

    void Update()
    {
        if (timerValue < 0)
        {
            timerValue = 0;

            if(warningMenu != null)
            {
                warningMenu.SetActive(true);
            }
        }

        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;

            PlayerPrefs.SetInt("COUNTERVALRUNTIME", (int)Mathf.Round(timerValue));
            PlayerPrefs.Save();
        }
    }


    public void BackFromWarningMenu()
    {

        if(warningMenu != null)
        {
            warningMenu.SetActive(false);
            timerValue = (PlayerPrefs.GetInt("COUNTERVAL", 30) * 60 >= int.MaxValue) ? int.MaxValue : PlayerPrefs.GetInt("COUNTERVAL", 30) * 60;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
