using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    Animator animator;
    StageClear stageClear;
    DropItem dropItem;

    public UnityEvent<int, Vector2> hitDamage;
    public UnityEvent<int, int> healthChanged;

    [Header("Enemy Score")]
    [SerializeField] private int enemyScore = 500;

    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            // kondisi untuk membuat nilai darah tidak kurang dari 0
            if (value < 0)
            {
                _health = 0; 
            }
            else
            {
                _health = value;
            }

            healthChanged?.Invoke(_health, MaxHealth);

            // kondisi ketika darah kurang dari sama dengan 0 = player mati
            if(_health <= 0)
            {
                IsAlive = false;

                if (CompareTag("Enemy"))
                {
                    CharacterEvent.scores(gameObject, enemyScore);
                    Score.instance.AddKillingEnemyScore(enemyScore);
                }

                if(CompareTag("Boss"))
                {
                    stageClear.isFinish = true;
                    SoundManager.Instance.Stop("BossBGM");
                    SoundManager.Instance.Play("Complete");
                }   

                if(CompareTag("Objects"))
                {
                    dropItem.dropArtifact();
                }
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }

    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0;
    [SerializeField] private float invicibilityTime = 0.25f;

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stageClear = FindObjectOfType<StageClear>();
        dropItem = FindObjectOfType<DropItem>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invicibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger("hit");
            LockVelocity = true;
            hitDamage?.Invoke(damage, knockback);
            if(Health > 0)
            {
                CharacterEvent.characterDamaged.Invoke(gameObject, damage);
            }

            return true;
        }

        return false;
    }    

    public void Heal(int healthrestored)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthrestored);
            Health += actualHeal;
            CharacterEvent.characterHealed(gameObject, "Health Restored");
        }
    }
    public void BowAttackSound()
    {
        SoundManager.Instance.Play("BowAttack");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Respawn()
    {
        Health = 100;
        IsAlive = true;
    }
}
