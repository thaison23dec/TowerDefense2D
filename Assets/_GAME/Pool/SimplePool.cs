using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    //khoi tao pool moi
    public static void PreLoad(UnitController prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(prefab, amount, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }

    //lay phan tu ra
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : UnitController
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + " IS NOT PRELOAD");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }

    //tra phan tu vao
    public static void Despawn(UnitController unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + " IS NOT PRELOAD");
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }


    //thu thap phan tu
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + " IS NOT PRELOAD");
        }
        poolInstance[poolType].Collect();
    }

    //thu thap tat ca 
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    //destroy 1 pool
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + " IS NOT PRELOAD");
        }
        poolInstance[poolType].Release();
    }

    //destroy tat ca
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;
    UnitController prefab;
    //list chua cac unit dang o trong pool
    Queue<UnitController> inactives = new Queue<UnitController>();
    //list chua cac unit dang duoc su dung
    List<UnitController> actives = new List<UnitController>();

    //khoi tao pool
    public void PreLoad(UnitController prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }

    //lay phan tu tu pool
    public UnitController Spawn(Vector3 pos, Quaternion rot)
    {
        UnitController unit;

        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
            Debug.Log("instantiate");
        }
        else
        {
            unit = inactives.Dequeue();
            Debug.Log("dequeue");
        }

        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);

        return unit;
    }

    //tra phan tu vao trong pool
    public void Despawn(UnitController unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }

    //thu thap tat ca phan tu dang dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    //destroy tat ca phan tu
    public void Release()
    {
        Collect();
        for (int i = 0; i < actives.Count; i++)
        {
            GameObject.Destroy(actives[i].gameObject);
        }
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}

