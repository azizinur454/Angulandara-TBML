using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    [Header("Arrow Spawner")]
    public Transform launchPoint;
    public GameObject arrowPrefab;

    [Header("Slash Effects")]
    public Transform slash;
    public GameObject slashPrefab;
    public void ArrowLaunch()
    {
        GameObject arrow = Instantiate(arrowPrefab, launchPoint.transform.position, arrowPrefab.transform.rotation);
        Vector3 originalScale = arrow.transform.localScale;

        arrow.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? originalScale.x : -originalScale.x,
            originalScale.y, originalScale.z);
    }

    public void SlashFX()
    {
        GameObject slashfx = Instantiate(slashPrefab, slash.transform.position, slashPrefab.transform.rotation);
        Vector3 originalScale = slashfx.transform.localScale;

        slashfx.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? originalScale.x : -originalScale.x,
            originalScale.y, originalScale.z);
    }
}
