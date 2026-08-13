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
        gameObject.SetActive(false);

        switch (tower.Type)
        {
            case TowerType.Archer:
                Instantiate(GameManager.Instance.PrefabData.TowerArcher, transform.position, Quaternion.identity);
                break;
            case TowerType.Gun:
                Instantiate(GameManager.Instance.PrefabData.TowerGun, transform.position, Quaternion.identity);
                break;
            case TowerType.Spawn:
                Instantiate(GameManager.Instance.PrefabData.TowerSpawn, transform.position, Quaternion.identity).Init();
                break;
        }
    }
}
