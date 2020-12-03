using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Unity setup")]
    public Transform target;
    public Transform partToRotate;
    public Transform projectileSpawnPoint;
    private float turnSpeed = 10f;
    
    [Header("Bullet setup")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser setup")]
    public LineRenderer lineRenderer;
    public bool useLaser;
    public float damagePerSecond = 30f;

    [Header("Common Attributes")]
    public float range = 10f;

    public Enemy targetEnemy;


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
            targetEnemy = target.GetComponent<Enemy>();
        } else
        {
            target = null;
            targetEnemy = null;
        }
    }

    private void Update()
    {
        if (!target)
        {
            StopLaser();
            return;
        }

        RotateTurretToEnemy();

        if (useLaser)
        {
            Shoot("laser");
            return;
        }

        UpdateCountdownAndShoot();
    }

    private void UpdateCountdownAndShoot()
    {
        if (fireCountdown <= 0f)
        {
            Shoot("bullet");
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void RotateTurretToEnemy()
    {
        var direction = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot(string type)
    {
        switch (type)
        {
            case "bullet":
                var bulletGameObject = Instantiate(bulletPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                var bulletScript = bulletGameObject.GetComponent<Bullet>();
                bulletScript.SetTarget(target);
                break;
            case "laser":
                lineRenderer.SetPosition(0, projectileSpawnPoint.position);
                lineRenderer.SetPosition(1, target.position);
                lineRenderer.enabled = true;
                targetEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void StopLaser()
    {
        if (!useLaser)
        {
            return;
        }

        lineRenderer.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
