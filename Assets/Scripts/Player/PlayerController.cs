using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirection), typeof(Damage))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    TouchDirection touchDirection;
    Damage damage;
    UIManager uiManager;
    PlayerInput input;
    TutorialScreen tutorialScreen;
    ArtifactCount artifactCount;

    [Header("Player Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float onAirSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Artifact Settings")]
    public int prabuArtifactAmount = 0;
    public bool prabuArtifact = false;

    Vector2 moveInput;

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    [Header("Condition Settings")]
    [SerializeField] private bool _isMoving = false;
    public bool IsMoving 
    { 
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    [SerializeField] private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    [SerializeField] private bool _isCrouch = false;
    public bool IsCrouch
    {
        get
        {
            return _isCrouch;
        }
        set
        {
            _isCrouch = value;
            animator.SetBool("isCrouch", value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if(_isFacingRight != value)
            {
                // Flip sprite player ke arah berlawanan
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchDirection.IsOnWall)
                {
                    // kondisi saat tidak menyentuh tembok
                    if (touchDirection.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // menjadi nilai onAirSpeed saat di tidak menyentuh tanah
                        return onAirSpeed;
                    }
                }
                else
                {
                    // kondisi idle, speed menjadi 0
                    return 0;
                }
            }
            else
            {
                //kondisi movement berhenti
                return 0;
            }
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool("isAlive");
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDirection = GetComponent<TouchDirection>();
        damage = GetComponent<Damage>();
        uiManager = FindObjectOfType<UIManager>();
        input = GetComponent<PlayerInput>();
        tutorialScreen = FindObjectOfType<TutorialScreen>();
        artifactCount = FindObjectOfType<ArtifactCount>();
    }

    private void Start()
    {
        tutorialScreen.isTutorial = true;
    }

    private void FixedUpdate()
    {
        if(!damage.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Update()
    {
        if (uiManager.isPaused || tutorialScreen.isTutorial)
        {
            input.DeactivateInput();
        }
        else if (!uiManager.isPaused || !tutorialScreen.isTutorial)
        {
            input.ActivateInput();
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Sprite menghadap ke kanan
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Sprite menghadap ke kiri
            IsFacingRight = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsCrouch = true;
        }
        else if (context.canceled)
        {
            IsCrouch = false;
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if(context.started && touchDirection.IsGrounded && !IsCrouch)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchDirection.IsGrounded && !IsCrouch)
        {
            animator.SetTrigger("rangedAttack");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnSlow(float slowMovementSpeed)
    {
        walkSpeed = walkSpeed * slowMovementSpeed;
        runSpeed = runSpeed * slowMovementSpeed;
        onAirSpeed = onAirSpeed * slowMovementSpeed;
    }

    public void OnRemoveSlow()
    {
        walkSpeed = walkSpeed * 2;
        runSpeed = runSpeed * 2;
        onAirSpeed = onAirSpeed * 2;
    }


    public void WalkSound()
    {
        SoundManager.Instance.Play("Walk");
    }
    public void RunSound()
    {
        SoundManager.Instance.Play("Run");
    }

    public void JumpSound()
    {
        SoundManager.Instance.Play("Jump");
    }

    public void BowAttackSound()
    {
        SoundManager.Instance.Play("BowAttack");
    }

    public void SwordAttackSound()
    {
        SoundManager.Instance.Play("Slash");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Artifact"))
        {
            prabuArtifactAmount++;
            artifactCount.ShowAmountArtifact();
            if (prabuArtifactAmount >= 3)
            {
                prabuArtifact = true;
            }
        }
    }
}
