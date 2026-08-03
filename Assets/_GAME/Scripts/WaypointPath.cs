using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public static WaypointPath Instance { get; private set; }

    [SerializeField] private Transform[] waypointList;
    public Transform[] WaypointList => waypointList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
