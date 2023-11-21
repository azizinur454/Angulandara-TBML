using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    PlayerController player;

    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public GameObject Dialogue;

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

    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        string messageTextContent = messageToDisplay.message;

        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        messageText.text = "";

        for (int i = 0; i < messageTextContent.Length; i++)
        {
            char currentChar = messageTextContent[i];

            LeanTween.delayedCall(i * 0.1f, () =>
            {
                messageText.text += currentChar.ToString();
            });
        }
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
            NextMessage();
        }
    }
}
