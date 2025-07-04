using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public TextMeshProUGUI turnText;

    public List<Unit> units;
    private int currentIndex = 0;
    private bool gameOver = false;
    
    private bool skill3UsedThisTurn = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.SetButtonInteractable(false);
        UIManager.Instance.SetHealButtonInteractable(false);
        StartTurn();
    }

    void StartTurn()
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        turnText.text = $"{currentUnit.unitName}'s Turn";

        Debug.Log($"<color=yellow>{currentUnit.unitName}의 턴 시작!</color>");
        Debug.Log($"{currentUnit.unitName} 체력: {currentUnit.hp}");

        if (currentUnit.isPlayer)
        {
            skill3UsedThisTurn = false; // 다음 턴이면 다시 사용 가능
            UIManager.Instance.SetButtonInteractable(true);
            UIManager.Instance.SetHealButtonInteractable(currentUnit.hp < 100);
            UIManager.Instance.SetSkill3Interactable(true); // 다시 켜주기
        }
        else
        {
            UIManager.Instance.SetButtonInteractable(false);
            UIManager.Instance.SetHealButtonInteractable(false);
            Invoke(nameof(EnemyAction), 1f);
        }
    }


    void EnemyAction()
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        currentUnit.Attack(currentUnit.attack); // 적은 랜덤 데미지 처리됨

        if (!gameOver)
        {
            EndTurn();
        }
    }

    public void PlayerAttack(int damage)
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        if (currentUnit.isPlayer)
        {
            currentUnit.Attack(damage);
            EndTurn();
        }
    }

    public void PlayerHeal(int amount)
    {
        if (gameOver) return;

        Unit currentUnit = units[currentIndex];
        if (currentUnit.isPlayer)
        {
            currentUnit.Heal(amount);
            EndTurn();
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
        UIManager.Instance.SetHealButtonInteractable(false);
        UIManager.Instance.SetSkill3Interactable(false);

        UIManager.Instance.ShowGameOverPanel(winnerName);
    }

    
    public bool IsSkill3UsedThisTurn()
    {
        return skill3UsedThisTurn;
    }

    public void SetSkill3Used()
    {
        skill3UsedThisTurn = true;
        
        
    }

}
