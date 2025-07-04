using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int hp = 100;
    public int attack = 10;
    public bool isPlayer;

    public FloatingTextController floatingTextController;

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }
    }

    public void StartTurn()
    {
        Debug.Log($"{unitName}의 턴입니다.");
    }

    public void Attack(int baseDamage)
    {
        Unit target = FindTarget();
        if (target == null) return;

        int finalDamage = isPlayer ? baseDamage : Random.Range(10, 26); // 10~25
        target.TakeDamage(finalDamage, unitName);
    }

    public void TakeDamage(int damage, string attackerName)
    {
        hp -= damage;
        hp = Mathf.Max(hp, 0);

        ShowFloatingText($"-{damage}", new Color(1f, 0.3f, 0.3f));
        StartCoroutine(FlashDamageEffect());

        if (hp <= 0)
        {
            TurnManager.Instance.GameOver(attackerName);
            return;
        }
    }

    public void Heal(int amount)
    {
        if (hp >= 100)
        {
            Debug.Log("체력이 가득 찼습니다.");
            return;
        }

        int healAmount = Mathf.Min(amount, 100 - hp);
        hp += healAmount;

        ShowFloatingText($"+{healAmount}", new Color(0.4f, 1f, 0.4f));
        StartCoroutine(FlashHealEffect());


        if (isPlayer)
            UIManager.Instance.SetHealButtonInteractable(hp < 100);
    }

    private void ShowFloatingText(string text, Color color)
    {
        if (floatingTextController != null)
        {
            floatingTextController.ShowText(text, color);
        }
    }

    private IEnumerator FlashHealEffect()
    {
        Color healColor = new Color(178f / 255f, 251f / 255f, 151f / 255f);
        foreach (var sr in spriteRenderers) sr.color = healColor;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < spriteRenderers.Length; i++) spriteRenderers[i].color = originalColors[i];
    }

    private IEnumerator FlashDamageEffect()
    {
        Color hitColor = new Color(253f / 255f, 124f / 255f, 124f / 255f);
        foreach (var sr in spriteRenderers) sr.color = hitColor;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < spriteRenderers.Length; i++) spriteRenderers[i].color = originalColors[i];
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
