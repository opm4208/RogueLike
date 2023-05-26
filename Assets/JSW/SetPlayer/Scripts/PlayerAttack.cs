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
    private bool inTime = true;

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
        /*if (a)
        {
            a = false;*/
            if (curTime <= 0.5f)
            {
                if (attackCombo == 3)
                {
                    Debug.Log("Attack_A");

                    Collider2D[] coll2Da = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in coll2Da)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<Enemy>().GetDamange(5f);
                        }
                    };
                    anim.SetTrigger("IsAttackA");
                    curTime = coolTime;
                    attackCombo -= 1;
                    // 코우틴 실행중이면 코루틴 정지
                    StartCoroutine(WaitForIt());

                    Debug.Log("Attack_Combo1");
                }
                else if (attackCombo == 2 && curCombo >= 0)
                {
                    Debug.Log("Attack_B");

                    Collider2D[] coll2Db = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in coll2Db)
                    {
                        if (collider.tag == "Enemy")
                        {
                            Debug.Log("Attack damege");
                            collider.GetComponent<Enemy>().GetDamange(5f);
                        }
                    }
                    Debug.Log("Anime2");
                    anim.SetTrigger("IsAttackB");
                    curTime = coolTime;
                    attackCombo -= 1;

                    Debug.Log("Attack_Combo2");
                }
                else if (attackCombo == 1 && curCombo >= 0)
                {
                    Debug.Log("Attack_C");

                    Collider2D[] coll2Dc = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in coll2Dc)
                    {
                        if (collider.tag == "Enemy")
                        {
                            Debug.Log("Attack damege");
                            collider.GetComponent<Enemy>().GetDamange(5f);
                        }
                    }
                    Debug.Log("Anime3");
                    anim.SetTrigger("IsAttackC");
                    curTime = coolTime;
                    attackCombo = 3;

                    Debug.Log("Attack_Combo3");

                }
                curCombo = 1;
            }
        //}
        
    }

    IEnumerator WaitForIt()
    {
        inTime = false;
        yield return new WaitForSeconds(1.5f);
        attackCombo = 3;
        inTime = true;
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
    /*bool a=true;
    private void aeae()
    {
        a = true;
    }*/
}
