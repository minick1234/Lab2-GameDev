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

    public TextMeshProUGUI ButtonText;
    public bool IsLab3Open = false;
    public bool IsAnimationFinished = true;
    public Animator MainMenuAnimator;
    public float NextAnimation = 0f;

    public float currTime;
    public float timeDelayBetweenAnimations = 5f;

    public void Start()
    {
        MainMenuAnimator.SetBool("FirstTime", true);
    }

    // Update is called once per frame
    void Update()
    {
        currTime = Time.time;
        //this is just for show, its easier then trying to find the variable in the regedit and changing / deleting it each time,
        //this is onyl because i want to be able to demonstrate it to you.
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("TutorialFinished", 0);
            Debug.Log("Ye reset ye playerprefs lad.");
        }

        if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !MainMenuAnimator.IsInTransition(0))
        {
            IsAnimationFinished = true;

            if (IsLab3Open)
            {
                if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                    !MainMenuAnimator.IsInTransition(0))
                {
                    MainMenuAnimator.SetBool("PlayAnimation", true);
                }
            }
        }
        else
        {
            IsAnimationFinished = false;
        }

        if (Time.time >= NextAnimation && IsLab3Open &&
            MainMenuAnimator.GetBool("PlayAnimation"))
        {
            NextAnimation = Time.time + timeDelayBetweenAnimations;
            MainMenuAnimator.SetTrigger("Slide");
            Debug.Log("Player Loop Animation");
        }
    }

    public void OpenCloseLab3()
    {
        MainMenuAnimator.SetBool("FirstTime", false);
        if (!IsLab3Open && IsAnimationFinished)
        {
            ButtonText.text = "OPEN MAIN MENU";
            IsLab3Open = true;
            MainMenuAnimator.SetBool("IsLab3Open", IsLab3Open);
            NextAnimation = Time.time + timeDelayBetweenAnimations - 3;
        }
        else if (IsLab3Open && IsAnimationFinished)
        {
            ButtonText.text = "OPEN LAB 3";
            IsLab3Open = false;
            MainMenuAnimator.SetBool("IsLab3Open", IsLab3Open);
            MainMenuAnimator.SetBool("PlayAnimation", false);
        }
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