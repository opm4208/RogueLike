using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private float rollPower;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer flip;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
    }
    private void Roll()
    {
        if (flip.flipX == false)
        {
            Debug.Log("11");
            anim.SetTrigger("IsRoll");
            rb.AddForce(Vector2.right * rollPower, ForceMode2D.Impulse);
        }
        else
        {
            anim.SetTrigger("IsRoll");
            rb.AddForce(Vector2.left * rollPower, ForceMode2D.Impulse);
        }

    }
    private void OnRoll(InputValue value)
    {
        Roll();
    }
}
