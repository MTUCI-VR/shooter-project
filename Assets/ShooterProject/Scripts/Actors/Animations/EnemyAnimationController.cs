using ShooterProject.Scripts.Actors.AI;
using ShooterProject.Scripts.Actors.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.Actors.Animations
{
	[RequireComponent(typeof(EnemyMovement), typeof(Animator))]
	public class EnemyAnimationController : MonoBehaviour
	{
		#region Fields

		private EnemyMovement _enemyMovement;
		private Animator _animator;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_enemyMovement = GetComponent<EnemyMovement>();
			_animator = GetComponent<Animator>();
		}

		private void OnEnable()
		{
			_enemyMovement.BehaviourChanged += BehaviourChanged;

			if (_enemyMovement.Behaviour != null)
				_animator.SetTrigger(_enemyMovement.Behaviour.GetAnimationTriggerName());
		}

		private void OnDisable()
		{
			_enemyMovement.BehaviourChanged -= BehaviourChanged;
		}

		#endregion

		#region Private Methods

		private void BehaviourChanged(EnemyBehaviour behaviour)
		{
			_animator.SetTrigger(behaviour.GetAnimationTriggerName());
		}


		#endregion
	}
}
