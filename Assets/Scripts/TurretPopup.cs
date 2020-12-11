﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPopup : MonoBehaviour
{
    public static TurretPopup instance;
    public Button upgradeButton;
    public Text upgradeCost;
    public GameObject canvas;
    private Node openedOn;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Another Turret Popup instance");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Toggle(Node node)
    {
        openedOn = node;
        upgradeCost.text = BuildManager.instance.turretToUpgrade.upgradeCost + "$";

        if (transform.position == node.GetBuildPosition())
        {
            canvas.SetActive(!canvas.activeSelf);
            return;
        }

        transform.position = node.GetBuildPosition();
        canvas.SetActive(true);
    }

    public void Hide()
    {
        openedOn = null;
        canvas.SetActive(false);
    }

    public void OnUpgrade()
    {
        openedOn.UpgradeTurret();

        if (openedOn.isUpgraded)
        {
            upgradeCost.text = "Max";
            upgradeButton.interactable = false;
        }
    }
}
