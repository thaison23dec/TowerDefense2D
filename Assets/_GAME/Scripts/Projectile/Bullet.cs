using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float progress;
    [SerializeField] private float arcHeight = 2f;
    public LayerMask targetMask;


    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Init(Vector3 targetPos)
    {
        startPosition = transform.position;
        targetPosition = targetPos;
        progress = 0f;
    }

    protected override void Update()
    {
        float distance = Vector3.Distance(startPosition, targetPosition);

        if (distance <= 0.01f)
            return;

        progress += (speed * Time.deltaTime) / distance;
        progress = Mathf.Clamp01(progress);

        Vector3 pos = Vector3.Lerp(startPosition, targetPosition, progress);

        pos.y += Mathf.Sin(progress * Mathf.PI) * arcHeight;

        transform.position = pos;

        float nextProgress = Mathf.Min(progress + 0.01f, 1f);

        Vector3 nextPos = Vector3.Lerp(startPosition, targetPosition, nextProgress);
        nextPos.y += Mathf.Sin(nextProgress * Mathf.PI) * arcHeight;

        Vector2 dir = (nextPos - pos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if (progress >= 1f)
        {
            Explosion(targetPosition);
            SimplePool.Despawn(this);
        }
    }


    private void Explosion(Vector3 explodePos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explodePos, 0.75f, targetMask);

        SimplePool.Spawn<VFXExplosion>(
            PoolType.Explosion,
            explodePos,
            Quaternion.identity);

        foreach (Collider2D hit in hitColliders)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();

            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }
}
