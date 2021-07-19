using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPCs : MonoBehaviour
{
    private void FixedUpdate()
    {
        foreach (Transform NPC in transform)
        {
            DestinationPath destinationPath = NPC.GetComponent<DestinationPath>();
            NavMeshAgent navMeshAgent = NPC.GetComponent<NavMeshAgent>();
            if (navMeshAgent.remainingDistance < 5)
            {
                int nextTargetIndex = ++destinationPath.curretTarget % destinationPath.targets.Count;
                navMeshAgent.SetDestination(destinationPath.targets[nextTargetIndex].position);
            }
        }
    }
}
