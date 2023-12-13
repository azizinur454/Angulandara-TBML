using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roro : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Damage damage;

    [Header("Target Location")]
    public Transform player;
    public Transform beamAttackLoc;
    public Transform splashAttackLoc;

    [Header("Enemy Settings")]
    public Transform[] enemyPos;
    public DetectionZone attackZone;
    public GameObject splashAttackPrefab;
    public GameObject beamAttackPrefab;

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

    public float AttackCooldown2
    {
        get
        {
            return animator.GetFloat("attackCooldown2");
        }
        set
        {
            animator.SetFloat("attackCooldown2", Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damage = GetComponent<Damage>();
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

        if (AttackCooldown2 > 0)
        {
            AttackCooldown2 -= Time.deltaTime;
        }
    }

    public void SpawnRoroBeamAttack()
    {
        GameObject beamAttack = Instantiate(beamAttackPrefab, beamAttackLoc.transform.position, Quaternion.identity);

        Destroy(beamAttack, 0.6f );
    }

    public void SpawnRoroSplashAttack()
    {
        GameObject splashAttack = Instantiate(splashAttackPrefab, splashAttackLoc.transform.position,Quaternion.identity);

        Destroy(splashAttack, 1f);
    }
}
