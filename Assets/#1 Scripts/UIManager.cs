using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button attackButton;

    void Awake()
    {
        Instance = this;
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    void OnAttackButtonClicked()
    {
        TurnManager.Instance.PlayerAttack();
    }

    public void SetButtonInteractable(bool interactable)
    {
        attackButton.interactable = interactable;
    }
}