// Scripts/Unit.cs
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int hp = 100;
    public int attack = 10;
    public bool isPlayer;

    protected TurnManager turnManager;

    void Start()
    {
        turnManager = TurnManager.Instance;
    }

    public virtual void StartTurn()
    {
        Debug.Log($"{unitName}의 턴 시작");
    }

    public void Attack()
    {
        Unit target = FindTarget();
        if (target != null)
        {
            Debug.Log($"{unitName}이 {target.unitName}에게 {attack} 데미지!");
            target.hp -= attack;
        }

        turnManager.EndTurn();
    }

    private Unit FindTarget()
    {
        foreach (Unit u in turnManager.units)
        {
            if (u != this)
                return u;
        }
        return null;
    }
}