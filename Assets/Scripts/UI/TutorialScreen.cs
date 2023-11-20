using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public GameObject tutorialScreen;

    public float fadeDuration = 1.0f;
    public float slideDistance = 100.0f;

    private RectTransform popUpRectTransform;
    private CanvasGroup popUpCanvasGroup;

    private bool isAnimating = false;
    public bool isTutorial = false;

    private void Awake()
    {
        popUpRectTransform = tutorialScreen.GetComponent<RectTransform>();
        popUpCanvasGroup = tutorialScreen.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        LeanTween.reset();
        tutorialScreen.SetActive(false);
        Invoke("PopUpTutorial", 1f);
    }

    public void PopUpTutorial()
    {
        if (isAnimating)
        {
            return;
        }

        isTutorial = true;

        tutorialScreen.SetActive(true);

        SoundManager.Instance.Play("Swipe");

        RectTransform rectTransform = tutorialScreen.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, slideDistance);

        CanvasGroup canvasGroup = tutorialScreen.GetComponent<CanvasGroup>();
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
        isTutorial = false;

        LeanTween.alphaCanvas(popUpCanvasGroup, 0.0f, fadeDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                isAnimating = false;
                tutorialScreen.SetActive(false);
            });

        LeanTween.moveY(popUpRectTransform, slideDistance, fadeDuration)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
