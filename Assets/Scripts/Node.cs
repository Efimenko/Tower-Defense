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
    public GameObject turret;
    private BuildManager buildManager;

    private bool canBuild
    {
        get
        {
            return buildManager.canBuild && !EventSystem.current.IsPointerOverGameObject() && !turret;
        }
    }

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
        if (!canBuild)
        {
            return;
        }

        buildManager.BuildTurret(this);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
