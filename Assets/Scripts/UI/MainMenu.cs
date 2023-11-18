using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.Instance.Play("Button");
        Invoke("PlayNextScene", 0.3f);
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

    public void PlayNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
