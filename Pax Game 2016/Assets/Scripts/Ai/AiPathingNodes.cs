﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// singleton class for setting up the ai pathfinding nodes
/// </summary>
public class AiPathingNodes : MonoBehaviour
{

    public List<Transform> pathfindingNodes;
    public bool addAllChildren;
	void Awake ()
    {
        if (addAllChildren)
        {
            pathfindingNodes.Clear();
            pathfindingNodes.AddRange(gameObject.GetComponentsInChildren<Transform>());
            pathfindingNodes.Remove(transform);
           
        }
        AiPathFinding.patrolNodes = pathfindingNodes.ToArray();
    }
	
}
