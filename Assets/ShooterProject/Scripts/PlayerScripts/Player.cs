using UnityEngine;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.PlayerScripts
{
	[RequireComponent(typeof(Health))]
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

	}
}
