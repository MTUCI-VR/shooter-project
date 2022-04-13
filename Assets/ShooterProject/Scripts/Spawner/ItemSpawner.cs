using UnityEngine;
using System.Collections;

namespace ShooterProject.Scripts.Spawner
{
    public class ItemSpawner : GeneralSpawner
    {
        #region Fields

		[SerializeField]
		private int spawnDelayInSeconds;

		[SerializeField]
		private string itemTag;

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
			if (collider.tag == itemTag)
			{
				_canSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.tag == itemTag)
			{
				StartCoroutine(ItemSpawn());
				
				_canSpawn = true;
			}
		}

		#endregion
    }
}