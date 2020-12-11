using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPopup : MonoBehaviour
{
    public Text upgradeCost;

    private void OnEnable()
    {
        upgradeCost.text = BuildManager.instance.turretToUpgrade.upgradeCost + "$";
    }
}
