using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    PlayerRespawn playerRespawn;
    LevelLoader levelLoader;

    [Header("UI Settings")]
    public GameObject damageText;
    public GameObject healthText;
    public GameObject scoreText;

    [Header("Menu Settings")]
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject pauseMenuWithCheckpoint;
    public GameObject loseMenuWithCheckpoint;
    public GameObject CompletionMenu;

    public Canvas gameCanvas;

    [Header("State Menu Condition")]
    public bool isPaused = false;
    public bool isLose = false;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        playerRespawn = FindObjectOfType<PlayerRespawn>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        loseMenu.SetActive(false);
        pauseMenuWithCheckpoint.SetActive(false);
        loseMenuWithCheckpoint.SetActive(false);
        CompletionMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLose)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                if (playerRespawn.currentCheckpoint == null)
                {
                    PauseGame();
                }
                else
                {
                    PauseGameWithCheckpoint();
                }
            }
        }
    }

    private void OnEnable()
    {
        CharacterEvent.characterDamaged += (CharacterTakeDamage);
        CharacterEvent.characterHealed += (CharacterHeal);
        CharacterEvent.scores += (GetScores);
    }

    private void OnDisable()
    {
        CharacterEvent.characterDamaged -= (CharacterTakeDamage);
        CharacterEvent.characterHealed -= (CharacterHeal);
        CharacterEvent.scores -= (GetScores);
    }

    public void CharacterTakeDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate
            (damageText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHeal(GameObject character, string healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate
            (healthText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }

    public void GetScores(GameObject objects, int scores)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(objects.transform.position);

        TMP_Text tmpText = Instantiate
            (scoreText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = scores.ToString();
    }

    public void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
    }

    public void PauseGameWithCheckpoint()
    {
        isPaused = true;

        Time.timeScale = 0f;

        pauseMenuWithCheckpoint.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;

        pauseMenu.SetActive(false);
        pauseMenuWithCheckpoint.SetActive(false);

        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        isPaused = false;
        isLose = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        CompletionMenu.SetActive(false);

        isPaused = false;

        Time.timeScale = 1f;
        levelLoader.Invoke("LoadNextLevel", levelLoader.delayTransitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartFromCheckpoint()
    {
        isPaused = false;
        isLose = false;

        pauseMenuWithCheckpoint.SetActive(false);
        loseMenuWithCheckpoint.SetActive(false);

        loseMenu.SetActive(false);
        Time.timeScale = 1f;

        playerRespawn.ReloadCheckpoint();
    }

    public void BackToMenu()
    {
        isPaused = false;
        isLose = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoseGame()
    {
        isLose = true;
        loseMenu.SetActive(true);
        SoundManager.Instance.Play("Lose");
        SoundManager.Instance.Stop("Stage1");
        SoundManager.Instance.Stop("BossBGM");
    }

    public void LoseGameWithCheckpoint()
    {
        isLose = true;
        loseMenuWithCheckpoint.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("This game in exit state");
        Application.Quit();
    }

    public void ClickButtonSound()
    {
        SoundManager.Instance.Play("Button");
    }
}
