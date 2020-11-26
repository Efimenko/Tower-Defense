using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rendererComponent;
    private Color initialColor;
    private GameObject turret;

    private void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        initialColor = rendererComponent.material.color;
    }

    private void OnMouseEnter()
    {
        rendererComponent.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rendererComponent.material.color = initialColor;
    }

    private void OnMouseDown()
    {
        if (turret)
        {
            return;
        }

        var turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
