using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public bool bossArea = false;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossArea = true;
        if (collision.CompareTag("Player"))
        {
            animator.Play("Camera_2");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bossArea = false;
        if (collision.CompareTag("Player"))
        {
            animator.Play("Camera_1");
        }
    }
}
