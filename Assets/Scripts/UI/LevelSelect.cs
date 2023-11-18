using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] private int maxPage;
    private int currentPage;

    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] Button prevBtn, nextBtn;

    public float tweenTime;
    public LeanTweenType tweenType;

    private void Start()
    {
        LeanTween.reset();
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        UpdateButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    public void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateButton();
    }

    public void UpdateButton()
    {
        nextBtn.interactable = true;
        prevBtn.interactable = true;
        if (currentPage == 1)
        {
            prevBtn.interactable = false;
        }
        else if (currentPage == maxPage)
        {
            nextBtn.interactable = false;
        }
    }

    public void SelectLevel()
    {
        if(currentPage == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(currentPage == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    public void ClickButtonSound()
    {
        SoundManager.Instance.Play("Button");
    }

    public void SwipeSound()
    {
        SoundManager.Instance.Play("Swipe");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
