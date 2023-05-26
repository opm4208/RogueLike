using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public GameObject Player;
    private PlayerStatus controller;
    public Slider hp_slider;

    private void Awake()
    {
        controller = Player.GetComponent<PlayerStatus>();
        hp_slider = GetComponent<Slider>();
        hp_slider.minValue = 0;
        hp_slider.maxValue = controller.Hp;
    }

    private void Update()
    {
        hp_slider.value = controller.MaxHp;
    }
}
