using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 10f;
    public Transform partToRotate;
    public float turnSpeed = 10f;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            var currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (!target)
        {
            return;
        }

        var direction = target.position - transform.position;
        //Debug.Log("direction: " + direction);
        var lookRotation = Quaternion.LookRotation(direction);
        //Debug.Log("lookRotation: " + lookRotation);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        //var rotation = lookRotation.eulerAngles;
        Debug.Log(rotation.y);
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
