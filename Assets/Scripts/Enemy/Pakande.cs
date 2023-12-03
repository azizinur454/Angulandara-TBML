using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pakande : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Damage damage;

    [Header("Target Location")]
    public Transform player;
    public Transform attackLoc;

    [Header("Enemy Settings")]
    public Transform[] enemyPos;
    public DetectionZone attackZone;
    public GameObject attackPrefab;
    [SerializeField] private int amountOfAttack = 3;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeAttackInterval = 1f;

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

    public bool _isEnraged = false;

    public bool IsEnraged
    {
        get 
        { 
            return _isEnraged; 
        }
        set
        {
            _isEnraged = value;
            animator.SetBool("isEnraged", value);
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

        OnHealthConditionChanged();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void SpawnPakandeAttack()
    {
        GameObject magicAttack = Instantiate(attackPrefab, attackLoc.transform.position, Quaternion.identity);
    }

    public void SpawnPakandeEnragedAttack()
    {
        StartCoroutine(SpawnEnragedAttack());
    }

    public void OnHealthConditionChanged()
    {
        if (damage.Health >= 50 && damage.Health <= 75)
        {
            MoveToPosition(enemyPos[0]);
        }

        else if(damage.Health >= 25 && damage.Health <= 50 )
        {
            IsEnraged = true;
            MoveToPosition(enemyPos[1]);
        }

        else if (damage.Health >= 0 && damage.Health <= 25 )
        {
            MoveToPosition(enemyPos[2]);
        }
    }

    public void MoveToPosition(Transform targetPosition)
    {
        if (targetPosition != null)
        {
            Vector2 targetPositionVector = targetPosition.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPositionVector, moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Target position is not decided");
        }
    }

    IEnumerator SpawnEnragedAttack()
    {
        for (int i = 0; i < amountOfAttack; i++)
        {
            GameObject magicAttack = Instantiate(attackPrefab, attackLoc.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timeAttackInterval);
        }
    }
}
