using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private float timeElapsed = 0f;
    private Color color;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        color = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if(timeElapsed < timeToFade)
        {
            float fadeAlpha = color.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(color.r, color.g, color.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
