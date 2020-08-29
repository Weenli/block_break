using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class FortuneWheel : MonoBehaviour
{
    private int NumberOfTurns; //количество оборотов
    public bool CanRotate = false; //можем ли мы вращать колесо
    private float speed;
    ScoreManager scoreManager;
    public Button CancelButton;
    public bool CanPress = false;
    private int WhatWeWin; //определяем приз
    void Start()
    {
        if (Advertisement.isSupported)
        {
            //Advertisement.Initialize("3790013", false);
        }
        CanPress = true;
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }
    void Update()
    {
        if (CanRotate)
        {
            CancelButton.interactable = false;
            // вращение
            StartCoroutine(WheelRotating());
        }
    }
 
    private IEnumerator WheelRotating()
    {
        CanRotate = false;
        CanPress = false;
        NumberOfTurns = UnityEngine.Random.Range(250, 300);
        speed = 0.008f;
        for (int i = 0; i < NumberOfTurns; i++)
        {
            transform.Rotate(0, 0, 15f);
            if (i > Mathf.RoundToInt(NumberOfTurns * .5f))
            {
                speed = .01f;
            }
            if (i > Mathf.RoundToInt(NumberOfTurns * .6f))
            {
                speed = .016f;
            }
            if (i > Mathf.RoundToInt(NumberOfTurns * 0.7f))
            {
                speed = .04f;
            }
            if (i > Mathf.RoundToInt(NumberOfTurns * 0.8f))
            {
                speed = .06f;
            }
            if (i > Mathf.RoundToInt(NumberOfTurns * 0.9f))
            {
                speed = .1f;
            }
            yield return new WaitForSeconds(speed);
        }
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 30 != 0)
        {
            transform.Rotate(0, 0, 15f);
        }
        WhatWeWin = Mathf.RoundToInt(transform.eulerAngles.z);
        switch (WhatWeWin)
        {
            case 0:
                scoreManager.SetPurchaseCoins(3000);
                break; 

            case 30:
                scoreManager.SetPurchaseCoins(1500);
                break;

            case 60:
                scoreManager.SetPurchaseCoins(800);
                break;

            case 90:
                scoreManager.SetPurchaseCoins(800);
                break;

            case 120:
                scoreManager.SetPurchaseCoins(800);
                break;

            case 150:
                scoreManager.SetPurchaseCoins(1500);
                break;

            case 180:
                scoreManager.SetPurchaseCoins(3000);
                break;

            case 210:
                scoreManager.SetPurchaseCoins(1500);
                break;

            case 240:
                scoreManager.SetPurchaseCoins(800);
                break;

            case 270:
                scoreManager.SetPurchaseCoins(800);
                break;

            case 300:
                scoreManager.SetPurchaseCoins(800);
                break;
            case 330:
                scoreManager.SetPurchaseCoins(1500);
                break;
        }
        CancelButton.interactable = true;
        CanPress = true;
    }
}
