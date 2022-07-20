using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI.Behaviours
{
	public abstract class EnemyBehaviour
	{
		#region Fields

		protected NavMeshAgent _agent;

		#endregion

		#region Constructors

		public EnemyBehaviour(NavMeshAgent agent)
		{
			_agent = agent;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Возвращает точку назначения
		/// </summary>
		/// <returns>Координаты точки назначения</returns>
		public abstract Vector3 GetDestination();

		/// <summary>
		/// Обновляет цель движения агента
		/// </summary>
		public void UpdateDestination()
		{
			var newDestination = GetDestination();
			if (_agent.destination != newDestination)
			{
				_agent.SetDestination(newDestination);
			}
		}

		/// <summary>
		/// Возвращает название триггера для переключения анимации
		/// </summary>
		/// <returns>название триггера</returns>
		public virtual string GetAnimationTriggerName()
		{
			return this.GetType().Name;
		}
		#endregion
	}
}
