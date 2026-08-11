using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private TowerBase currentTower;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Show(TowerBase tower)
    {
        currentTower = tower;

        panel.SetActive(true);

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(tower.transform.position);

        panel.transform.position = screenPos;
    }

    public void Hide()
    {
        currentTower = null;
        panel.SetActive(false);
    }

    public void Upgrade()
    {
        if (currentTower == null)
            return;

        currentTower.Upgrade();
        Hide();
    }

    public void Sell()
    {
        if (currentTower == null)
            return;

        currentTower.Sell();
        Hide();
    }
}
