using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject buildEffectPrefab;

    private TurretBlueprint _turretToBuild;

    private TurretBlueprint _turretToUpgrade;

    public TurretBlueprint turretToBuild => _turretToBuild;

    public TurretBlueprint turretToUpgrade => _turretToUpgrade;

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

    public void SelectTurretToBuild(TurretBlueprint selectedTurret)
    {
        _turretToBuild = selectedTurret;
    }

    public void SelectTurretToUpgrade()
    {
        _turretToUpgrade = _turretToBuild ?? _turretToUpgrade;
        _turretToBuild = null;
    }
}
