using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValidCharacterList : MonoBehaviour
{
    public static ValidCharacterList instanceOfCharacterList;
    public List<CharacterInfo_SO> characterInformationList = new List<CharacterInfo_SO>();
    public CharacterDetailsInfo CurrentlySelectedCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instanceOfCharacterList == null)
        {
            instanceOfCharacterList = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (characterInformationList.Count > 11)
        {
            int NumberOfElementsAboveAllowed = characterInformationList.Count - 11;
            characterInformationList.RemoveRange(11, NumberOfElementsAboveAllowed);
        }

        CurrentlySelectedCharacter.CurrentHerosName = characterInformationList[0].HeroName;
        CurrentlySelectedCharacter.CurrentHerosClass = characterInformationList[0].HeroClass;

        CurrentlySelectedCharacter.CurrentHerosMana = characterInformationList[0].HeroMana;
        CurrentlySelectedCharacter.CurrentHerosHealth = characterInformationList[0].HeroHealth;

        CurrentlySelectedCharacter.CurrentHeros3DModel = characterInformationList[0].PlayerObject;
        CurrentlySelectedCharacter.CurrentHerosInventorySprite = characterInformationList[0].PlayerUISprite;
    }

    public void UpdateCurrentlySelectedCharacterStats(CharacterDetailsInfo info)
    {
        CurrentlySelectedCharacter.CurrentHerosName = info.CurrentHerosName;
        CurrentlySelectedCharacter.CurrentHerosClass = info.CurrentHerosClass;

        CurrentlySelectedCharacter.CurrentHerosMana = info.CurrentHerosMana;
        CurrentlySelectedCharacter.CurrentHerosHealth = info.CurrentHerosHealth;

        CurrentlySelectedCharacter.CurrentHeros3DModel = info.CurrentHeros3DModel;
        CurrentlySelectedCharacter.CurrentHerosInventorySprite = info.CurrentHerosInventorySprite;
    }
}