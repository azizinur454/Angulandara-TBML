using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public TMP_Text hiddenNotifText;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowHiddenPathNotif()
    {
        if (canvasGroup != null)
        {
            if (canvasGroup.gameObject != null)
            {
                canvasGroup.alpha = 1f;
                hiddenNotifText.text = "You've uncovered a hidden route!";

                StartCoroutine(NotificationAfterDelay(3f));
            }
        }
    }

    private IEnumerator NotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        canvasGroup.alpha = 0f;
    }
}
