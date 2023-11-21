using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Collider2D col2D;

    public DialogueTrigger trigger;

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            trigger.StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        col2D.enabled = false;
    }
}
