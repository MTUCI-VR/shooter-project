using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Spawner
{
	public class EnemySpawner : GeneralSpawner
	{
		#region Fields

		[SerializeField]
		private int spawnDelayInSeconds;

		private bool _canSpawn = true;

		#endregion

		#region  LifeCycle

		private void Start()
		{
			StartCoroutine(EnemySpawn());
		}

		#endregion

		#region Private Methods

		private IEnumerator EnemySpawn()
		{
			while (true)
			{
				yield return new WaitForSeconds(spawnDelayInSeconds);

				if (!_canSpawn) yield break;

				Spawn();
			}
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				_canSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				StartCoroutine(EnemySpawn());

				_canSpawn = true;
			}
		}

		#endregion
	}
}
