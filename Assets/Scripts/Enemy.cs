using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int currentIndex = 0;

    public int health = 100;

    public int enemyValue = 50;

    public float speed = 10f;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(target.position, transform.position) <= 0.3f)
        {
            TrackNextWaypoint();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.instance.SetMoney((currentMoney) => currentMoney + enemyValue);
        Destroy(gameObject);
    }

    private void TrackNextWaypoint()
    {
        if (currentIndex == Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        
        target = Waypoints.points[++currentIndex];
    }

    void EndPath()
    {
        PlayerStats.instance.SubtractLives(1);
        Destroy(gameObject);
    }
}
