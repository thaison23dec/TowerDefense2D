using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : MonoBehaviour
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
                    Instantiate(GameManager.Instance.PrefabData.TowerArcher, transform.position, Quaternion.identity);
                    VFXCoinPopUp popUp = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.VFXCoinPopUp, transform.position, Quaternion.identity);
                    popUp.DecreasePopUp(GameManager.Instance.PrefabData.TowerArcher.BuyPrice);
                    Destroy(gameObject);
                }
                break;
            case TowerType.Gun:
                if (GameManager.Instance.DecreaseCoin(GameManager.Instance.PrefabData.TowerGun.BuyPrice))
                {
                    UIManager.Instance.TriggerCoinUpdate();
                    Instantiate(GameManager.Instance.PrefabData.TowerGun, transform.position, Quaternion.identity);
                    VFXCoinPopUp popUp = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.VFXCoinPopUp, transform.position, Quaternion.identity);
                    popUp.DecreasePopUp(GameManager.Instance.PrefabData.TowerGun.BuyPrice);
                    Destroy(gameObject);
                }
                break;
            case TowerType.Spawn:
                if (GameManager.Instance.DecreaseCoin(GameManager.Instance.PrefabData.TowerSpawn.BuyPrice))
                {
                    UIManager.Instance.TriggerCoinUpdate();
                    Instantiate(GameManager.Instance.PrefabData.TowerSpawn, transform.position, Quaternion.identity).Init();
                    VFXCoinPopUp popUp = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.VFXCoinPopUp, transform.position, Quaternion.identity);
                    popUp.DecreasePopUp(GameManager.Instance.PrefabData.TowerSpawn.BuyPrice);
                    Destroy(gameObject);
                }
                break;
        }
    }
}
