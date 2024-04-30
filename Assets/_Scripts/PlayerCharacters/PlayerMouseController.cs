using GameUI;
using UnityEngine;


public class PlayerMouseController : MonoBehaviour
{
    public GameObject currentSelectedObject;
    private BattleSystem _battleSystem;
    private SpawnSystem _spawnSystem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ClickableObject"))
                {
                    currentSelectedObject = hit.collider.gameObject;
                    if (_battleSystem.state == BattleState.BattleMapSetup)
                    {
                        _spawnSystem.ShowCharacterContainer(currentSelectedObject);
                    }
                    else
                    {
                        _spawnSystem.HideCharacterContainer();
                    }
                    Debug.Log("Object " + currentSelectedObject.name + " found");
                }
            }
        }
    }
}
