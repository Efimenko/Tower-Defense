using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TurretBlueprint _turretToBuild;

    public GameObject popup;

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

    public void TogglePopup(Node node)
    {
        _turretToBuild = null;

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
        popup.SetActive(false);
    }
}
