using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePageScript : MonoBehaviour
{
    public string[] TutorialText;
    public int TutorialNum = 0;

    public Text TutorialLabel;
    public GameObject TutorialWindow;


    public void NextTutorialMessage()
    {
        TutorialNum++;

        if(TutorialNum >= TutorialText.Length)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
            TutorialWindow.gameObject.SetActive(false);
        }
        else
        {
            TutorialLabel.text = TutorialText[TutorialNum];
        }
    }


    public void GoHome()
    {
        SceneManager.LoadScene("HomePage");
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("TutorialDone"))
        {
            if (PlayerPrefs.GetInt("TutorialDone") == 1) // not necessary
            {
                TutorialWindow.gameObject.SetActive(false);
            }
        }
        else
        {
            TutorialLabel.text = TutorialText[TutorialNum];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
