using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidAnimationsList : MonoBehaviour
{
    public static ValidAnimationsList instanceOfAnimationList;
    public List<AnimationDetailsInfo> ListOfAnimationDetails = new List<AnimationDetailsInfo>();

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

        if (ListOfAnimationDetails.Count > 11)
        {
            int NumberOfElementsAboveAllowed = ListOfAnimationDetails.Count - 11;
            ListOfAnimationDetails.RemoveRange(11, NumberOfElementsAboveAllowed);
        }
    }
}