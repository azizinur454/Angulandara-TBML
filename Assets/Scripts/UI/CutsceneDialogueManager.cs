using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneDialogueManager : MonoBehaviour
{
    Animator animator;

    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public float dialogueSpeed = 0f;
    public GameObject dialogue;
    public GameObject[] pictures;

    private Coroutine typewriter;

    CutsceneMessage[] currentMessages;
    CutsceneActor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDialogue(CutsceneMessage[] messages, CutsceneActor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Loaded Messages: " + messages.Length);

        DisplayMessage();
    }

    public void DisplayMessage()
    {
        if (typewriter != null)
        {
            StopCoroutine(typewriter);
        }

        CutsceneMessage messageToDisplay = currentMessages[activeMessage];
        string messageTextContent = messageToDisplay.message;

        messageText.text = "";
        typewriter = StartCoroutine(ShowTextWithTypewriter(messageTextContent));

        CutsceneActor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
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
        ChangeScene();

        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }

        else
        {
            Debug.Log("Conversation End");
            isActive = false;
            dialogue.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ChangeScene()
    {
        if(activeMessage == 2)
        {
            pictures[0].SetActive(false);
            pictures[1].SetActive(true);
        }

        else if(activeMessage == 5)
        {
            pictures[1].SetActive(false);
            pictures[2].SetActive(true);
        }

        else if (activeMessage == 9)
        {
            pictures[2].SetActive(false);
            pictures[3].SetActive(true);
        }

        else if (activeMessage == 10)
        {
            pictures[3].SetActive(false);
            pictures[4].SetActive(true);
        }

        else if (activeMessage == 11)
        {
            pictures[4].SetActive(false);
            pictures[5].SetActive(true);
        }

        else if (activeMessage == 12)
        {
            pictures[5].SetActive(false);
            pictures[6].SetActive(true);
        }

        else if (activeMessage == 14)
        {
            pictures[6].SetActive(false);
            pictures[7].SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonUp(0) && isActive == true)
        {
            SoundManager.Instance.Play("Click");
            NextMessage();
        }
    }
}
