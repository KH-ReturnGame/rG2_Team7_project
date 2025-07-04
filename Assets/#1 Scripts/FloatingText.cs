using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float floatUpSpeed = 1f;
    public float duration = 1f;

    private float timer;

    public void Setup(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
    }

    void Update()
    {
        transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}