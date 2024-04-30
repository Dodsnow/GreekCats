using CharactersParams;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    // public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public Slider mpSlider;


    // public void Start()
    // {
    //     ToggleElements(false);
    // }
    
    void ToggleElements(bool flag)
    {
        hpSlider.GameObject().SetActive(flag);
        mpSlider.GameObject().SetActive(flag);
        nameText.GameObject().SetActive(flag);
        // levelText.GameObject().SetActive(flag);
    }
    public void SetHUD(PlayableEntity character)
    {
        ToggleElements(true);
        nameText.text = character.characterName;
        // levelText.text = "Lvl " + character.characterParameters.characterStats[CharactersStats.characterLevel].CurrentValue.ToString();
        hpSlider.maxValue = character.CharacterParameters.CharacterStats[CharactersStats.MaxHealth].CurrentValue;
        hpSlider.value = character.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue;
        mpSlider.maxValue = character.CharacterParameters.CharacterStats[CharactersStats.MaxMana].CurrentValue;
        mpSlider.value = character.CharacterParameters.CharacterStats[CharactersStats.CurrentMana].CurrentValue;
        
    }
    
    public void SetHp(int hp)
    {
        hpSlider.value = hp;
        if (hpSlider.value <= 0)
        {
         ToggleElements(false);   
        }
    }
    
    public void SetMp(int mp)
    {
        mpSlider.value = mp;
        if(mpSlider.value <= 0)
        {
            ToggleElements(false);
        }
    }
}
