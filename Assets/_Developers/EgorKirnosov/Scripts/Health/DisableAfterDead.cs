using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Developers.EgorKirnosov.Scripts.Health
{
	[RequireComponent(typeof(ShooterProject.Scripts.Actors.Health.Health))]
	public class DisableAfterDead : MonoBehaviour
	{
		#region Fields

		private ShooterProject.Scripts.Actors.Health.Health _health;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_health = GetComponent<ShooterProject.Scripts.Actors.Health.Health>();
		}
		private void OnEnable()
		{
			_health.OnDied += OnDied;
		}
		private void OnDisable()
		{
			_health.OnDied -= OnDied;
		}

		#endregion

		#region Private Methods

		private void OnDied(ShooterProject.Scripts.Actors.Health.Health sendeer)
		{
			gameObject.SetActive(false);
		}

		#endregion
	}
}
