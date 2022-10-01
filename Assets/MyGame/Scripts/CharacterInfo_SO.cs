using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterInformationInventory", menuName = "ScriptableObject/CharacterInfo")]
public class CharacterInfo_SO : ScriptableObject
{
    public Texture PlayerUISprite;
    public Mesh PlayerObject;

    public string HeroName;
    public string HeroClass;
    public string HeroHealth;
    public string HeroMana;
}