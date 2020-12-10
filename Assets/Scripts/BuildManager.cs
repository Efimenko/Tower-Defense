using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TurretBlueprint turretToBuild;

    public GameObject popup;

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
        if (PlayerStats.instance.money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        node.turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);

        PlayerStats.instance.SetMoney((currentMoney) => currentMoney - turretToBuild.cost);

        Debug.Log("Remain money: " + PlayerStats.instance.money);
    }

    public void TogglePopup(Node node)
    {
        turretToBuild = null;

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
