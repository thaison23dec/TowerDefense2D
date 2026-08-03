using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] public AllyController Ally;
    public Transform spawnPos;
    public List<PatrolSlot> patrolList; 

    private void Awake()
    {
        
    }
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            PatrolSlot patrolSlot = null;
            if (GetFreeSlot() != null)
            {
                patrolSlot = GetFreeSlot();
            }
            if(patrolSlot != null)
            {
                AllyController a = SimplePool.Spawn<AllyController>(PoolType.Ally_Sword, spawnPos.position, spawnPos.rotation);
                a.patrolPos = patrolSlot.Point;
                patrolSlot.Owner = a;
                a.Init();
            }
        }

    }

    PatrolSlot GetFreeSlot()
    {
        foreach(PatrolSlot slot in patrolList)
        {
            if (!slot.IsOccupied)
            {
                return slot;
            }
        }
        return null;
    }

}
