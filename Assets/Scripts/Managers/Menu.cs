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
        goldUI.text = LevelManager.instance.gold.ToString();
    }

    public void SetTower()
    {

    }
}
