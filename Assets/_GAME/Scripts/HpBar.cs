using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject bg;
    public GameObject fill;
    private Coroutine hpBarCoroutine;

    public void UpdateBar(float fillHp, float maxHp)
    {
        if (fillHp <= 0)
            fillHp = 0;

        float targetPercent = fillHp / maxHp;

        if (hpBarCoroutine != null)
            StopCoroutine(hpBarCoroutine);

        hpBarCoroutine = StartCoroutine(
            SmoothUpdateBar(targetPercent)
        );
    }

    private IEnumerator SmoothUpdateBar(float targetPercent)
    {
        float currentPercent = fill.transform.localScale.x;

        while (currentPercent > targetPercent)
        {
            currentPercent = Mathf.MoveTowards(
                currentPercent,
                targetPercent,
                2f * Time.deltaTime
            );

            fill.transform.localScale = new Vector3(
                currentPercent,
                1f,
                1f
            );

            yield return null;
        }

        fill.transform.localScale = new Vector3(
            targetPercent,
            1f,
            1f
        );

        hpBarCoroutine = null;
    }
}
