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
    public Transform warningLoc;
    public Transform ultimateLoc;

    [Header("Enemy Settings")]
    public Transform[] enemyPos;
    public DetectionZone attackZone;
    public GameObject attackPrefab;
    public GameObject[] thunderFX;
    public GameObject[] warningSign;

    [SerializeField] private int amountOfAttack = 3;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeAttackInterval = 1f;
    [SerializeField] private bool isFinishUltimate = false;

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

    public bool _isCharging = false;

    public bool IsCharging
    {
        get
        {
            return _isCharging;
        }
        set
        {
            _isCharging = value;
            animator.SetBool("isCharging", value);
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
        SoundManager.Instance.Play("MagicCircle");

        Destroy(magicAttack, 2f);
    }

    public void SpawnPakandeEnragedAttack()
    {
        StartCoroutine(SpawnEnragedAttack());
    }

    public void OnHealthConditionChanged()
    {
        if (damage.Health >= 101 && damage.Health <= 150)
        {
            MoveToPosition(enemyPos[4]);
        }

        if (damage.Health >= 76 && damage.Health <= 100)
        {
            MoveToPosition(enemyPos[0]);
        }

        else if (damage.Health >= 51 && damage.Health <= 75 && !isFinishUltimate)
        {
            IsCharging = true;
            IsEnraged = true;
            MoveToPosition(enemyPos[2]);
        }

        else if (damage.Health >= 51 && damage.Health <= 75 && isFinishUltimate)
        {
            IsCharging = false;
            IsEnraged = true;
            MoveToPosition(enemyPos[3]);
        }

        else if (damage.Health >= 0 && damage.Health <= 50)
        {
            IsEnraged = true;
            MoveToPosition(enemyPos[1]);
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

    public void UltimateAttack()
    {
        StartCoroutine(ChargingAttack());
    }

    public void StopUltimateAttack()
    {
        IsCharging = false;
        isFinishUltimate = true;
    }

    public void SpawnWarningSign()
    {
        GameObject warning = Instantiate(warningSign[0], warningLoc.transform.position, Quaternion.identity);

        Destroy(warning, 6f);
    }

    public void SpawnWarningSign2()
    {
        GameObject warning = Instantiate(warningSign[1], warningLoc.transform.position, Quaternion.identity);

        Destroy(warning, 4f);
    }

    public void SpawnUltimateAttack()
    {
        GameObject ultimateAttack = Instantiate(thunderFX[0], ultimateLoc.transform.position, Quaternion.identity);
        SoundManager.Instance.Play("Thunder");

        Destroy(ultimateAttack, 4f);
    }

    public void SpawnUltimateAttack2()
    {
        GameObject ultimateAttack = Instantiate(thunderFX[1], ultimateLoc.transform.position, Quaternion.identity);
        SoundManager.Instance.Play("Thunder");

        Destroy(ultimateAttack, 4f);
    }

    public void ChargeAttackSound()
    {
        SoundManager.Instance.Play("ChargeAttack");
    }

    IEnumerator SpawnEnragedAttack()
    {
        for (int i = 0; i < amountOfAttack; i++)
        {
            GameObject magicAttack = Instantiate(attackPrefab, attackLoc.transform.position, Quaternion.identity);
            SoundManager.Instance.Play("MagicCircle");

            Destroy(magicAttack, 3f);
            yield return new WaitForSeconds(timeAttackInterval);
        }
    }

    IEnumerator ChargingAttack()
    {
        yield return new WaitForSeconds(4f);

        animator.SetTrigger("ultimateAttack");
    }
}
