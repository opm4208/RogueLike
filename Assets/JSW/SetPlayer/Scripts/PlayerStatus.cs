using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int maxHp = 50;
    private int hp;

    private void Awake()
    {
        hp = maxHp;
    }

    public int Hp { get { return hp; } set { hp -= value; } }
    public int MaxHp { get { return maxHp; } }
}
