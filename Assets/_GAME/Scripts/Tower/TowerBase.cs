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

public enum TowerLevel
{
    Lv1,
    Lv2,
}

public class TowerBase : UnitController
{
    public TowerType Type;
    public TowerLevel Level;
    [SerializeField] protected TowerData towerData;
    public int BuyPrice;
    public int SellPrice;
    public int UpgradePrice;

    protected virtual void Start()
    {
        Init();
    }

    private void OnValidate()
    {
        BuyPrice = towerData.BuyPrice;
        SellPrice = towerData.SellPrice;
        UpgradePrice = towerData.UpgradePrice;
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
        GameManager.Instance.DecreaseCoin(SellPrice);
        Destroy(gameObject);
    }

    public virtual void Sell()
    {
        GameManager.Instance.IncreaseCoin(towerData.SellPrice);
        UIManager.Instance.TriggerCoinUpdate();
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
