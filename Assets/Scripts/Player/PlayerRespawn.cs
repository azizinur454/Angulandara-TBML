using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentCheckpoint;

    Damage playerhealth;
    UIManager uiManager;

    private void Awake()
    {
        playerhealth = GetComponent<Damage>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnPlayer()
    {
        if (currentCheckpoint == null)
        {
            uiManager.LoseGame();
        }
        else
        {
            uiManager.LoseGameWithCheckpoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Checkpoint");
        }
    }

    public void ReloadCheckpoint()
    {
        transform.position = currentCheckpoint.position;
        playerhealth.Respawn();
    }
}
