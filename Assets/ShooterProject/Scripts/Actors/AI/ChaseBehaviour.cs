using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public class ChaseBehaviour : IEnemyBehaviour
	{
		#region Fields

		private Vector3 _destination;

		#endregion

		#region Public Methods

		public Vector3 GetDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			return _destination;
		}

		public Vector3 NewDestinationPoint(NavMeshAgent currentAgent, Transform player)
		{
			_destination = player.position;
			return player.position;
		}

		public IEnumerator GetMovingCoroutine(NavMeshAgent currentAgent, Transform player)
		{
			while(true)
			{
				currentAgent.SetDestination(NewDestinationPoint(currentAgent,player));
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}
