using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Unity setup")]
    public Transform target;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    [Header("Attributes")]
    public float range = 10f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    private float fireCountdown = 0f;

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
        if (fireCountdown > 0f) {
            fireCountdown -= Time.deltaTime;
        }

        if (!target)
        {
            return;
        }

        RotateTurretToEnemy();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    private void RotateTurretToEnemy()
    {
        var direction = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        var bulletGameObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        var bulletScript = bulletGameObject.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
