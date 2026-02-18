using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUIController : MonoBehaviour
{
    private TextMeshProUGUI txt;
    private float countDown = 400f;
    private float accumulator = 0f;

    private const float NES_TICK = 24f / 60f;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (countDown <= 0) {
            MainClass.Player.MinusLive();
            //TODO Mario Death
            return; 
        }

        accumulator += Time.deltaTime;

        if (accumulator >= NES_TICK)
        {
            countDown--;
            accumulator -= NES_TICK;
        }

        txt.text = ((int) countDown).ToString("000");
    }
}
