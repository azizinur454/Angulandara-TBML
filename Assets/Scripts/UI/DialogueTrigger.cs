using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    DialogueManager dialogueManager;

    public GameObject dialogue;

    public Message[] messages;
    public Actor[] actors;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Start()
    {
        dialogue.SetActive(false);
    }

    public void StartDialogue()
    {
        dialogue.SetActive(true);
        dialogueManager.OpenDialogue(messages, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
