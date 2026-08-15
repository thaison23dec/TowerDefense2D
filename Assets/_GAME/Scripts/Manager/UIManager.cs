using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIManager : Singleton<UIManager>
{   
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    [SerializeField] private Text mainTowerHpText;
    [SerializeField] private Text currentCoinText;
    [SerializeField] private Text waveIndexText;

    [SerializeField] private Button startWaveBtn;

    [SerializeField] private GameObject towePanel;
    [SerializeField] private GameObject buildSpotPanel;

    [SerializeField] private Button buyTowerSpawnBtn;
    [SerializeField] private Button buyTowerArcherBtn;
    [SerializeField] private Button buyTowerGunBtn;
    [SerializeField] private Button sellTowerBtn;
    [SerializeField] private Button upgradeTowerBtn;


    [SerializeField] private Text buyTowerSpawnPriceText;
    [SerializeField] private Text buyTowerArcherPriceText;
    [SerializeField] private Text buyTowerGunPriceText;

    [SerializeField] private Text upgradeTowerPriceText;
    [SerializeField] private Text sellTowerPriceText;

    private TowerBase currentTower;
    private BuildSpot currentBuildSpot;

    public event Action OnCoinUpdate;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        towePanel.SetActive(false);
        TriggerCoinUpdate();
        buyTowerSpawnPriceText.text = GameManager.Instance.PrefabData.TowerSpawn.BuyPrice.ToString() + "$";
        buyTowerArcherPriceText.text = GameManager.Instance.PrefabData.TowerArcher.BuyPrice.ToString() + "$";
        buyTowerGunPriceText.text = GameManager.Instance.PrefabData.TowerGun.BuyPrice.ToString() + "$";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mouseWorldPos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit == null || (hit.tag != "Tower" && hit.tag != "BuildSpot"))
            {
                HideAllUI();
            }
        }
    }

    private void OnEnable()
    {
        OnCoinUpdate += UpdateCoinText;
    }

    
    private void OnDisable()
    {
        OnCoinUpdate -= UpdateCoinText;
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        LosePanel.SetActive(true);
    }

    public void UpdateMainTowerHpText(float currentHp)
    {
        mainTowerHpText.text = currentHp.ToString() + "HP";
    }

    public void UpdateCoinText()
    {
        currentCoinText.text = GameManager.Instance.CurrentCoin.ToString() + "$";
    }

    public void UpdateWaveIndexText(int currentWaveIndex, int waveAmount)
    {
        waveIndexText.text = currentWaveIndex.ToString() + "/" + waveAmount.ToString();
    }

    public void TriggerCoinUpdate()
    {
        OnCoinUpdate?.Invoke();
    }

    public void ShowStartWaveBtn()
    {
        startWaveBtn.gameObject.SetActive(true);
    }

    public void HideStartWaveBtn()
    {
        startWaveBtn.gameObject.SetActive(false);
    }

    public void ShowBuildSpotPanel(BuildSpot buildSpot)
    {
        currentBuildSpot = buildSpot;
        HideTower();
        buildSpotPanel.SetActive(true);

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(buildSpot.transform.position);

        buildSpotPanel.transform.position = screenPos;
    }

    public void HideBuildSpotPanel()
    {
        currentBuildSpot = null;
        buildSpotPanel.SetActive(false);
    }

    public void BuyTowerSpawn()
    {
        currentBuildSpot.BuidTower(GameManager.Instance.PrefabData.TowerSpawn);
        HideBuildSpotPanel();
    }

    public void BuyTowerArcher()
    {
        currentBuildSpot.BuidTower(GameManager.Instance.PrefabData.TowerArcher);
        HideBuildSpotPanel();
    }

    public void BuyTowerGun()
    {
        currentBuildSpot.BuidTower(GameManager.Instance.PrefabData.TowerGun);
        HideBuildSpotPanel();
    }

    public void ShowTower(TowerBase tower)
    {
        currentTower = tower;
        HideBuildSpotPanel();
        towePanel.SetActive(true);

        upgradeTowerPriceText.text = tower.UpgradePrice.ToString() + "$";
        sellTowerPriceText.text = tower.SellPrice.ToString() + "$";

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(tower.transform.position);

        towePanel.transform.position = screenPos;
    }

    public void HideTower()
    {
        currentTower = null;
        towePanel.SetActive(false);
    }

    public void UpgradeTower()
    {
        if (currentTower == null)
            return;

        currentTower.Upgrade();
        HideTower();
    }

    public void SellTower()
    {
        if (currentTower == null)
            return;

        currentTower.Sell();
        HideTower();
    }

    public void HideAllUI()
    {
        currentTower = null;
        currentBuildSpot = null;

        towePanel.SetActive(false);
        buildSpotPanel.SetActive(false);
    }

}
