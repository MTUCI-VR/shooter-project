using UnityEngine;
using ShooterProject.Scripts.Actors.Health;

namespace ShooterProject.Scripts.PlayerScripts
{
	[RequireComponent(typeof(Health))]
	public class Player : MonoBehaviour
	{
		#region Fields

		public static Player instance;

		public Health PlayerHealth { get; private set; }

		#endregion

		#region Life Cycle

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}	
			else
			{
				Destroy(gameObject);
			}

			PlayerHealth = GetComponent<Health>();
		}

		#endregion
	}
}
