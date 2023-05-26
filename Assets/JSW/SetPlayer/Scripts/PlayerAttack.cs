using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public Transform pos;
    public Vector2 boxSize;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        curTime -= Time.deltaTime;
        curCombo -= Time.deltaTime;
    }
    private float curTime;
    public float coolTime = 0.5f;
    public int attackCombo = 3;
    private float curCombo = 1;
    private void Attack()
    {
        if (curTime <= 0.5f)
        {
            if (attackCombo == 3)
            {
                Debug.Log("Attack_A");
                // АјАн
                Collider2D[] coll2Da = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in coll2Da)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<Enemy>();
                    }
                }
                anim.SetTrigger("IsAttackA");
                curTime = coolTime;
                attackCombo -= 1;
                Debug.Log("Attack_Combo1");
            }
            else if (attackCombo == 2 && curCombo >= 0) 
            {
                Collider2D[] coll2Db = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in coll2Db)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<Enemy>();
                    }
                }
                anim.SetTrigger("IsAttackA");
                curTime = coolTime;
                attackCombo -= 1;
            }
            else if (attackCombo == 1 && curCombo >= 0)
            {
                Collider2D[] coll2Dc = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in coll2Dc)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<Enemy>();
                    }
                }
                anim.SetTrigger("IsAttackA");
                curTime = coolTime;
            }
            curCombo = 1;
        }
        else
        {
            Debug.Log("-Time");
            curCombo = 1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void OnAttack(InputValue value)
    {
        Attack();
    }
    
}
