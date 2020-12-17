using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject buildEffectPrefab;

    public GameObject sellEffectPrefab;

    private TurretBlueprint _turretToBuild;

    public TurretBlueprint turretToBuild => _turretToBuild;

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
}
