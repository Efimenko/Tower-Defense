using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject turret;
    public bool isUpgraded = false;
    public TurretBlueprint turretBlueprint;

    private Renderer rendererComponent;
    private Color initialColor;
    private BuildManager buildManager;

    private bool canBuild => buildManager.turretToBuild != null && !turret;

    private void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        initialColor = rendererComponent.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseEnter()
    {
        if (!canBuild)
        {
            return;
        }

        rendererComponent.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rendererComponent.material.color = initialColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret)
        {
            TurretPopup.instance.Toggle(this);
        }

        if (!canBuild)
        {
            return;
        }

       BuildTurret();
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void BuildTurret()
    {
        var turretToBuild = BuildManager.instance.turretToBuild;

        if (PlayerStats.instance.money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        turret = Instantiate(turretToBuild.prefab, GetBuildPosition(), Quaternion.identity);
        var buildEffect = Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        turretBlueprint = turretToBuild;

        Destroy(buildEffect, 5f);

        PlayerStats.instance.SetMoney((currentMoney) => currentMoney - turretToBuild.cost);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.instance.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money");
            return;
        }

        isUpgraded = true;

        Destroy(turret);

        turret = Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), transform.rotation);

        var upgradeEffect = Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);

        Destroy(upgradeEffect, 5f);

        PlayerStats.instance.SetMoney((currentMoney) => currentMoney - turretBlueprint.upgradeCost);
    }
}
