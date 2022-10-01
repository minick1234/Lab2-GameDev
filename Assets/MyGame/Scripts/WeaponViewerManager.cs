using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponViewerManager : MonoBehaviour
{
    public SceneLoader sl;

    public GameObject SpawnableWeaponItemUI;

    public WeaponDetailsInfo CurrentWeaponDetailsInfo;
    public WeaponDetailsInfo PreviousWeaponDetail;
    public WeaponDetailsInfo NextWeaponDetail;

    public GameObject WeaponObjectCentre;
    public GameObject WeaponObjectPrevious;
    public GameObject WeaponObjectNext;

    public Transform MiddlePoint;
    public Transform RightPoint;
    public Transform LeftPoint;

    public TextMeshProUGUI WeaponDescriptionText;
    public TextMeshProUGUI WeaponNameText;

    public ValidWeaponList VWL;

    public float TransitionTime = 2f;
    public float CurrSlerpTime = 0;
    public float fracOfJourneyComplete = 0;
    public bool IAmSlerping = false;
    public bool IAmSlerpingNew = false;
    public float TransitionMovementMultiplier;

    public void LoadBackToMainMenu()
    {
        StartCoroutine(sl.FakeLoadSceneAsync("Main Menu", 3f));
    }

    public void GoToNextWeaponItem()
    {
        if (!IAmSlerping)
        {
            CurrSlerpTime = Time.time;
            fracOfJourneyComplete = 0;

            WeaponObjectNext = Instantiate(SpawnableWeaponItemUI, RightPoint.transform.position, Quaternion.identity);

            if (CurrentWeaponDetailsInfo.CurrWeaponIndex + 1 > VWL.WeaponCollectionList.Count - 1)
            {
                WeaponObjectNext.transform.Find("SkinnedMesh").GetComponent<SkinnedMeshRenderer>().sharedMesh =
                    VWL.WeaponCollectionList[0].CurrentWeaponMesh;

                CurrentWeaponDetailsInfo.CurrentWeaponName =
                    VWL.WeaponCollectionList[0].CurrentWeaponName;
                CurrentWeaponDetailsInfo.CurrentWeaponMesh =
                    VWL.WeaponCollectionList[0].CurrentWeaponMesh;
                CurrentWeaponDetailsInfo.CurrentWeaponDescription = VWL
                    .WeaponCollectionList[0].CurrentWeaponDescription;
                CurrentWeaponDetailsInfo.CurrWeaponIndex =
                    VWL.WeaponCollectionList[0].CurrWeaponIndex;
            }
            else if (CurrentWeaponDetailsInfo.CurrWeaponIndex + 1 <= VWL.WeaponCollectionList.Count - 1)
            {
                WeaponObjectNext.transform.Find("SkinnedMesh").GetComponent<SkinnedMeshRenderer>().sharedMesh =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex + 1].CurrentWeaponMesh;

                CurrentWeaponDetailsInfo.CurrentWeaponName =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex + 1].CurrentWeaponName;
                CurrentWeaponDetailsInfo.CurrentWeaponMesh =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex + 1].CurrentWeaponMesh;
                CurrentWeaponDetailsInfo.CurrentWeaponDescription = VWL
                    .WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex + 1].CurrentWeaponDescription;
                CurrentWeaponDetailsInfo.CurrWeaponIndex =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex + 1].CurrWeaponIndex;
            }

            IAmSlerping = true;
            IAmSlerpingNew = true;
        }
    }

    public void GoToPreviousWeaponItem()
    {
        if (!IAmSlerping)
        {
            CurrSlerpTime = Time.time;
            fracOfJourneyComplete = 0;

            WeaponObjectPrevious =
                Instantiate(SpawnableWeaponItemUI, LeftPoint.transform.position, Quaternion.identity);

            if (CurrentWeaponDetailsInfo.CurrWeaponIndex - 1 < 0)
            {
                WeaponObjectPrevious.transform.Find("SkinnedMesh").GetComponent<SkinnedMeshRenderer>().sharedMesh =
                    VWL.WeaponCollectionList[VWL.WeaponCollectionList.Count - 1].CurrentWeaponMesh;

                CurrentWeaponDetailsInfo.CurrentWeaponName =
                    VWL.WeaponCollectionList[VWL.WeaponCollectionList.Count - 1].CurrentWeaponName;
                CurrentWeaponDetailsInfo.CurrentWeaponMesh =
                    VWL.WeaponCollectionList[VWL.WeaponCollectionList.Count - 1].CurrentWeaponMesh;
                CurrentWeaponDetailsInfo.CurrentWeaponDescription = VWL
                    .WeaponCollectionList[VWL.WeaponCollectionList.Count - 1].CurrentWeaponDescription;
                CurrentWeaponDetailsInfo.CurrWeaponIndex =
                    VWL.WeaponCollectionList[VWL.WeaponCollectionList.Count - 1].CurrWeaponIndex;
            }
            else if (CurrentWeaponDetailsInfo.CurrWeaponIndex - 1 >= 0)
            {
                WeaponObjectPrevious.transform.Find("SkinnedMesh").GetComponent<SkinnedMeshRenderer>().sharedMesh =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex - 1].CurrentWeaponMesh;

                CurrentWeaponDetailsInfo.CurrentWeaponName =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex - 1].CurrentWeaponName;
                CurrentWeaponDetailsInfo.CurrentWeaponMesh =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex - 1].CurrentWeaponMesh;
                CurrentWeaponDetailsInfo.CurrentWeaponDescription = VWL
                    .WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex - 1].CurrentWeaponDescription;
                CurrentWeaponDetailsInfo.CurrWeaponIndex =
                    VWL.WeaponCollectionList[CurrentWeaponDetailsInfo.CurrWeaponIndex - 1].CurrWeaponIndex;
            }

            IAmSlerping = true;
            IAmSlerpingNew = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        WeaponObjectCentre = Instantiate(SpawnableWeaponItemUI, MiddlePoint.transform.position, Quaternion.identity);
        CurrentWeaponDetailsInfo.CurrentWeaponName = VWL.CurrentlySelectedWeapon.CurrentWeaponName;
        CurrentWeaponDetailsInfo.CurrentWeaponMesh = VWL.CurrentlySelectedWeapon.CurrentWeaponMesh;
        CurrentWeaponDetailsInfo.CurrentWeaponDescription = VWL.CurrentlySelectedWeapon.CurrentWeaponDescription;
        CurrentWeaponDetailsInfo.CurrWeaponIndex = VWL.CurrentlySelectedWeapon.CurrWeaponIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (IAmSlerping)
        {
            fracOfJourneyComplete = (Time.time - CurrSlerpTime) / TransitionTime;
            if (IAmSlerpingNew)
            {
                WeaponObjectCentre.transform.position = Vector3.Slerp(WeaponObjectCentre.transform.position,
                    LeftPoint.position, TransitionMovementMultiplier * Time.deltaTime);
                WeaponObjectNext.transform.position = Vector3.Slerp(WeaponObjectNext.transform.position,
                    MiddlePoint.position, TransitionMovementMultiplier * Time.deltaTime);
            }
            else
            {
                WeaponObjectCentre.transform.position = Vector3.Slerp(WeaponObjectCentre.transform.position,
                    RightPoint.position, TransitionMovementMultiplier * Time.deltaTime);
                WeaponObjectPrevious.transform.position = Vector3.Slerp(WeaponObjectPrevious.transform.position,
                    MiddlePoint.position, TransitionMovementMultiplier * Time.deltaTime);
            }

            if (fracOfJourneyComplete >= 0.05f)
            {
                VWL.CurrentlySelectedWeapon.CurrentWeaponName = CurrentWeaponDetailsInfo.CurrentWeaponName;
                VWL.CurrentlySelectedWeapon.CurrentWeaponDescription =
                    CurrentWeaponDetailsInfo.CurrentWeaponDescription;
                VWL.CurrentlySelectedWeapon.CurrentWeaponMesh = CurrentWeaponDetailsInfo.CurrentWeaponMesh;
                VWL.CurrentlySelectedWeapon.CurrWeaponIndex = CurrentWeaponDetailsInfo.CurrWeaponIndex;
            }

            if (fracOfJourneyComplete >= 1f)
            {
                GameObject tempObject = WeaponObjectCentre;
                if (IAmSlerpingNew)
                {
                    WeaponObjectCentre = WeaponObjectNext;
                    WeaponObjectNext = tempObject;
                    Destroy(WeaponObjectNext);
                }
                else
                {
                    WeaponObjectCentre = WeaponObjectPrevious;
                    WeaponObjectPrevious = tempObject;
                    Destroy(WeaponObjectPrevious);
                }

                IAmSlerping = false;
            }
        }

        if (!IAmSlerping)
        {
            WeaponObjectCentre.transform.position = new Vector3(WeaponObjectCentre.transform.position.x,
                WeaponObjectCentre.transform.position.y, 0f);
        }

        //update the value of the name and descriptions
        WeaponDescriptionText.text = VWL.CurrentlySelectedWeapon.CurrentWeaponDescription;
        WeaponNameText.text = VWL.CurrentlySelectedWeapon.CurrentWeaponName;

        //Update the weaponMesh whenever the value changes.
        WeaponObjectCentre.transform.Find("SkinnedMesh").GetComponent<SkinnedMeshRenderer>().sharedMesh =
            VWL.CurrentlySelectedWeapon.CurrentWeaponMesh;
    }
}