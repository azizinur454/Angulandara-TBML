using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenPath : MonoBehaviour
{
    Notification notification;
    public GameObject hiddenPath;

    private void Awake()
    {
        notification = FindObjectOfType<Notification>();
    }

    private void OnDestroy()
    {
        if (hiddenPath != null && hiddenPath.activeSelf)
        {
            hiddenPath.SetActive(false);
            notification?.ShowHiddenPathNotif();
        }
    }
}
