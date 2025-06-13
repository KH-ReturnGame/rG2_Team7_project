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
        Skil1_Btn.onClick.AddListener(OnAttackButtonClicked);
        Skil2_Btn.onClick.AddListener(OnAttackButtonClicked);
        Skil3_Btn.onClick.AddListener(OnAttackButtonClicked);
        Heal_Btn.onClick.AddListener(OnAttackButtonClicked);
    }

    void OnAttackButtonClicked()
    {
        TurnManager.Instance.PlayerAttack();
    }

    public void SetButtonInteractable(bool interactable)
    {
        Skil1_Btn.interactable = interactable;
        Skil2_Btn.interactable = interactable;
        Skil3_Btn.interactable = interactable;
        Heal_Btn.interactable = interactable;
    }
}