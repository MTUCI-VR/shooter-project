using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShooterProject.Scripts.PlayerScripts;
using System;

namespace ShooterProject.Scripts.Spawner
{
	public class EnemySpawner : GeneralSpawner
	{
		#region Fields
		[SerializeField]
		private int spawnDelayInSeconds;

		private bool _canSpawn = true;

		#endregion

		#region Properties

		public bool CanSpawn
		{
			get
			{
				return _canSpawn;
			}
			private set
			{
				OnActivationChanged?.Invoke(this, value);
				_canSpawn = value;
			}
		}

		#endregion

		#region Event

		public event Action<EnemySpawner,bool> OnActivationChanged;

		#endregion

		#region Public Methods

		public GameObject SpawnEnemy()
		{
			if (_canSpawn)
			{
				StartCoroutine(SpawnCoolDownCoroutine(spawnDelayInSeconds));
				return Spawn();
			}
			return null;
		}

		#endregion

		#region Private Methods

		private IEnumerator SpawnCoolDownCoroutine(float spawnDelayInSeconds)
		{
			CanSpawn = false;
			yield return new WaitForSeconds(spawnDelayInSeconds);
			CanSpawn = true;
		}
		private void OnTriggerEnter(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				CanSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				CanSpawn = true;
			}
		}

		#endregion
	}
}
