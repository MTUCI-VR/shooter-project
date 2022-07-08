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
			NavMeshHit hit;
			var path = new NavMeshPath();
			do
			{
				var randomPoint = new Vector3(Random.Range(-_patrolAreaRadius, _patrolAreaRadius),
					_agent.transform.position.y, Random.Range(-_patrolAreaRadius, _patrolAreaRadius));
				NavMesh.SamplePosition(randomPoint, out hit, int.MaxValue, NavMesh.AllAreas);
			} while (!_agent.CalculatePath(hit.position, path));
			Debug.Log(path.status);
			_destination = hit.position;
		}

		#endregion

	}
}
