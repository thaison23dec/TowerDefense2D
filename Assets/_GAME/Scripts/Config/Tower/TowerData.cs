using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : ScriptableObject
{
    [SerializeField] public int BuyPrice;
    [SerializeField] public int SellPrice;
    [SerializeField] public int UpgradePrice;
    [SerializeField] public int Level;
}
