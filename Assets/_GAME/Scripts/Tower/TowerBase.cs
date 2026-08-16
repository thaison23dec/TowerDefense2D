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

public class TowerBase : MonoBehaviour
{
    public TowerType Type;
    public TowerLevel Level;
    [SerializeField] protected TowerData towerData;
    [SerializeField] protected TowerBase upgradedTower;
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

    public virtual void Upgrade()
    {
        Debug.Log("Upgrade " + name);
        if (GameManager.Instance.DecreaseCoin(UpgradePrice) && upgradedTower != null)
        {
            UIManager.Instance.TriggerCoinUpdate();
            OnDespawn();
            Instantiate(upgradedTower, transform.position, Quaternion.identity);
            VFXCoinPopUp popUp = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.VFXCoinPopUp, transform.position, Quaternion.identity);
            popUp.DecreasePopUp(UpgradePrice);
            Destroy(gameObject);
        }
    }

    public virtual void Sell()
    {
        GameManager.Instance.IncreaseCoin(towerData.SellPrice);
        UIManager.Instance.TriggerCoinUpdate();
        VFXCoinPopUp popUp = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.VFXCoinPopUp, transform.position, Quaternion.identity);
        popUp.IncreasePopUp(towerData.SellPrice);
        OnDespawn();
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
