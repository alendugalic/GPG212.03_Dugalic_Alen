using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerMain[] towerPrefabs;

    private int selectedTower = 0;

    public static BuildManager Instance;

   

    private void Awake()
    {
        Instance = this;
    }

    public TowerMain GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }

    public void PlaceSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
