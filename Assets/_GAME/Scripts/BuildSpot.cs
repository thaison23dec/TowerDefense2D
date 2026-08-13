using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : UnitController
{
    private void OnMouseDown()
    {
        UIManager.Instance.ShowBuildSpotPanel(this);
        Debug.Log("Show " + name);
    }

    public void BuidTower(TowerBase tower)
    {
        

        switch (tower.Type)
        {
            case TowerType.Archer:           
                if (GameManager.Instance.DecreaseCoin(GameManager.Instance.PrefabData.TowerArcher.BuyPrice))
                {
                    UIManager.Instance.TriggerCoinUpdate();
                    gameObject.SetActive(false);
                    Instantiate(GameManager.Instance.PrefabData.TowerArcher, transform.position, Quaternion.identity);
                }
                break;
            case TowerType.Gun:
                if (GameManager.Instance.DecreaseCoin(GameManager.Instance.PrefabData.TowerGun.BuyPrice))
                {
                    UIManager.Instance.TriggerCoinUpdate();
                    gameObject.SetActive(false);
                    Instantiate(GameManager.Instance.PrefabData.TowerGun, transform.position, Quaternion.identity);
                }
                break;
            case TowerType.Spawn:
                if (GameManager.Instance.DecreaseCoin(GameManager.Instance.PrefabData.TowerSpawn.BuyPrice))
                {
                    UIManager.Instance.TriggerCoinUpdate();
                    gameObject.SetActive(false);
                    Instantiate(GameManager.Instance.PrefabData.TowerSpawn, transform.position, Quaternion.identity).Init();
                }
                break;
        }
    }
}
