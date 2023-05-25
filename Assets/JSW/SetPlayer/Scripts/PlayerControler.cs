using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] LayerMask groundChack;

    private Rigidbody2D rb;
    private Vector2 inputDir;
    private Animator anim;
    private new SpriteRenderer renderer;

    public bool isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        GroundChack();
    }

    private void Move()
    {
        if (inputDir.x < 0 && rb.velocity.x > -maxSpeed)
            rb.transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        else if (inputDir.x > 0 && rb.velocity.x < maxSpeed)
            rb.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        anim.SetFloat("IsMove", Mathf.Abs(inputDir.x));

        if (inputDir.x > 0)
            renderer.flipX = false;
        else if (inputDir.x < 0)
            renderer.flipX = true;
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    private void OnJump(InputValue value)
    {
        if(value.isPressed && isGround)
            Jump();
    }
    private void GroundChack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        if (hit.collider != false)
        {
            isGround = true;
            anim.SetBool("IsGround", true);
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
        }
        else
        {
            isGround = false;
            anim.SetBool("IsGround", false);
            Debug.DrawRay(transform.position, Vector3.down * 1f, Color.green);
        }
    }
}
