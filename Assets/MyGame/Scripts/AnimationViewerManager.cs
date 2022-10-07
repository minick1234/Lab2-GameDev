using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationViewerManager : MonoBehaviour
{
    public SceneLoader sl;

    public GameObject CharacterContainer;
    public GameObject AnimationUIContainer;

    public ValidAnimationsList VAL;
    public AnimationDetailsInfo CurrentAnimationDetails;

    public delegate void ChangeAnimationDetails(AnimationDetailsInfo item);

    public event ChangeAnimationDetails InventoryEvent;

    public SkinnedMeshRenderer PlayerViewedSkinnedMeshRenderer;

    public TextMeshProUGUI tempObjectName;

    public ValidCharacterList VCL;
    
    public Animator animatorThing;
    
    // Start is called before the first frame update
    void Awake()
    {
        VCL = GameObject.Find("ValidCharacterList").GetComponent<ValidCharacterList>();
        VAL = GameObject.Find("ValidAnimationsList").GetComponent<ValidAnimationsList>();

        //for each of the characters in the list fill out the viewer with all the information and when they are clicked we can select them and the information they contain and display it to the other
        //panels.
        for (int i = 0; i < VAL.ListOfAnimation_SOS.Count; i++)
        {
            GameObject tempADIUIObject =
                Instantiate(AnimationUIContainer, Vector3.zero, Quaternion.identity);
            AnimationDetailsInfo ADI = tempADIUIObject.GetComponent<AnimationDetailsInfo>();
            tempADIUIObject.transform.SetParent(CharacterContainer.transform);
            tempADIUIObject.name = "Animation_UI_Item_" + (i + 1);
            ADI.AnimationName = VAL.ListOfAnimation_SOS[i].AnimationName;
            tempObjectName = tempADIUIObject.transform.Find("RightPanel").Find("AnimationName")
                .GetComponent<TextMeshProUGUI>();
            tempObjectName.text = ADI.AnimationName;
            tempADIUIObject.GetComponent<Button>().onClick.AddListener(ClickedInventoryItem);
        }

        InventoryEvent += OnClickedItem;

        CurrentAnimationDetails.AnimationName = VAL.CurrentlySelectedAnimation.AnimationName;
    }

    private void OnDisable()
    {
        InventoryEvent -= OnClickedItem;
    }

    public void LoadBackToMainMenu()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Main Menu", 3f));
    }

    public void LoadToCharacterViewer()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Character Viewer", 3f));
    }

    public void PlayGame()
    {
        Debug.Log("maybe will implement this");
    }

    public void ClickedInventoryItem()
    {
        InventoryEvent?.Invoke(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
            .GetComponent<AnimationDetailsInfo>());
    }

    public void OnClickedItem(AnimationDetailsInfo item)
    {
        //set the current item to the new selected item.
        CurrentAnimationDetails = item;
        animatorThing.SetTrigger(item.AnimationName);
    }

    public void Update()
    {
        PlayerViewedSkinnedMeshRenderer.sharedMesh = VCL.CurrentlySelectedCharacter.CurrentHeros3DModel;
    }
}