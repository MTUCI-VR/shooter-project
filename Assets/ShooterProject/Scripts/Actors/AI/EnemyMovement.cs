using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ShooterProject.Scripts.Actors.AI.Behaviours;
using ShooterProject.Scripts.Actors.Health;
namespace ShooterProject.Scripts.Actors.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyMovement : MonoBehaviour
	{
		#region Private Fields

		[SerializeField]
		private Transform targetTransform;

		[SerializeField]
		[Min(1)]
		private float patrolRadius;

		private EnemyBehaviour _behaviour;
		private NavMeshAgent _agent;
		private HealthController _targetHealth;
		#endregion

		#region LifeCycle

		private void Awake()
		{
			_agent = GetComponent<NavMeshAgent>();
		}
		private void Start()
		{
			_behaviour = new PatrolBehaviour(_agent, patrolRadius);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.transform == targetTransform
				&& other.TryGetComponent<HealthController>(out var targetHealth))
			{
				_targetHealth = targetHealth;
				_targetHealth.OnDied += OnTargetDied;
				_behaviour = new ChaseBehaviour(_agent, targetTransform);
			}
		}
		private void Update()
		{
			_behaviour.UpdateDestination();
		}
		#endregion

		#region Private Methods

		private void OnTargetDied()
		{

		}

		#endregion
	}
}
