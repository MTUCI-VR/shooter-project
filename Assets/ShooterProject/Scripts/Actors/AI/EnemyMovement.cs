using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyMovement : MonoBehaviour
	{
		private IEnemyBehaviour _behaviour;
		private NavMeshAgent _agent;
		private Vector3 _target;

		private void Awake()
		{
			_agent = GetComponent<NavMeshAgent>();
		}
		private void Start()
		{
			_behaviour = new PatrolBehaviour(40);
		}

		private void Update()
		{
			if(!_agent.hasPath)
				_target = _behaviour.SetDestinationPoint(_agent, transform);
		}
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(_target,5);
		}
	}
}
