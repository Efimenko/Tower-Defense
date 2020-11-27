using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandartTurret()
    {
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void PurchaseMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
