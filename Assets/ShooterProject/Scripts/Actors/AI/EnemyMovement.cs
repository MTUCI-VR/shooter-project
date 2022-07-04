using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyMovement : MonoBehaviour
	{
		#region Private Fields

		[SerializeField]
		private Transform player;

		[SerializeField]
		[Min(1)]
		private float patrolRadius;

		private EnemyBehaviour _behaviour;
		private NavMeshAgent _agent;

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
			if(other.transform == player)
			{
				_behaviour = new ChaseBehaviour(_agent, player);
			}
		}
		private void Update()
		{
			_agent.SetDestination(_behaviour.GetDestination());
		}
		#endregion

		#region Private Methods
		#endregion
	}
}
