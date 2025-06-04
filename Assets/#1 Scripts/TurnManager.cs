using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public List<Unit> units;
    private int currentIndex = 0;
    private bool gameOver = false;

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
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        Debug.Log($"<color=yellow>{currentUnit.unitName}의 턴 시작!</color>");
        Debug.Log($"{currentUnit.unitName} 체력: {currentUnit.hp}");

        if (currentUnit.isPlayer)
        {
            UIManager.Instance.SetButtonInteractable(true);
        }
        else
        {
            UIManager.Instance.SetButtonInteractable(false);
            Invoke(nameof(EnemyAction), 1f);
        }
    }

    void EnemyAction()
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        currentUnit.Attack();
    }

    public void PlayerAttack()
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        if (currentUnit.isPlayer)
        {
            currentUnit.Attack();
        }
    }

    public void EndTurn()
    {
        if (gameOver) return;

        currentIndex = (currentIndex + 1) % units.Count;
        StartTurn();
    }

    public void GameOver(string winnerName)
    {
        gameOver = true;
        Debug.Log($"<color=red>게임 종료! 승리자: {winnerName}</color>");
        UIManager.Instance.SetButtonInteractable(false);
    }
}