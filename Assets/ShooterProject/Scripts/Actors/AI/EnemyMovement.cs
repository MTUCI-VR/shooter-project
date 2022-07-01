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

		[SerializeField] private Transform player;

		private IEnemyBehaviour _behaviour;
		private NavMeshAgent _agent;
		private Coroutine _currentBehaviourCoroutine;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_agent = GetComponent<NavMeshAgent>();
		}
		private void Start()
		{
			ChangeBehaviour(new PatrolBehaviour(40));
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject == player)
			{
				ChangeBehaviour(new ChaseBehaviour());
			}
		}

		#endregion

		#region Private Methods

		private void ChangeBehaviour(IEnemyBehaviour newBehaviour)
		{
			if (_currentBehaviourCoroutine != null)
				StopCoroutine(_currentBehaviourCoroutine);

			_behaviour = newBehaviour;
			_currentBehaviourCoroutine = StartCoroutine(_behaviour.GetMovingCoroutine(_agent,player));
		}

		#endregion
	}
}
