using UnityEngine;
using TMPro;

public class UnitHud : MonoBehaviour
{
    public Unit unit;
    public TextMeshProUGUI hpText;

    void Update()
    {
        if (unit != null && hpText != null)
        {
            hpText.text = $"HP: {unit.hp}";
        }
    }
}