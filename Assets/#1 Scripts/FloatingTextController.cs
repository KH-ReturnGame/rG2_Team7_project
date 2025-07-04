using UnityEngine;
using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingTextController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float duration = 0.5f;

    private Coroutine hideRoutine;

    public void ShowText(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
        gameObject.SetActive(true);

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
