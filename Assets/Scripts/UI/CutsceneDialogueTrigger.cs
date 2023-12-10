using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    CutsceneDialogueManager cutsceneDialogueManager;

    public CutsceneMessage[] messages;
    public CutsceneActor[] actors;

    private void Awake()
    {
        cutsceneDialogueManager = FindObjectOfType<CutsceneDialogueManager>();
    }

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        cutsceneDialogueManager.OpenDialogue(messages, actors);
    }
}

[System.Serializable]
public class CutsceneMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class CutsceneActor
{
    public string name;
}
