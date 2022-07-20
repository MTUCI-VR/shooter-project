using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI.Behaviours
{
	public class PatrolBehaviour : EnemyBehaviour
	{
		#region Fields

		private float _patrolAreaRadius;
		private Vector3 _destination;

		#endregion

		#region Constructors

		public PatrolBehaviour(NavMeshAgent agent, float patrolAreaRadius) : base(agent)
		{
			_patrolAreaRadius = patrolAreaRadius;
		}

		#endregion

		#region Public Methods

		public override Vector3 GetDestination()
		{
			if (!_agent.hasPath)
				CalculateDestination();
			return _destination;
		}
		public override string GetAnimationTriggerName()
		{
			return "Moving";
		}

		#endregion

		#region Private Methods

		private void CalculateDestination()
		{
			Vector3 direction = Random.insideUnitSphere * _patrolAreaRadius;
			direction += _agent.transform.position;
			NavMesh.SamplePosition(direction, out var hit, Random.Range(0f, _patrolAreaRadius), NavMesh.AllAreas);

			_destination = hit.position;
		}

		#endregion

	}
}
