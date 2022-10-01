using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterViewerManager : MonoBehaviour
{
    public SceneLoader sl;


    public GameObject CharacterContainer;
    public GameObject CharacterUIItem;

    public GameObject TutorialPanel;
    public GameObject TutorialYesButton;
    public GameObject TutorialNoButton;

    public GameObject TutorialNextButton;
    public TextMeshProUGUI TutorialMainText;

    public List<string> TutorialText = new List<string>();
    public int CurrTextIndex = 0;

    public ValidCharacterList VCL;
    public CharacterDetailsInfo CurrentItemDetailsInfo;

    public delegate void ChangeDetailsOfCharacter(CharacterDetailsInfo item);

    public event ChangeDetailsOfCharacter InventoryEvent;

    public TextMeshProUGUI HeroNameText;
    public TextMeshProUGUI HeroClassText;
    public TextMeshProUGUI HeroHealthText;
    public TextMeshProUGUI HeroManaText;

    public TextMeshProUGUI TempUIInventoryName;
    public RawImage TempUIInventorySprite;

    public SkinnedMeshRenderer PlayerViewedSkinnedMeshRenderer;
    
    public void NextTutorialText()
    {
        if (CurrTextIndex < TutorialText.Count - 1)
        {
            TutorialMainText.text = TutorialText[CurrTextIndex];
            CurrTextIndex++;
        }
        else
        {
            CloseTutorial();
        }
    }

    public void CloseTutorial()
    {
        PlayerPrefs.SetInt("TutorialFinished", 1);
        TutorialPanel.SetActive(false);
        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("TutorialFinished"));
    }

    public void YesTutorialWanted()
    {
        TutorialNextButton.SetActive(true);
        TutorialNoButton.SetActive(false);
        TutorialYesButton.SetActive(false);
        NextTutorialText();
    }

    public void NoTutorialNotWanted()
    {
        CloseTutorial();
    }

    // Start is called before the first frame update
    void Awake()
    {
        VCL = GameObject.Find("ValidCharacterList").GetComponent<ValidCharacterList>();
        //check if the playerpref exists, to check if we want to go through the tutorial or not.
        if (PlayerPrefs.GetInt("TutorialFinished") != 0)
        {
            TutorialPanel.SetActive(false);
        }
        else
        {
            TutorialPanel.SetActive(true);
            TutorialMainText.text =
                "We noticed that you have not played before, would you like a tutorial before you continue?";
        }

        //for each of the characters in the list fill out the viewer with all the information and when they are clicked we can select them and the information they contain and display it to the other
        //panels.
        for (int i = 0; i < VCL.characterInformationList.Count; i++)
        {
            GameObject tempCharObject =
                Instantiate(CharacterUIItem, Vector3.zero, Quaternion.identity);
            CharacterDetailsInfo CDI = tempCharObject.GetComponent<CharacterDetailsInfo>();
            tempCharObject.transform.SetParent(CharacterContainer.transform);
            tempCharObject.name = "Inventory_UI_Item_" + (i + 1);
            CDI.CurrentHerosName = VCL.characterInformationList[i].HeroName;
            CDI.CurrentHerosClass = VCL.characterInformationList[i].HeroClass;
            CDI.CurrentHerosMana = VCL.characterInformationList[i].HeroMana;
            CDI.CurrentHerosHealth = VCL.characterInformationList[i].HeroHealth;
            CDI.CurrentHeros3DModel = VCL.characterInformationList[i].PlayerObject;
            CDI.CurrentHerosInventorySprite = VCL.characterInformationList[i].PlayerUISprite;

            TempUIInventoryName = tempCharObject.transform.Find("HorizontalSplitter").Find("RightPanel")
                .Find("CharacterName")
                .GetComponent<TextMeshProUGUI>();
            TempUIInventorySprite = tempCharObject.transform.Find("HorizontalSplitter").Find("LeftPanel")
                .GetComponent<RawImage>();

            TempUIInventoryName.text = CDI.CurrentHerosName;
            TempUIInventorySprite.texture = CDI.CurrentHerosInventorySprite;
            tempCharObject.GetComponent<Button>().onClick.AddListener(ClickedInventoryItem);
        }

        InventoryEvent += OnClickedItem;

        CurrentItemDetailsInfo.CurrentHerosName = VCL.CurrentlySelectedCharacter.CurrentHerosName;
        CurrentItemDetailsInfo.CurrentHerosClass = VCL.CurrentlySelectedCharacter.CurrentHerosClass;
        CurrentItemDetailsInfo.CurrentHerosHealth = VCL.CurrentlySelectedCharacter.CurrentHerosHealth;
        CurrentItemDetailsInfo.CurrentHerosMana = VCL.CurrentlySelectedCharacter.CurrentHerosMana;


        CurrentItemDetailsInfo.CurrentHerosInventorySprite = VCL.CurrentlySelectedCharacter.CurrentHerosInventorySprite;
        CurrentItemDetailsInfo.CurrentHeros3DModel = VCL.CurrentlySelectedCharacter.CurrentHeros3DModel;
    }

    private void OnDisable()
    {
        InventoryEvent -= OnClickedItem;
    }

    public void LoadBackToMainMenu()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Main Menu", 3f));
    }

    public void LoadToCharacterAnimationViewer()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Character Animation Viewer", 3f));
    }

    public void ApplyCurrentCharacterAsSelected()
    {
        VCL.UpdateCurrentlySelectedCharacterStats(CurrentItemDetailsInfo);
    }

    public void ClickedInventoryItem()
    {
        InventoryEvent?.Invoke(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
            .GetComponent<CharacterDetailsInfo>());
    }

    public void OnClickedItem(CharacterDetailsInfo item)
    {
        //set the current item to the new selected item.
        CurrentItemDetailsInfo = item;
    }

    // Update is called once per frame
    void Update()
    {
        //update the character details constantly.
        HeroHealthText.text = CurrentItemDetailsInfo.CurrentHerosHealth;
        HeroClassText.text = CurrentItemDetailsInfo.CurrentHerosClass;
        HeroNameText.text = CurrentItemDetailsInfo.CurrentHerosName;
        HeroManaText.text = CurrentItemDetailsInfo.CurrentHerosMana;

        PlayerViewedSkinnedMeshRenderer.sharedMesh = CurrentItemDetailsInfo
            .CurrentHeros3DModel;
    }
}