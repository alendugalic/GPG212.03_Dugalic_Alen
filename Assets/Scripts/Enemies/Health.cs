using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int HP = 2;
    [SerializeField] private int goldValue = 5;


    private bool isDestroyed = false;
    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0 && !isDestroyed)
        {
            Spawner.onEnemyDestroy.Invoke();
            LevelManager.instance.IncreaseGold(goldValue);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
