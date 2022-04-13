using UnityEngine;
using System.Collections;

namespace ShooterProject.Scripts.Spawner
{
	public class EnemySpawner : GeneralSpawner
	{
		#region Fields

		[SerializeField]
		private int spawnDelayInSeconds;

		[SerializeField]
		private string playerTag;

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
			if (collider.tag == playerTag)
			{
				_canSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.tag == playerTag)
			{
				StartCoroutine(EnemySpawn());

				_canSpawn = true;
			}
		}

		#endregion
	}
}
