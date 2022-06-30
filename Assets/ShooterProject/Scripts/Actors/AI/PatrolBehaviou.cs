using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public class PatrolBehaviour : IEnemyBehaviour
	{

		private float _patrolAreaRadius;
		public PatrolBehaviour(float patrolAreaRadius)
		{
			_patrolAreaRadius = patrolAreaRadius;
		}
		public Vector3 GetDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			var randomPoint = new Vector3(Random.Range(-_patrolAreaRadius, _patrolAreaRadius), currentAgent.transform.position.y, Random.Range(-_patrolAreaRadius, _patrolAreaRadius));
			NavMesh.SamplePosition(randomPoint, out var hit, 200, NavMesh.AllAreas);
			return hit.position;
		}

		public Vector3 SetDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			var point = GetDestinationPoint(currentAgent, player);
			currentAgent.SetDestination(point);
			return point;
		}
	}
}
