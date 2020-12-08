using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int money;
    public int initialMoney = 400;

    public int lives;
    public int initialLives = 10;

    public int wave;

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
        wave = 0;

        UpdateMoneyUI();
        UpdateLivesUI();
    }

    public int SetMoney(Func<int, int>updater)
    {
        money = updater(money);
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
        UpdateLivesUI();

        return lives;
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives:" + lives;
    }
}
