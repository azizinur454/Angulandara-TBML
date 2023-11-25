using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    PlayerController player;

    private Coroutine typewriter;

    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public GameObject Dialogue;
    public float dialogueSpeed = 0f;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        player.isDialogue = true;

        Debug.Log("Loaded Messages: " + messages.Length);
        DisplayMessage();
    }

    public void OpenAlternativeDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        player.isDialogue = true;

        Debug.Log("Loaded Messages: " + messages.Length);
        DisplayAlternativeMessage();
    }

    public void DisplayMessage()
    {
        if (typewriter != null)
        {
            StopCoroutine(typewriter);
        }

        Message messageToDisplay = currentMessages[activeMessage];
        string messageTextContent = messageToDisplay.message;

        messageText.text = "";
        typewriter = StartCoroutine(ShowTextWithTypewriter(messageTextContent));

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void DisplayAlternativeMessage()
    {
        if (typewriter != null)
        {
            StopCoroutine(typewriter);
        }

        Message messageToDisplay = currentMessages[activeMessage];
        string messageTextContent = messageToDisplay.message;

        messageText.text = "";
        typewriter = StartCoroutine(ShowTextWithTypewriter(messageTextContent));

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    IEnumerator ShowTextWithTypewriter(string text)
    {
        int index = 0;
        while (index < text.Length)
        {
            messageText.text += text[index++];
            yield return new WaitForSeconds(dialogueSpeed / 100);
        }

        typewriter = null;
    }

    public void NextMessage()
    {
        activeMessage++;

        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation end");
            player.isDialogue = false;
            Dialogue.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonUp(0) && player.isDialogue == true)
        {
            SoundManager.Instance.Play("Button");
            NextMessage();
        }
    }
}
