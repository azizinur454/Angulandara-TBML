using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public bool isFinish = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Finish();
            SoundManager.Instance.Stop("Stage1");
            SoundManager.Instance.Play("Complete");
        }
    }

    private void Finish()
    {
        isFinish = true;
    }
}
