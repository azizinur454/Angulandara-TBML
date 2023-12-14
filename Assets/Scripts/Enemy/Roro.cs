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
    public Transform[] splashAttackLoc;

    [Header("Enemy Settings")]
    public Transform[] enemyPos;
    public DetectionZone attackZone;
    public GameObject splashAttackPrefab;
    public GameObject beamAttackPrefab;
    public GameObject roroAura;
    public GameObject auraDamage;
    public bool hasBeenStunned = false;

    public bool _isStunned = false;
    public bool IsStunned
    {
        get
        {
            return _isStunned;
        }
        set
        {
            _isStunned = value;
            animator.SetBool("isStunned", value);
        }
    }

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

    public float StunDuration
    {
        get
        {
            return animator.GetFloat("stunDuration");
        }
        set
        {
            animator.SetFloat("stunDuration", Mathf.Max(value, 0));
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

        if (StunDuration > 0)
        {
            StunDuration -= Time.deltaTime;
        }

        EnemyStunned();
        RemoveBarrier();
        RevertStunState();
    }

    public void RevertStunState()
    {
        if(damage.Health == damage.MaxHealth)
        {
            hasBeenStunned = false;
            IsStunned = false;
            StunDuration = 0;
        }
    }

    public void EnemyStunned()
    {
        if (damage.Health <= 300 && !IsStunned && !hasBeenStunned)
        {
            IsStunned = true;
            hasBeenStunned = true;
        }

        else
        {
            if(StunDuration <= 0)
            {
                IsStunned = false;
            }
        }
    }

    public void RemoveBarrier()
    {
        if (IsStunned)
        {
            roroAura.SetActive(false);
            auraDamage.SetActive(false);
        }

        if(!IsStunned)
        {
            roroAura.SetActive(true);
            auraDamage.SetActive(true);
        }
    }

    public void SpawnRoroBeamAttack()
    {
        GameObject beamAttack = Instantiate(beamAttackPrefab, beamAttackLoc.transform.position, Quaternion.identity);

        Destroy(beamAttack, 0.6f );
    }

    public void SpawnRoroSplashAttack()
    {
        int idRoroAttackLoc = Random.Range(1, 4);

        if(idRoroAttackLoc == 1)
        {
            GameObject splashAttack = Instantiate(splashAttackPrefab, splashAttackLoc[0].transform.position, Quaternion.identity);

            Destroy(splashAttack, 1f);

            idRoroAttackLoc++;
        }

        else if (idRoroAttackLoc == 2)
        {
            GameObject splashAttack = Instantiate(splashAttackPrefab, splashAttackLoc[1].transform.position, Quaternion.identity);

            Destroy(splashAttack, 1f);

            idRoroAttackLoc++;
        }
        else if(idRoroAttackLoc == 3)
        {
            GameObject splashAttack = Instantiate(splashAttackPrefab, splashAttackLoc[2].transform.position, Quaternion.identity);

            Destroy(splashAttack, 1f);

            idRoroAttackLoc = 1;
        }
    }

    public void SpearHitSandSound()
    {
        SoundManager.Instance.Play("SpearHitSand");
    }
    public void SpearSwingSound()
    {
        SoundManager.Instance.Play("SpearSwing");
    }
}
