using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms.Impl;

public class CoinsCounter : MonoBehaviour
{
    string coinKey = "Coins";
    public TMP_Text coinsAmount;
    public int coins;

    public int CurrentCoinAmount {  get; set; }

    private void Awake()
    {
        CurrentCoinAmount = PlayerPrefs.GetInt(coinKey);
    }
    private void Start()
    {
        coins = CurrentCoinAmount;
        coinsAmount.text = coins + " ";
    }

    private void SetCoin(int coins)
    {
        PlayerPrefs.SetInt(coinKey, coins);
    }
}
