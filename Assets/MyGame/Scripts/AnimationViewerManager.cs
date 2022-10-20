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

    public GameObject UpperRightButtonsPanel;
    public GameObject OutLines;
    public GameObject UpperMiddleUIPanel;
    public GameObject LeftCharacterContainer;

    public GameObject MiddleCharacterViewer;
    public GameObject AnimationViewerCamera;
    public GameObject PlayerObject;

    public ValidAnimationsList VAL;
    public AnimationDetailsInfo CurrentAnimationDetails;

    public int Direction;

    [SerializeField] public Touch TouchyTouchy;
    [SerializeField] private Vector2 touchStartingPoint, touchEndingPoint;
    [SerializeField] private string TouchedDirection;
    [SerializeField] private PlayerController player;


    public delegate void ChangeAnimationDetails(AnimationDetailsInfo item);

    public event ChangeAnimationDetails InventoryEvent;

    public SkinnedMeshRenderer PlayerViewedSkinnedMeshRenderer;

    public TextMeshProUGUI tempObjectName;

    public ValidCharacterList VCL;

    public Animator animatorThing;


    public bool IsGameOn;


    // Start is called before the first frame update
    void Awake()
    {
        VCL = GameObject.Find("ValidCharacterList").GetComponent<ValidCharacterList>();
        VAL = GameObject.Find("ValidAnimationsList").GetComponent<ValidAnimationsList>();

        //for each of the characters in the list fill out the viewer with all the information and when they are clicked we can select them and the information they contain and display it to the other
        //panels.

        for (int i = 0; i < VAL.ListOfAnimationDetails.Count - 2; i++)
        {
            GameObject tempADIUIObject =
                Instantiate(AnimationUIContainer, Vector3.zero, Quaternion.identity);
            AnimationDetailsInfo ADI = tempADIUIObject.GetComponent<AnimationDetailsInfo>();
            tempADIUIObject.transform.SetParent(CharacterContainer.transform);
            tempADIUIObject.name = "Animation_UI_Item_" + (i + 1);
            ADI.AnimationName = VAL.ListOfAnimationDetails[i].AnimationName;
            tempObjectName = tempADIUIObject.transform.Find("AnimationName")
                .GetComponent<TextMeshProUGUI>();
            tempObjectName.text = ADI.AnimationName;
            tempADIUIObject.GetComponent<Button>().onClick.AddListener(ClickedInventoryItem);
        }

        InventoryEvent += OnClickedItem;

        CurrentAnimationDetails.AnimationName = VAL.ListOfAnimationDetails[0].AnimationName;
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

    public void StartPlayGame()
    {
        IsGameOn = true;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerObject.SetActive(true);
        AnimationViewerCamera.SetActive(false);
        MiddleCharacterViewer.SetActive(false);
        UpperMiddleUIPanel.SetActive(true);
        UpperRightButtonsPanel.SetActive(false);
        LeftCharacterContainer.SetActive(false);
        OutLines.SetActive(false);
    }

    public void StopPlayingGame()
    {
        IsGameOn = false;
        Cursor.lockState = CursorLockMode.None;
        PlayerObject.SetActive(false);
        AnimationViewerCamera.SetActive(true);
        MiddleCharacterViewer.SetActive(true);
        UpperMiddleUIPanel.SetActive(false);
        UpperRightButtonsPanel.SetActive(true);
        LeftCharacterContainer.SetActive(true);
        OutLines.SetActive(true);
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
        //We will do all the mobile stuff here, which wont be to organized but i dont have alot of time to code something nice right now.
        //since i am using the new unity system i could use that but again its quiccker to prototype this way right now and if need be i will change it in the future loool.

#if UNITY_ANDRIOID || UNITY_IOS
        //Initial input checks 
        if (Input.touchCount > 0)
        {
            TouchyTouchy = Input.GetTouch(0);

            if (TouchyTouchy.phase == TouchPhase.Began)
            {
                touchStartingPoint = TouchyTouchy.position;
            }
            else if (TouchyTouchy.phase == TouchPhase.Moved || TouchyTouchy.phase == TouchPhase.Ended)
            {
                touchEndingPoint = TouchyTouchy.position;

                float x = touchEndingPoint.x - touchStartingPoint.x;
                float y = touchEndingPoint.y - touchStartingPoint.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    Direction = 4;
                    TouchedDirection = "Tapped";
                    Debug.Log("tappping.");
                }else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    Direction = x > 0 ? 1 : 3;
                    if (Direction == 1)
                    {
                        TouchedDirection = "Right";
                    }
                    else
                    {
                        TouchedDirection = "Left";
                    }
                }
                else
                {
                    Direction = y > 0 ? 0 : 2;
                    if (Direction == 0)
                    {
                        TouchedDirection = "Up";
                    }
                    else
                    {
                        TouchedDirection = "Down";
                    }
                }
                
                if (IsGameOn)
                {
                    CheckGameOnMobileInputs(Direction);
                }
                else
                {
                    CheckAnimationViewerMobileInputs(Direction);
                }
            }
        }
#endif

        PlayerViewedSkinnedMeshRenderer.sharedMesh = VCL.CurrentlySelectedCharacter.CurrentHeros3DModel;
    }

    public void CheckGameOnMobileInputs(int direction)
    {
        if (direction == 1 || direction == 3)
        {
            player.ShootFireBallMOBILE();
        }
    }

    public void CheckAnimationViewerMobileInputs(int direction)
    {
        //swiping up and down runs these secret animations
        if (direction == 0)
        {
            CurrentAnimationDetails.AnimationName = "SecretAnimation00";
            OnClickedItem(CurrentAnimationDetails);
        }
        else if (direction == 2)
        {
            CurrentAnimationDetails.AnimationName = "SecretAnimation01";
            OnClickedItem(CurrentAnimationDetails);
        }
        else if (direction == 4)
        {
            //run the next animation from the current list if tapping.   
            if (CurrentAnimationDetails.AnimationName == "SecretAnimation00" || CurrentAnimationDetails
                    .AnimationName == "SecretAnimation01")
            {
                CurrentAnimationDetails = VAL.ListOfAnimationDetails[0];
            }
            else
            {
                int index = 0;
                for (int i = 0; i < VAL.ListOfAnimationDetails.Count - 1; i++)
                {
                    if (CurrentAnimationDetails == VAL.ListOfAnimationDetails[i])
                    {
                        index = i;
                        break;
                    }
                }

                if (index + 1 > VAL.ListOfAnimationDetails.Count - 1)
                {
                    CurrentAnimationDetails = VAL.ListOfAnimationDetails[0];
                }
                else if (index + 1 <= VAL.ListOfAnimationDetails.Count - 1)
                {
                    CurrentAnimationDetails = VAL.ListOfAnimationDetails[index + 1];
                }
            }

            OnClickedItem(CurrentAnimationDetails);
        }
    }
}