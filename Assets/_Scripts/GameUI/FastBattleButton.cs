using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBattleButton : MonoBehaviour
{
    public BattleSystem battleSystem;
    public void FastBattleButtonClick()
    {
        if (battleSystem.combatSpeedModificator == 0.5f)
        {
            Debug.Log("Normal Battle Mode");
            battleSystem.combatSpeedModificator = 1f;
        }
        else if (battleSystem.combatSpeedModificator == 1f)
        {
            Debug.Log("Fast Battle Mode");
            battleSystem.combatSpeedModificator = 0.5f;
        }
    }
}
