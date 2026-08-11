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
}
