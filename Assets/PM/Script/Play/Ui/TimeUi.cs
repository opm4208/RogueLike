using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUi : MonoBehaviour
{
    private TMP_Text TMP;

    private void Awake()
    {
        TMP = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        TMP.text = $"{((int)Time.time/60)}:{(int)Time.time%60}";
    }
}
