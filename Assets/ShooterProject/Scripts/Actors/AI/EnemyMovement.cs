using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ShooterProject.Scripts.Actors.AI.Behaviours;
using ShooterProject.Scripts.Actors.Health;
using System;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Actors.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyMovement : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		[Min(1)]
		private float patrolRadius;

		[SerializeField]
		[Min(0)]
		private float attackStartRadius;

		[SerializeField]
		[Min(1)]
		private float attackEndRadius;

		private EnemyBehaviour _behaviour;
		private NavMeshAgent _agent;
		private Health.Health _targetHealth;
		private Transform _targetTransform;
		#endregion

		#region Events

		public event Action<EnemyBehaviour> BehaviourChanged;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_agent = GetComponent<NavMeshAgent>();
		}

		private void Start()
		{
			ChangeBehaviour(new PatrolBehaviour(_agent, patrolRadius));
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Player>(out var player)
				&& other.TryGetComponent<Health.Health>(out var targetHealth))
			{
				_targetTransform = player.transform;
				_targetHealth = targetHealth;
				_targetHealth.OnDied += OnTargetDied;
				ChangeBehaviour(new ChaseBehaviour(_agent, _targetTransform));
			}
		}

		private void OnEnable()
		{
			if (_targetHealth != null)
				_targetHealth.OnDied += OnTargetDied;
		}

		private void OnDisable()
		{
			if (_targetHealth != null)
				_targetHealth.OnDied -= OnTargetDied;
		}

		private void Update()
		{
			_behaviour.UpdateDestination();
			if (_behaviour.GetType() == typeof(ChaseBehaviour)
				&& _agent.hasPath
				&& _agent.remainingDistance < attackStartRadius)
			{
				ChangeBehaviour(new AttackBehaviour(_agent));
			}
			else if (_behaviour.GetType() == typeof(AttackBehaviour)
				&& (_agent.transform.position - _targetTransform.position).magnitude > attackEndRadius)
			{
				ChangeBehaviour(new ChaseBehaviour(_agent, _targetTransform));
			}
		}

		#endregion

		#region Private Methods

		private void ChangeBehaviour(EnemyBehaviour newBehaviour)
		{
			_behaviour = newBehaviour;
			BehaviourChanged?.Invoke(newBehaviour);
		}

		private void OnTargetDied(Health.Health targetHealth)
		{
			ChangeBehaviour(new GameoverBehaviour(_agent));
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackStartRadius);
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, attackEndRadius);
		}

		private void OnDrawGizmos()
		{
			if(_behaviour == null) return;
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(_behaviour.GetDestination(),2);
		}

		#endregion
	}
}
