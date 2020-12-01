using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int money;
    public int initialMoney = 400;

    public int lives;
    public int initialLives = 10;

    public Text livesText;
    public Text moneyText;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        money = initialMoney;
        lives = initialLives;

        UpdateMoneyUI();
        UpdateLivesUI();
    }

    public int SubtractMoney(int subtractionMoney)
    {
        money -= subtractionMoney;
        UpdateMoneyUI();

        return money;
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "Money: $" + money;
    }

    public int SubtractLives(int subtractionLives)
    {
        lives -= subtractionLives;
        livesText.text = "Lives: " + lives;

        return lives;
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives:" + lives;
    }
}
