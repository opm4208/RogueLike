using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movePower;
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxSpeed;
    public int playerhp=20;
    public int playermaxhp=20;

    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Vector2 inputDir;
    public bool isGround;
    //private Collider2D col;


    private void Awake()
    {
        //col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    public void Move()
    {
        if(inputDir.x<0&&rb.velocity.x>-maxSpeed)
            rb.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        else if(inputDir.x>0&&rb.velocity.x<maxSpeed)
            rb.AddForce(Vector2.right *inputDir.x*movePower, ForceMode2D.Force);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        if (inputDir.x > 0) 
        {
            rbSprite.flipX = false;
        }
            
        else if (inputDir.x < 0)
            rbSprite.flipX = true;
    }

    private void OnJump(InputValue value)
    {
        if(isGround)
            Jump();
    }

    private void GroundCheck()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, new Vector3(hit.point.x, hit.point.y, 0) - transform.position, Color.red);
            isGround = true;
        }
        else
        {
            isGround=false;
            Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.green);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }*/
}
