using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI.Behaviours
{
	public class GameoverBehaviour : EnemyBehaviour
	{
		public GameoverBehaviour(NavMeshAgent agent) : base(agent)
		{
		}

		public override Vector3 GetDestination()
		{
			return _agent.transform.position;
		}
	}
}
