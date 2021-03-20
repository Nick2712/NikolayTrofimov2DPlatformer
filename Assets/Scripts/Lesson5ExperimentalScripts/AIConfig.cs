using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    [Serializable]
    public struct AIConfig
    {
        public float speed;
        public float minSqrDistanceToTarget;
        public Transform[] waypoints;
    }
}