using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public interface IEnemyBehaviour
	{
		public Vector3 GetDestinationPoint(NavMeshAgent currentAgent, Transform player);
		public Vector3 SetDestinationPoint(NavMeshAgent currentAgent, Transform player);
	}
}
