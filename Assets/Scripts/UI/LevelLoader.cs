using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator animator;
    StageClear stageClear;
    UIManager uiManager;
    Score scoreManager;

    public float transitionTime = 1f;
    public float delayTransitionTime = 3f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stageClear = FindObjectOfType<StageClear>();
        uiManager = FindObjectOfType<UIManager>();
        scoreManager = FindObjectOfType<Score>();
    }

    void Update()
    {
        if (stageClear.isFinish)
        {
            uiManager.isPaused = true;
            StartCoroutine(ShowCompletionMenuAfterDelay());
            scoreManager.CompletionScoreStage();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator ShowCompletionMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayTransitionTime);

        uiManager.CompletionMenu.SetActive(true);
    }
}
