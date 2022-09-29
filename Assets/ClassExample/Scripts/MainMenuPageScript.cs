using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPageScript : MonoBehaviour
{
    public UnityEngine.UI.Text LoadText;
    private int LoadProgress = 5;

    public void GoToSettings()
    {
        // SceneManager.LoadScene("SettingsScene");
        StartCoroutine(SceneAsynchronousLoad("SettingsScene"));
    }

    public void GoToGame()
    {
        //SceneManager.LoadScene("GameScene");
        StartCoroutine(SceneAsynchronousLoad("GameScene"));

    }

    IEnumerator FakeLogin()
    {
        while(true)
        {

            // the following is a simple countdown timer.  In this block you would implement
            // your login and data retrieval code from a server
            yield return new WaitForSeconds(1);
            LoadProgress--;

            if (LoadProgress <= 0)
                break;

        }
        LoadText.text = "Welcome!\n Your Stats:\nRecord: 34-6\nXP: 567,897\nCoins: 432";

    }


    IEnumerator SceneAsynchronousLoad(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            LoadText.text = "Loading: " + (int)(progress * 100) + "%";

            if (ao.progress == 0.9f)
                ao.allowSceneActivation = true;

            yield return null;

        }


    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FakeLogin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
