using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXExplosion : UnitController
{
    [SerializeField] private float lifeTime = 2f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        animator.Play("Explosion", 0, 0f);
        StartCoroutine(AutoDespawn());
    }

    private IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(lifeTime);
        SimplePool.Despawn(this);
    }
}
