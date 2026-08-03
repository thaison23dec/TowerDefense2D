using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject bg;
    public GameObject fill;



    public void UpdateBar(float fillHp, float maxHp)
    {
        if (fillHp <= 0) fillHp = 0;
        float percent = fillHp / maxHp;

        fill.transform.localScale = new Vector3(
            percent,
            1f,
            1f
        );
    }
}
