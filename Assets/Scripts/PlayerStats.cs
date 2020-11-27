using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int initialMoney = 400;
    public Text moneyText;

    private void Start()
    {
        money = initialMoney;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }
}
