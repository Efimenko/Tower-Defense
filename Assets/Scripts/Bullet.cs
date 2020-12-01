using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public GameObject impactEffectPrefab;

    public float explosionRange = 0f;

    public float speed = 70f;

    public string enemyTag = "Enemy";

    public int damage = 50;

    public void SetTarget (Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        var direction = target.position - transform.position;
        var distanceInThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceInThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceInThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        var impactEffectGameObject = Instantiate(impactEffectPrefab, transform.position, transform.rotation);
        Destroy(impactEffectGameObject, 2f);

        if (explosionRange > 0f)
        {
            Explode();
        } else
        {
            Damage(target);
        }
    }

    private void Explode()
    {
        var itemsInRadius = Physics.OverlapSphere(transform.position, explosionRange);

        foreach (var item in itemsInRadius)
        {
            if (item.CompareTag(enemyTag))
            {
                Damage(item.transform);
            }
        }
    }

    private void Damage(Transform _target)
    {
        Destroy(gameObject);
        target.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
