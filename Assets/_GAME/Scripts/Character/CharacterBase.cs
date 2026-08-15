using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] public CharacterData data;
    public Animator animator;
    public GameObject graphic;
    public CharacterBase currentTarget;
    public float currentHp;
    public float damage;
    public TeamType Team;
    public bool IsDead;
    [SerializeField] protected Collider2D collider;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] HpBar hpBar;
    protected string currentAnimName;
    private Coroutine attackCoroutine;
    public CHARACTER_STATE currentState;
    public LayerMask targetMask;
    private List<Vector3> targetList;

    public enum CHARACTER_STATE
    {
        IDLE,
        WALK_FREE,
        WALK_MOVE_TO_TARGET,
        ATTACK,
        DIE
    }


    public virtual void Init()
    {    
        currentTarget = null;
        IsDead = false;
        currentHp = data.Hp;
        damage = data.Damage;
        Team = data.TeamType;
        attackCoroutine = null;
        currentState = CHARACTER_STATE.WALK_FREE;
        hpBar.UpdateBar(currentHp, data.Hp);
        graphic.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(true);
    }

    protected virtual void Awake()
    {
        targetList = new List<Vector3>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        Init();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        IsTrackTarget();
        ProcessState();
        if(currentHp <= 0 && !IsDead)
        {
            Die();
        }
    }

    private void ProcessState() {
        switch (currentState)
        {
            case CHARACTER_STATE.IDLE:
                if (IsTrackTarget())
                {
                    DirectToTarget();
                }
                break;

            case CHARACTER_STATE.WALK_FREE:
                if (IsTrackTarget())
                {
                    DirectToTarget();
                } else
                {
                    Walk();
                }

                break;

            case CHARACTER_STATE.WALK_MOVE_TO_TARGET:

                MoveToTarget();

                break;

            case CHARACTER_STATE.ATTACK:
                if (currentTarget == null)
                {
                    Walk();
                }
                break;

            case CHARACTER_STATE.DIE:
                
                break;
        }
    }

    public virtual void OnDespawn()
    {
        //SimplePool.Despawn(this);
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }

    private void DirectToTarget()
    {
        if (IsDead) return;
        currentState = CHARACTER_STATE.WALK_MOVE_TO_TARGET;
        currentTarget = LoadTarget();
    }


    private void MoveToTarget()
    {
        if (IsDead) return;
        if (currentTarget != null)
        {
            Vector3 targetPos = currentTarget.transform.position;
            Vector3 currentPos = transform.position;
            float distance = Vector3.Distance(currentPos, targetPos);

            if (distance >= data.AttackRange)
            {
                Vector3 directionTravel = targetPos - currentPos;
                directionTravel.Normalize();
                FacingCheck(targetPos);
                rb.MovePosition(currentPos + (directionTravel * Mathf.Abs(data.MoveSpeed) * Time.deltaTime));
                //ChangeAnim("walk");
                ChangeAnimBool("walk");


            }
            else
            {
                StartAttack();
            }
                
        }
        else
        {
            currentTarget = LoadTarget();
            if (currentTarget == null)
            {
                Walk();
            }
        }
    }


    public bool IsTrackTarget()
    {
        bool isTrack = false;
        Collider2D[] targetsCol = Physics2D.OverlapCircleAll(transform.position, data.SightRange, targetMask);
        
        if(targetsCol.Length > 0)
        {
            for(int i = 0; i < targetsCol.Length; i++)
            {
                if (Vector2.Distance(transform.position, targetsCol[i].transform.position) < data.SightRange)
                {
                    isTrack = true;
                    return isTrack;
                }
            }
            isTrack = false;
        }
        return isTrack;
    }

    public int FindNearestTarget(List<Vector3> targetArr, Vector3 centerPos)
    {
        int nearestTargetIndex = 0;
        float nearestDistance = Vector3.Distance(targetArr[nearestTargetIndex], centerPos);
        for(int i = 0; i < targetArr.Count; i++)
        {
            if(nearestDistance > Vector3.Distance(targetArr[i], centerPos))
            {
                nearestTargetIndex = i;
                nearestDistance = Vector3.Distance(targetArr[i], centerPos);
            }
        }
        return nearestTargetIndex;
    }

    private CharacterBase LoadTarget()
    {
        CharacterBase target = null;

        Collider2D[] targetCol = Physics2D.OverlapCircleAll(transform.position, data.SightRange, targetMask);

        if (targetCol.Length > 0)
        {
            targetList.Clear();
            for (int i = 0; i < targetCol.Length; i++)
            {
                targetList.Add(targetCol[i].transform.position);
            }

            int nearestIndex = FindNearestTarget(targetList, transform.position);

            CharacterBase tempTarget = targetCol[nearestIndex].GetComponent<CharacterBase>();

            if (tempTarget.currentHp > 0.0f && tempTarget.currentState != CHARACTER_STATE.DIE)
                target = tempTarget;
        }

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > data.SightRange)
            {
                target = null;
            }

        }


        return target;
    }

    public void MoveToPosition(Vector3 arrival)
    {
        if (IsDead) return;
        Vector3 currentPos = transform.position;
        Vector3 directionTravel = arrival - currentPos;
        directionTravel.Normalize();
        FacingCheck(arrival);
        rb.MovePosition(currentPos + (directionTravel * Mathf.Abs(data.MoveSpeed) * Time.deltaTime));
    }


    public void FacingCheck(Vector3 arrival)
    {
        if (arrival.x > transform.position.x)
        {
            graphic.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            graphic.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public virtual void Walk()
    {
        if (IsDead) return;
        ChangeAnimBool("walk");
        currentState = CHARACTER_STATE.WALK_FREE;  
    }

    protected void Idle()
    {
        if (IsDead) return;
        currentState = CHARACTER_STATE.IDLE;
        ChangeAnimBool("idle");
        //ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    protected virtual void Die()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;
        currentState = CHARACTER_STATE.DIE;
        AudioManager.Instance.PlayDie();
        animator.SetBool("idle", false);
        animator.SetBool("walk", false);
        ChangeAnimTrigger("die");
        Invoke("OnDespawn", 1f);
    }


    public void StartAttack()
    {
        if (IsDead) return;
        if (attackCoroutine == null)
        {
            currentState = CHARACTER_STATE.ATTACK;
            animator.SetBool("idle", false);
            animator.SetBool("walk", false);
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            Walk();
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (currentTarget != null &&
      !currentTarget.IsDead && Vector2.Distance(transform.position, currentTarget.transform.position) < data.AttackRange)
        {
            //rb.velocity = Vector2.zero;

            ChangeAnimTrigger("attack");
            AudioManager.Instance.PlayHit();

            yield return new WaitForSeconds(0.5f);
            currentTarget.TakeDamage(damage);

            yield return new WaitForSeconds(data.AttackCooldown);
        }

        currentTarget = null;


        StopAttack();
    }


    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        hpBar.UpdateBar(currentHp, data.Hp);
    }

    public void ChangeAnimTrigger(string animName)
    {
        //if (animName != currentAnimName)
        //    animator.ResetTrigger(animName);
        if(animName != "attack")
        {
            if (currentAnimName == animName) return;
        }
        currentAnimName = animName;
        animator.SetTrigger(currentAnimName);
    }

    public void ChangeAnimBool(string animName)
    {
        if (animator.GetBool(animName) == false)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", false);
            animator.SetBool(animName, true);
        }
    }

}

public enum TeamType
{
    Ally,
    Enemy
}
