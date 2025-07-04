using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button Skil1_Btn;
    public Button Skil2_Btn;
    public Button Skil3_Btn;
    public Button Heal_Btn;

    void Awake()
    {
        Instance = this;

        Skil1_Btn.onClick.AddListener(OnSkill1ButtonClicked);
        Skil2_Btn.onClick.AddListener(OnSkill2ButtonClicked);
        Skil3_Btn.onClick.AddListener(OnSkill3ButtonClicked);
        Heal_Btn.onClick.AddListener(OnHealButtonClicked);
    }

    void OnSkill1ButtonClicked()
    {
        TurnManager.Instance.PlayerAttack(10);
    }

    void OnSkill2ButtonClicked()
    {
        TurnManager.Instance.PlayerAttack(15);
    }

    void OnSkill3ButtonClicked()
    {
        TurnManager.Instance.PlayerAttack(20);
    }

    void OnHealButtonClicked()
    {
        TurnManager.Instance.PlayerHeal(10);
    }

    public void SetButtonInteractable(bool interactable)
    {
        if (Skil1_Btn != null) Skil1_Btn.interactable = interactable;
        if (Skil2_Btn != null) Skil2_Btn.interactable = interactable;
        if (Skil3_Btn != null) Skil3_Btn.interactable = interactable;
        if (Heal_Btn != null) Heal_Btn.interactable = interactable;
    }
}