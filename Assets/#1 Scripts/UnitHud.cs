using UnityEngine;
using TMPro;

public class UnitHud : MonoBehaviour
{
    public Unit unit;  // 체력을 표시할 유닛
    public TextMeshProUGUI hpText;  // 머리 위 텍스트

    void Update()
    {
        if (unit != null && hpText != null)
        {
            hpText.text = $"HP: {unit.hp}";
        }
    }
}

