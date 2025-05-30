using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button attackButton;
    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    public void OnAttackButtonClicked()
    {
        TurnManager.Instance.PlayerAttack();
    }

    public void SetButtonInteractable(bool interactable)
    {
        attackButton.interactable = interactable;
    }
}