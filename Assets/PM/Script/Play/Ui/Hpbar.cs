using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public GameObject Player;
    private PlayerController controller;
    public Slider hp_slider;

    private void Awake()
    {
        controller = Player.GetComponent<PlayerController>();
        hp_slider = GetComponent<Slider>();
        hp_slider.minValue = 0;
        hp_slider.maxValue = controller.playermaxhp;
    }

    private void Update()
    {
        hp_slider.value = controller.playerhp;
    }
}
