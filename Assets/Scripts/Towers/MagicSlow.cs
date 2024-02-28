using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject attackPrefab;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float shootSpeed = 1f;

    private float timeUntilFire;
    private void Update()
    {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / shootSpeed)
            {
            RootEnemies();
                timeUntilFire = 0f;
            }
        }

    private void RootEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
    }
}

