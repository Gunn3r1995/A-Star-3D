﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public bool walkable;
    public Vector3 WorldPosition;

    public Node(bool _walkable, Vector3 _worldPosition)
    {
        walkable = _walkable;
        WorldPosition = _worldPosition;
    }
}
