using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TurretBlueprint _turretToBuild;

    private TurretBlueprint _turretToUpgrade;

    public GameObject popup;

    private Node popupOpenedOn;

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

    public void TogglePopup(Node node)
    {
        _turretToUpgrade = _turretToBuild;
        _turretToBuild = null;
        popupOpenedOn = node;

        if (popup.transform.position == node.GetBuildPosition())
        {
            popup.SetActive(!popup.activeSelf);
            return;
        }

        popup.transform.position = node.GetBuildPosition();
        popup.SetActive(true);
        
    }

    public void HidePopup()
    {
        popupOpenedOn = null;
        popup.SetActive(false);
    }

    public void UpgradeTurret()
    {
        popupOpenedOn.UpgradeTurret();
    }
}
