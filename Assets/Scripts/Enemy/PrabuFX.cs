using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrabuFX : MonoBehaviour
{
    [Header("Smoke Effects")]
    public Transform smoke;
    public GameObject smokePrefab;

    public void SmokeFX()
    {
        GameObject slashfx = Instantiate(smokePrefab, smoke.transform.position, smoke.transform.rotation);
        Vector3 originalScale = slashfx.transform.localScale;

        slashfx.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? originalScale.x : -originalScale.x,
            originalScale.y, originalScale.z);
    }
}
