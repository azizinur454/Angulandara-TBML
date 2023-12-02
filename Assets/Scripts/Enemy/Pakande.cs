using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pakande : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public Transform player;
    public Transform attackLoc;
    public DetectionZone attackZone;
    public GameObject attackPrefab;

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat("attackCooldown");
        }
        set
        {
            animator.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (HasTarget)
        {
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        else
        {
            spriteRenderer.flipX = true;
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void spawnPakandeAttack()
    {
        GameObject magicAttack = Instantiate(attackPrefab, attackLoc.transform.position, Quaternion.identity);
    }
}
