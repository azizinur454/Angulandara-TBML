using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject openCreditBtn, closedCreditBtn, creditsScreen;
    public float fadeDuration = 1.0f;
    public float slideDistance = 100.0f;

    private RectTransform popUpRectTransform;
    private CanvasGroup popUpCanvasGroup;

    private bool isAnimating = false;

    private void Awake()
    {
        popUpRectTransform = creditsScreen.GetComponent<RectTransform>();
        popUpCanvasGroup = creditsScreen.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        LeanTween.reset();
        creditsScreen.SetActive(false);
    }

    public void PopUpCredits()
    {
        if (isAnimating)
        {
            return;
        }

        creditsScreen.SetActive(true);
        openCreditBtn.SetActive(false);
        closedCreditBtn.SetActive(true);

        SoundManager.Instance.Play("Swipe");

        RectTransform rectTransform = creditsScreen.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, slideDistance);

        CanvasGroup canvasGroup = creditsScreen.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        isAnimating = true;

        LeanTween.alphaCanvas(popUpCanvasGroup, 1.0f, fadeDuration)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() => isAnimating = false);

        LeanTween.moveY(popUpRectTransform, 0, fadeDuration)
            .setEase(LeanTweenType.easeInQuad);
    }

    public void HidePopUp()
    {
        if (isAnimating)
        {
            return;
        }

        isAnimating = true;

        openCreditBtn.SetActive(true);
        closedCreditBtn.SetActive(false);

        LeanTween.alphaCanvas(popUpCanvasGroup, 0.0f, fadeDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                isAnimating = false; 
                creditsScreen.SetActive(false);
            });

        LeanTween.moveY(popUpRectTransform, slideDistance, fadeDuration)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
