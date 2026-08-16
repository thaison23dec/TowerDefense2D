using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCoinPopUp : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;

    private void Awake()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();

        renderer.sortingOrder = 800;
    }

    public void IncreasePopUp(int money)
    {
        StartCoroutine(IncreasePopUpCoroutine(money));
    }

    public void DecreasePopUp(int money)
    {
        StartCoroutine(DecreasePopUpCoroutine(money));
    }

    IEnumerator IncreasePopUpCoroutine(int money)
    {
        textMesh.text = "+" + money + "$";
        yield return new WaitForSeconds(0.75f);
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }

    IEnumerator DecreasePopUpCoroutine(int money)
    {
        textMesh.text = "-" + money + "$";
        yield return new WaitForSeconds(0.75f);
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }
}
