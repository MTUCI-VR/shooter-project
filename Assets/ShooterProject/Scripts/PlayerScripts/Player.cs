using UnityEngine;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.General;
using ShooterProject.Scripts.GameManager;

namespace ShooterProject.Scripts.PlayerScripts
{
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(CharacterController))]
	public class Player : Singleton<Player>
	{
		#region Fields

		private Health _playerHealth;

		#endregion

		#region  Properties

		public Health PlayerHealth
		{
			get
			{
				if (_playerHealth == null)
				{
					_playerHealth = GetComponent<Health>();
				}

				return _playerHealth;
			}
		}

		#endregion

		#region Life Cycle

		protected override void OnEnable()
		{
			SceneLoader.OnSceneSwitched += ResetPlayer;
		}

		protected override void OnDisable()
		{
			SceneLoader.OnSceneSwitched -= ResetPlayer;
		}

		#endregion

		#region Private Methods

		private void ResetPlayer()
		{
			transform.position = Vector3.zero;

			PlayerHealth.Reset();
		}

		#endregion
	}
}
