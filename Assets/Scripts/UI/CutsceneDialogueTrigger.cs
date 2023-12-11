using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    CutsceneDialogueManager cutsceneDialogueManager;

    public CutsceneMessage[] messages;
    public CutsceneActor[] actors;
    public GameObject dialog;

    private void Awake()
    {
        cutsceneDialogueManager = FindObjectOfType<CutsceneDialogueManager>();
    }

    private void Start()
    {
        StartCoroutine(DelayStartDialogue(4f));
    }

    public void StartDialogue()
    {
        dialog.SetActive(true);
        cutsceneDialogueManager.OpenDialogue(messages, actors);
    }

    IEnumerator DelayStartDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);

        StartDialogue();
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
