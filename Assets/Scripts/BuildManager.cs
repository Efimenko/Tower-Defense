using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject standartTurretPref;

    private GameObject turretToBuild;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Another Build Manager instance");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        turretToBuild = standartTurretPref;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
