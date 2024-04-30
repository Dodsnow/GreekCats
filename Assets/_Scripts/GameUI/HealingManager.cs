using CharactersParams;
using UnityEngine;

public class HealingManager : MonoBehaviour
{
    public bool CheckForRemainingHealth(PlayableEntity target)
    {
       
        return target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue /
            target.CharacterParameters.CharacterStats[CharactersStats.MaxHealth].CurrentValue <= 0.5f;
    }


    public int Heal(PlayableEntity source, PlayableEntity target)
    {
        int healValue = source.CharacterParameters.CharacterStats[CharactersStats.MagicDamage].CurrentValue;
        target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth].CurrentValue += 2 * healValue;
        target.spawnPoint.battleHUD.SetHp(target.CharacterParameters.CharacterStats[CharactersStats.CurrentHealth]
            .CurrentValue);

        return healValue * 2;
    }
}