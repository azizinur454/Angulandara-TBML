using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerState : MonoBehaviour
{
    CameraSwitcher cameraSwitcher;
    Damage damage;

    private void Awake()
    {
        damage = GetComponent<Damage>();
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        ReduceInvincibleTimeAtRoro();
    }

    public void ReduceInvincibleTimeAtRoro()
    {
        if (cameraSwitcher.bossArea)
        {
            damage.invicibilityTime = 0.5f;
        }

        else
        {
            damage.invicibilityTime = 1f;
        }
    }
}
