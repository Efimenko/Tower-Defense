using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TurretBlueprint turretToBuild;

    public bool canBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }

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
        turretToBuild = selectedTurret;
    }

    public void BuildTurret(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        node.turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);

        PlayerStats.money -= turretToBuild.cost;

        Debug.Log("Remain money: " + PlayerStats.money);
    }
}
