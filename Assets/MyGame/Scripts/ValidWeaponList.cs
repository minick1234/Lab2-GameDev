using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidWeaponList : MonoBehaviour
{
    public static ValidWeaponList instanceOfWeaponList;

    public WeaponDetailsInfo CurrentlySelectedWeapon;

    public List<WeaponDetailsInfo> WeaponCollectionList = new List<WeaponDetailsInfo>();


    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instanceOfWeaponList == null)
        {
            instanceOfWeaponList = this;
        }
        else
        {
            Destroy(gameObject);
        }

        int counter = 0;
        foreach (var weapon in WeaponCollectionList)
        {
            weapon.CurrWeaponIndex = counter;
            counter++;
        }

        if (CurrentlySelectedWeapon.CurrentWeaponName != null ||
            CurrentlySelectedWeapon.CurrentWeaponDescription != null ||
            CurrentlySelectedWeapon.CurrentWeaponMesh != null)
        {
            CurrentlySelectedWeapon.CurrentWeaponName = WeaponCollectionList[0].CurrentWeaponName;
            CurrentlySelectedWeapon.CurrentWeaponDescription = WeaponCollectionList[0].CurrentWeaponDescription;
            CurrentlySelectedWeapon.CurrentWeaponMesh = WeaponCollectionList[0].CurrentWeaponMesh;
        }
    }
}