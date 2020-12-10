﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject turret;

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
            buildManager.TogglePopup(this);
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

        turret = Instantiate(turretToBuild.prefab, GetBuildPosition(), transform.rotation);

        PlayerStats.instance.SetMoney((currentMoney) => currentMoney - turretToBuild.cost);
    }
}
