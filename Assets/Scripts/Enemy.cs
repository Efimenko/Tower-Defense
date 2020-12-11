
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;

    public int worth = 50;

    public float baseSpeed = 10f;

    [HideInInspector]
    public float speed;

    private void Start()
    {
        speed = baseSpeed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
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
