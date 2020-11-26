using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rendererComponent;
    private Color initialColor;
    private GameObject turret;
    private BuildManager buildManager;

    private void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        initialColor = rendererComponent.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !buildManager.GetTurretToBuild())
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

        var turretToBuild = buildManager.GetTurretToBuild();

        if (turret || !turretToBuild)
        {
            return;
        }

        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
