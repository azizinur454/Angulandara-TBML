using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    TouchDirection touchDirection;
    Animator animator;
    Damage damage;

    public float walkSpeed = 3f;
    [SerializeField] private float rangeDistance = 3f;
    public DetectionZone holeDetectionZone;
    public Transform player;
    private Vector2 walkDirectionVector = Vector2.right;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;

    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool IsChase = false;

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        touchDirection = GetComponent<TouchDirection>();
        damage = GetComponent<Damage>();
    }

    private void FixedUpdate()
    {
        if (touchDirection.IsGrounded && touchDirection.IsOnWall)
        {
            FlipDirection();
        }

        if (!damage.LockVelocity && !IsChase)
        {
            EnemyWalk();
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= rangeDistance)
        {
            IsChase = true;
            Vector2 direction = player.position - transform.position;

            Vector2 move = direction.normalized * walkSpeed * Time.deltaTime;

            transform.Translate(new Vector2(move.x, 0));

            if (direction.x > 0)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (direction.x < 0)
            {
                WalkDirection = WalkableDirection.Left;
            }
        }
        else
        {
            IsChase = false;
        }
    }

    public void EnemyWalk()
    {
        if (CanMove && touchDirection.IsGrounded)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Walk Direction is not set");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnHoleDetected()
    {
        if (touchDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}
