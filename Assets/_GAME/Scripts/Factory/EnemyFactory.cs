using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public EnemyController Create(EnemyType enemyType, Vector3 position)
    {
        EnemyController prefab = GameManager.Instance.PrefabData.GetEnemy(enemyType);

        if (prefab == null)
        {
            Debug.LogError(
                $"Enemy prefab not found: {enemyType}"
            );

            return null;
        }

        return ObjectPoolManager.Instance.SpawnObject(prefab, position, Quaternion.identity);
    }
}
