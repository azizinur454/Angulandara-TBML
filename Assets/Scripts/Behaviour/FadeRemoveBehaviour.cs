using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;

    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color color;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        float newAlpha = color.a * (1 - (timeElapsed / fadeTime));

        spriteRenderer.color = new Color(color.r, color.g, color.b, newAlpha);

        if (timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }
}
