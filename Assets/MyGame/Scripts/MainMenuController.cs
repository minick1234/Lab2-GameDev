using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public SceneLoader sl;

    public GameObject LabThreeCanvas;
    public GameObject MainFocusedMenu;
    
    public Animation LabThreeSlideOut;
    public Animation LabThreeSlideIn;

    public Animation MainMenuSlideOut;
    public Animation MainMenuslideIn;

    public Animation LabThreeImageSideSlideOut;
    public Animation LabThreeImageSideSlideIn;

    public Animation LabThreeImageCenterSizeInOut;

    public Animation ButtonAnimationForFadeInOut;
    
    public float timeDelayBetweenAnimations = 5f;
    public bool IsLabThreeActive = false;

    // Update is called once per frame
    void Update()
    {
        //this is just for show, its easier then trying to find the variable in the regedit and changing / deleting it each time,
        //this is onyl because i want to be able to demonstrate it to you.
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("TutorialFinished", 0);
            Debug.Log("Ye reset ye playerprefs lad.");
        }
    }

    public void OpenLabThreeCanvas()
    {
        LabThreeCanvas.SetActive(true);
        MainFocusedMenu.SetActive(false);
    }

    public void CloseLabThreeCanvas()
    {
        LabThreeCanvas.SetActive(false);
        MainFocusedMenu.SetActive(true);
    }


    public void LoadCharacterViewScene()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Character Viewer", 3f));
    }

    public void LoadWeaponViewScene()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Weapon Viewer", 3f));
    }

    public void LoadAboutUsScene()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("About Us", 3f));
    }

    public void LoadCharacterAnimationPreviewScene()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Character Animation Viewer", 3f));
    }
}