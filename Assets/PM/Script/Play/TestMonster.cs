using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    private int monsterdamage = 1;
    public GameObject Player;
    public PlayerController PlayerController;

    public void Awake()
    {
        PlayerController=Player.GetComponent<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerController.playerhp -= 1;
            Debug.Log(PlayerController.playerhp);
        }
        GameManager.Data.StageCounts1=1;
    }
}
