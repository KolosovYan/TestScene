using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public enum WaypointMovementType
    {
        Running,
        Walking
    }

    public WaypointMovementType CurrentWaypointType;
}
