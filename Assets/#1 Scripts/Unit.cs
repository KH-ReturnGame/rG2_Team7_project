using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int hp = 100;
    public int attack = 10;
    public bool isPlayer;

    public void StartTurn()
    {
        Debug.Log($"{unitName}의 턴입니다.");
    }

    public void Attack()
    {
        Unit target = FindTarget();

        if (target != null)
        {
            target.hp -= attack;
            Debug.Log($"{unitName}이(가) {target.unitName}에게 {attack} 데미지!");
            Debug.Log($"{target.unitName}의 남은 체력: {target.hp}");

            if (target.hp <= 0)
            {
                Debug.Log($"{target.unitName}이(가) 쓰러졌습니다!");
                TurnManager.Instance.GameOver(unitName); // 나의 승리로 게임 종료
                return;
            }
        }

        TurnManager.Instance.EndTurn();
    }

    private Unit FindTarget()
    {
        foreach (Unit unit in TurnManager.Instance.units)
        {
            if (unit != this && unit.hp > 0)
                return unit;
        }

        return null;
    }
}