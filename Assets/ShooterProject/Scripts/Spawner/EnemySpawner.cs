using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Spawner
{
	public class EnemySpawner : GeneralSpawner
	{
		#region Fields
		private IEnumerator _spawnCoroutine;

		#endregion

		#region  LifeCycle

		private void Start()
		{
			_spawnCoroutine = Spawn();
			StartCoroutine(_spawnCoroutine);
		}

		#endregion

		#region Private Methods
		private void OnTriggerEnter(Collider collider)
		{

			if (collider.tag == "Player")
			{
				StopCoroutine(_spawnCoroutine);
			}
		}
		private void OnTriggerExit(Collider collider)
		{

			if (collider.tag == "Player")
			{
				StartCoroutine(_spawnCoroutine);
			}
		}

		#endregion
	}
}
