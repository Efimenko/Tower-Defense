using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int currentIndex = 0;

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
