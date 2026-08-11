using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBase : UnitController
{
    protected virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    public virtual void OnSpawn()
    {

    }

    public virtual void OnDespawn()
    {

    }

    public void Upgrade()
    {
        Debug.Log("Upgrade " + name);
    }

    public virtual void Sell()
    {
        SimplePool.Spawn<BuildSpot>(
                PoolType.BuildSpot,
                transform.position,
                Quaternion.identity);
        SimplePool.Despawn(this);
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        UIManager.Instance.ShowTower(this);
        Debug.Log("Show " + name);
    }
}
