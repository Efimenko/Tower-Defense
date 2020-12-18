
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float baseHealth = 100;
    private float health;

    public int worth = 50;

    public float baseSpeed = 10f;

    public RectTransform healthBar;

    [HideInInspector]
    public float speed;

    private void Start()
    {
        speed = baseSpeed;
        health = baseHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        UpdateNextHealthBarSize();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateNextHealthBarSize()
    {
        var startWidth = 30f;
        var nextWidth = startWidth * health / baseHealth;
        healthBar.sizeDelta = new Vector2(nextWidth, healthBar.rect.height);
    }

    void Die()
    {
        PlayerStats.instance.SetMoney((currentMoney) => currentMoney + worth);
        Destroy(gameObject);
    }

    public void SlowDown(float slownessPercent)
    {
        speed = baseSpeed - baseSpeed * slownessPercent / 100;
    }
}
