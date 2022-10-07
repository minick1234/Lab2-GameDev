using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidAnimationsList : MonoBehaviour
{
    public static ValidAnimationsList instanceOfAnimationList;
    public List<AnimationInfo_SO> ListOfAnimation_SOS = new List<AnimationInfo_SO>();
    public AnimationDetailsInfo CurrentlySelectedAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instanceOfAnimationList == null)
        {
            instanceOfAnimationList = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (ListOfAnimation_SOS.Count > 11)
        {
            int NumberOfElementsAboveAllowed = ListOfAnimation_SOS.Count - 11;
            ListOfAnimation_SOS.RemoveRange(11, NumberOfElementsAboveAllowed);
        }

        CurrentlySelectedAnimation.AnimationName = ListOfAnimation_SOS[0].AnimationName;
    }
}