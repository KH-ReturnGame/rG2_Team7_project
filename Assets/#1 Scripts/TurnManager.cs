using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public List<Unit> units;
    private int currentTurnIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartTurn();
    }

    void StartTurn()
    {
        Unit currentUnit = units[currentTurnIndex];
        currentUnit.StartTurn();

        if (currentUnit.isPlayer)
        {
            UIManager.Instance.SetButtonInteractable(true);
        }
        else
        {
            UIManager.Instance.SetButtonInteractable(false);
            Invoke(nameof(AutoEnemyTurn), 1f);
        }
    }

    void AutoEnemyTurn()
    {
        Unit currentUnit = units[currentTurnIndex];
        currentUnit.Attack();  
    }

    public void PlayerAttack()
    {
        Unit currentUnit = units[currentTurnIndex];
        if (currentUnit.isPlayer)
        {
            currentUnit.Attack(); 
        }
    }

    public void EndTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % units.Count;
        StartTurn();
    }
}