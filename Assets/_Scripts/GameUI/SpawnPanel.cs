using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPanel : MonoBehaviour
{
    public SpriteRenderer[] characterSprite;
    public int[] characterPanelID;
    public  Slider slider;


    public void CharacterPaneltoSliderValue()
    {
        for (int i = 0; i <= characterPanelID.Length; i++)
        {
            characterPanelID[i] = (int)slider.value;
        }
    }
}