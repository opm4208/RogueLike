using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    private BoxCollider2D door;
    private void Awake()
    {
        door = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1");
        if (GameManager.Data.StageCounts1 <= 0)
        {
            Debug.Log("ef");
            door.gameObject.SetActive(false);
        }
        
    }
}
