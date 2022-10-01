using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDetailsInfo
{
    [SerializeField]
    public int CurrWeaponIndex;
    [SerializeField]
    public string CurrentWeaponName;
    [SerializeField]
    public string CurrentWeaponDescription;
    [SerializeField]
    public GameObject CurrentWeaponMesh;
    
}
