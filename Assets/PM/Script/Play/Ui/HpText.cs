using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class HpText : MonoBehaviour
{
    private TMP_Text tmp;
    public GameObject Player;
    private PlayerStatus controller;

    private void Awake()
    {
        controller = Player.GetComponent<PlayerStatus>();
        tmp = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        tmp.text = ($"{controller.Hp}/{controller.MaxHp}");
    }
}
