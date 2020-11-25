﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

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
    }

    private void HitTarget()
    {
        Destroy(gameObject);
    }
}
