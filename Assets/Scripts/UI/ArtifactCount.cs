using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactCount : MonoBehaviour
{
    PlayerController player;
    CanvasGroup canvasGroup;

    public TMP_Text amountText;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public void ShowAmountArtifact()
    {
        canvasGroup.alpha = 1f;
        amountText.text = "Artifact " + player.artifactAmount + "/3 Acquired" ;

        StartCoroutine(HideAmountArtifactAfterDelay(3f));
    }

    private IEnumerator HideAmountArtifactAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        canvasGroup.alpha = 0f;
    }
}
