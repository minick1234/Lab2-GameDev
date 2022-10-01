using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject LoadingCanvas;
    public TextMeshProUGUI LoadingText;

    public IEnumerator FakeLoadSceneAsync(string sceneToLoad, float LoadTime)
    {
        if (sceneToLoad == "Main Menu" && SceneManager.GetActiveScene().name == "Main Menu")
        {
            Debug.Log("i am in this area.");
            LoadingCanvas.SetActive(true);
            MainCanvas.SetActive(false);
            float currLoadTime = 0;
            float LastTenPercent = LoadTime - (LoadTime * 0.9f);
            while (currLoadTime <= LoadTime)
            {
                float progress = Mathf.Clamp01(currLoadTime / (LoadTime - LastTenPercent));
                LoadingText.text = "Loading " + sceneToLoad + "..." + (int)(progress * 100) + "%";

                yield return new WaitForSeconds(1f);
                currLoadTime++;
            }

            LoadingCanvas.SetActive(false);
            MainCanvas.SetActive(true);
        }
        else
        {
            LoadingCanvas.SetActive(true);
            MainCanvas.SetActive(false);
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad);
            ao.allowSceneActivation = false;
            float currLoadTime = 0;
            float LastTenPercent = LoadTime - (LoadTime * 0.9f);
            while (currLoadTime <= LoadTime)
            {
                float progress = Mathf.Clamp01(ao.progress / 0.9f);
                LoadingText.text = "Loading " + sceneToLoad + "..." + (int)(progress * 100) + "%";

                if (ao.progress >= 0.9f && (currLoadTime / (LoadTime - LastTenPercent)) >= 0.9f)
                {
                    ao.allowSceneActivation = true;
                }

                yield return new WaitForSeconds(1f);
                currLoadTime++;
            }

            LoadingCanvas.SetActive(false);
        }
    }

    //In order for you to see something i am using the function above as this would be to fast lol, even though it works.
    public IEnumerator RealSceneLoadAsynchronousLoad(string sceneToLoad)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            LoadingText.text = "Loading: " + (int)(progress * 100) + "%";

            if (ao.progress == 0.9f)
                ao.allowSceneActivation = true;

            yield return null;
        }
    }
}