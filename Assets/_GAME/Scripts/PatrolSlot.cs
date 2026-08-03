using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSlot : MonoBehaviour
{
    public Transform Point;
    public AllyController Owner;

    public bool IsOccupied => Owner != null;
}
