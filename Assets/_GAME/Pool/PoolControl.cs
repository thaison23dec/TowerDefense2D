using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;
    [SerializeField] UnitController[] unitList;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < unitList.Length; i++)
        {
            SimplePool.PreLoad(unitList[i], 0, new GameObject(unitList[i].name).transform);
        }

        //load tu list
        for (int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.PreLoad(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }


}

[System.Serializable]
public class PoolAmount
{
    public UnitController prefab;
    public Transform parent;
    public int amount;
}

public enum PoolType
{
    //CHARACTER
    Enemy_Orc,
    Ally_Sword,


    //PROJECTILE
    Projectile_Arrow,
    Projectile_Bullet,


    //VFX
    Explosion,

    //Tower
    Tower_Spawn,
    Tower_Archer,
    Tower_Gun,

    //BuildSpot
    BuildSpot,
}

