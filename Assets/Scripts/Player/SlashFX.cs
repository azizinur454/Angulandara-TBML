using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashFX : MonoBehaviour
{
    public float timeToDisappear;

    void Update()
    {
        Destroy(gameObject, timeToDisappear);
    }
}
