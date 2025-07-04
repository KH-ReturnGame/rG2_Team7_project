using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int hp = 100;
    public int attack = 10;
    public bool isPlayer;

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;
    
    public GameObject floatingTextPrefab;

    private void Awake()
    {
        // 자식 포함 모든 SpriteRenderer 저장
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // 원래 색상 저장
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

    public void Attack(int damage)
    {
        Unit target = FindTarget();

        if (target != null)
        {
            target.TakeDamage(damage);

            if (target.hp <= 0)
            {
                Debug.Log($"{target.unitName}이(가) 쓰러졌습니다!");
                TurnManager.Instance.GameOver(unitName);
                return;
            }
        }

        TurnManager.Instance.EndTurn();
    }

    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        ShowFloatingText($"-{damage}", new Color(1f, 0.3f, 0.3f)); // 연한 빨강

        if (isPlayer)
        {
            StopCoroutine(nameof(FlashDamageEffect));
            StartCoroutine(FlashDamageEffect());
        }

        Debug.Log($"{unitName}이(가) {damage} 데미지를 입었습니다! 남은 체력: {hp}");
    }
 

    public void Heal(int amount)
    {
        hp += amount;
        ShowFloatingText($"+{amount}", new Color(0.4f, 1f, 0.4f)); // 연한 초록

        StopAllCoroutines();
        StartCoroutine(FlashHealEffect());

        Debug.Log($"{unitName}이(가) {amount}만큼 회복! 현재 체력: {hp}");
    }

    private IEnumerator FlashHealEffect()
    {
        Color healColor = new Color(178f / 255f, 251f / 255f, 151f / 255f);

        foreach (var sr in spriteRenderers)
        {
            sr.color = healColor;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }
    
    private IEnumerator FlashDamageEffect()
    {
        Color hitColor = new Color(253f / 255f, 124f / 255f, 124f / 255f);

        foreach (var sr in spriteRenderers)
        {
            sr.color = hitColor;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }

    private void ShowFloatingText(string text, Color color)
    {
        if (floatingTextPrefab == null) return;

        GameObject textGO = Instantiate(floatingTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        textGO.transform.SetParent(GameObject.Find("Canvas").transform, false); // UI용 캔버스 안에
        FloatingText ft = textGO.GetComponent<FloatingText>();
        ft.Setup(text, color);
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
