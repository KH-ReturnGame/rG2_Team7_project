using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // ← 반드시 추가

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button Skil1_Btn;
    public Button Skil2_Btn;
    public Button Skil3_Btn;
    public Button Heal_Btn;

    public GameObject gameOverPanel;               // 게임오버 패널
    public TextMeshProUGUI gameOverText;           // 🔸 TMPro 텍스트

    public Button restartButton;
    public Button quitButton;

    void Awake()
    {
        Instance = this;

        Skil1_Btn.onClick.AddListener(OnSkill1ButtonClicked);
        Skil2_Btn.onClick.AddListener(OnSkill2ButtonClicked);
        Skil3_Btn.onClick.AddListener(OnSkill3ButtonClicked);
        Heal_Btn.onClick.AddListener(OnHealButtonClicked);

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void OnSkill1ButtonClicked() => TurnManager.Instance.PlayerAttack(10);
    void OnSkill2ButtonClicked() => TurnManager.Instance.PlayerAttack(15);
    void OnSkill3ButtonClicked()
    {
        if (TurnManager.Instance.IsSkill3UsedThisTurn()) return;

        TurnManager.Instance.PlayerAttack(20);
        TurnManager.Instance.SetSkill3Used();
        SetSkill3Interactable(false);
    }
    void OnHealButtonClicked() => TurnManager.Instance.PlayerHeal(10);

    public void SetButtonInteractable(bool interactable)
    {
        Skil1_Btn.interactable = interactable;
        Skil2_Btn.interactable = interactable;
        Skil3_Btn.interactable = interactable;
    }

    public void SetSkill3Interactable(bool interactable)
    {
        Skil3_Btn.interactable = interactable;
    }

    public void SetHealButtonInteractable(bool interactable)
    {
        Heal_Btn.interactable = interactable;
    }

    public void ShowGameOverPanel(string winnerName)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = $"Game Over! \n Win: {winnerName}";
    }


    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
