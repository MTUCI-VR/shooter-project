using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public interface IEnemyBehaviour
	{
		#region Public Methods

		/// <summary>
		/// Возвращает точку назначения
		/// </summary>
		/// <param name="currentAgent">Компонент NavMeshAgent текущего агента</param>
		/// <param name="player">Transform Игрока</param>
		/// <returns>Координаты точки назначения</returns>
		public Vector3 GetDestinationPoint(NavMeshAgent currentAgent, Transform player);

		/// <summary>
		/// Задает точку назначения агенту
		/// </summary>
		/// <param name="currentAgent">Компонент NavMeshAgent текущего агента</param>
		/// <param name="player">Transform Игрока</param>
		/// <returns>Координаты точки назначения</returns>
		public Vector3 SetDestinationPoint(NavMeshAgent currentAgent, Transform player);

		#endregion
	}
}
