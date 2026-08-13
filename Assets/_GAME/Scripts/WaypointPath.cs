using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private Transform[] waypointList;
    public Transform[] WaypointList => waypointList;

    private void Awake()
    {
        
    }
}
