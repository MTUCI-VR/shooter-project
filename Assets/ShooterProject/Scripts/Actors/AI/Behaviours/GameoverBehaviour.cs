using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI.Behaviours
{
	public class GameoverBehaviour : EnemyBehaviour
	{ 
		#region Constructors

		public GameoverBehaviour(NavMeshAgent agent) : base(agent)
		{
		}

		#endregion

		#region Public Methods

		public override Vector3 GetDestination()
		{
			return _agent.transform.position;
		}

		#endregion
	}
}
