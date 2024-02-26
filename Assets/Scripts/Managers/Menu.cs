using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI goldUI;

    private void OnGUI()
    {
        goldUI.text = ("Gold: " + LevelManager.instance.gold);
    }

    public void SetTower()
    {

    }
}
