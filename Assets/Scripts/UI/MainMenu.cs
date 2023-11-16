using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickButtonSound()
    {
        SoundManager.Instance.Play("Button");
    }

    public void ExitGame()
    {
        Debug.Log("This game on exit state");
        Application.Quit();
    }
}
