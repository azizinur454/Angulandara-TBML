using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Collider2D col2D;
    PlayerController player;

    public DialogueTrigger trigger;

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            if(player.isArtifactPrabuComplete)
            {
                trigger.StartDialogue();
            }
            else if(!player.isArtifactPrabuComplete)
            {
                trigger.StartAlternativeDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        col2D.enabled = false;
    }
}
