using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.Items;

namespace ShooterProject.Scripts.Spawner
{
	public class ItemSpawner : GeneralSpawner
	{
		#region Fields

		[SerializeField]
		private int spawnDelayInSeconds;

		private bool _canSpawn = true;

		#endregion

		#region  LifeCycle

		private void Start()
		{
			StartCoroutine(ItemSpawn());
		}

		#endregion

		#region Private Methods

		private IEnumerator ItemSpawn()
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
			if (collider.TryGetComponent<Item>(out Item item))
			{Debug.Log("enter");
				_canSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.TryGetComponent<Item>(out Item item))
			{Debug.Log("Exit");
				_canSpawn = true;

				StartCoroutine(ItemSpawn());
			}
		}

		#endregion
	}
}
