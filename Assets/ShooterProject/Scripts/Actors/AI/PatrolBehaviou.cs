using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public class PatrolBehaviour : IEnemyBehaviour
	{
		#region Fields

		private float _patrolAreaRadius;
		private Vector3 _destination;

		#endregion

		#region Constructors

		public PatrolBehaviour(float patrolAreaRadius)
		{
			_patrolAreaRadius = patrolAreaRadius;
		}

		#endregion

		#region Private Methods

		private void CalculateDestination(NavMeshAgent currentAgent, Transform player)
		{
			var randomPoint = new Vector3(Random.Range(-_patrolAreaRadius, _patrolAreaRadius), currentAgent.transform.position.y, Random.Range(-_patrolAreaRadius, _patrolAreaRadius));
			NavMesh.SamplePosition(randomPoint, out var hit, 200, NavMesh.AllAreas);
			_destination = hit.position;
		}

		#endregion

		#region Public Methods
		public Vector3 GetDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			return _destination;
		}

		public Vector3 NewDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			CalculateDestination(currentAgent, player);
			currentAgent.SetDestination(_destination);
			return _destination;
		}

		public IEnumerator GetMovingCoroutine(NavMeshAgent currentAgent, Transform player)
		{
			while (true)
			{
				NewDestinationPoint(currentAgent, player);
				while (currentAgent.hasPath)
					yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}
