using UnityEngine;
using TMPro;
using System.Collections;

public class ConsoleLine : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public float duration = 5f;
    public float fadeSpeed = 2f;

    void Start()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(duration);

        // Gradually fade the alpha to 0
        while (textElement.color.a > 0.01f)
        {
            Color c = textElement.color;
            c.a = Mathf.MoveTowards(c.a, 0, fadeSpeed * Time.deltaTime);
            textElement.color = c;
            yield return null;
        }

        Destroy(gameObject); 
    }
}