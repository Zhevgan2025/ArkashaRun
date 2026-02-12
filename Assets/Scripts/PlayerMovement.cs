using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float JumpForce = 10f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;
    private float baseRunSpeed;
    private Coroutine speedRoutine;




    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string slideBoolName = "IsSliding";


    [Header("Slide Collider")]
    [SerializeField] private BoxCollider2D bodyCollider;

    [SerializeField] private Vector2 slideSize = new Vector2(1f, 0.6f);
    [SerializeField] private Vector2 slideOffset = new Vector2(0f, -0.2f);

    private Vector2 standSize;
    private Vector2 standOffset;



    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!animator) animator = GetComponentInChildren<Animator>();


        if (!bodyCollider) bodyCollider = GetComponent<BoxCollider2D>();
        if (bodyCollider)
        {
            standSize = bodyCollider.size;
            standOffset = bodyCollider.offset;
        }
        baseRunSpeed = runSpeed;

    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = Vector2.up * JumpForce;
        }

        if (isJumping && Input.GetButtonDown("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * JumpForce;

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }



        if (animator)
        {
            bool isSliding = Input.GetKey(KeyCode.S);

            if (animator)
            {
                animator.SetBool(slideBoolName, isSliding);
            }

            if (bodyCollider)
            {
                if (isSliding)
                {
                    bodyCollider.size = slideSize;
                    bodyCollider.offset = slideOffset;
                }
                else
                {
                    bodyCollider.size = standSize;
                    bodyCollider.offset = standOffset;
                }
            }

        }




    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Mathf.Max(rb.velocity.x, runSpeed), rb.velocity.y);


    }

    public void AddSpeed(float amount, float duration)
    {
        if (speedRoutine != null) StopCoroutine(speedRoutine);
        speedRoutine = StartCoroutine(SpeedBoost(amount, duration));
    }

    private IEnumerator SpeedBoost(float amount, float duration)
    {
        runSpeed = baseRunSpeed + amount;

        yield return new WaitForSeconds(duration);

        runSpeed = baseRunSpeed;
        speedRoutine = null;
    }
}

