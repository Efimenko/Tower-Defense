using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int currentIndex = 0;
    private Enemy enemy;

    private void Start()
    {
        target = Waypoints.points[0];
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime);

        if (Vector3.Distance(target.position, transform.position) <= 0.3f)
        {
            TrackNextWaypoint();
        }

        enemy.speed = enemy.baseSpeed;
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
