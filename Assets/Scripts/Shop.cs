using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

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

    public void PurchaseLaserBeamer()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
