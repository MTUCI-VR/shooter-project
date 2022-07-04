using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShooterProject.Scripts.Actors.AI
{
	public class ChaseBehaviour : EnemyBehaviour
	{
		#region Fields

		private Transform _target;

		#endregion

		#region Constructors

		public ChaseBehaviour(NavMeshAgent agent, Transform target) : base(agent)
		{
			_target = target;
		}

		#endregion

		#region Public Methods

		public override Vector3 GetDestination()
		{
			return _target.position;
		}

		#endregion
	}
}
