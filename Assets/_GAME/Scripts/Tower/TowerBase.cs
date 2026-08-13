using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TowerType
{
    Archer,
    Gun,
    Spawn,
}

public class TowerBase : UnitController
{
    public TowerType Type;

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
        Instantiate(GameManager.Instance.PrefabData.BuildSpot, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        UIManager.Instance.ShowTower(this);
        Debug.Log("Show " + name);
    }
}
