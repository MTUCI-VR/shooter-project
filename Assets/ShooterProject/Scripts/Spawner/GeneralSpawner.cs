using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Spawner
{
	public class GeneralSpawner : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		protected SpawnObjectParams[] objectsForSpawn;

		[SerializeField]
		private int spawnDelayInSeconds;

		[SerializeField]
		private string blockSpawnTag;

		protected GameObjectsPool[] _impactPools;

		private List<int> _spawnWeights = new List<int>();

		private int _spawnWeightsSum = 0;

		private IEnumerator _spawnCoroutine;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_spawnCoroutine = Spawn();

			_impactPools = new GameObjectsPool[objectsForSpawn.Length];

			for (int i = 0; i < objectsForSpawn.Length; i++)
			{
				_impactPools[i] = new GameObjectsPool(objectsForSpawn[i].maxImpacts,
					false,
					false,
					objectsForSpawn[i].gameObject,
					null);
			}

			SpawnWeightsSort();
		}

		private void Start()
        {
            StartCoroutine(_spawnCoroutine);
        }

		#endregion

		#region Private Methods

		private void SpawnWeightsSort()
		{
			for (int i = 0; i < objectsForSpawn.Length; i++)
			{
				if (!_spawnWeights.Contains(objectsForSpawn[i].SpawnWeight))
				{
					_spawnWeightsSum += objectsForSpawn[i].SpawnWeight;
					_spawnWeights.Add(objectsForSpawn[i].SpawnWeight);
				}
			}

			_spawnWeights.Sort();
		}

		private int GetSpawnObjectIndex()
		{
			int currentSpawnWeight = 0;

			int random = Random.Range(0, _spawnWeightsSum);

			for (int i = 0; i < _spawnWeights.Count; i++)
			{
				if (random < _spawnWeights[i])
				{
					currentSpawnWeight = _spawnWeights[i];
					break;
				}
				else
					random -= _spawnWeights[i];
			}

			List<int> indices = new List<int>();

			for (int i = 0; i < objectsForSpawn.Length; i++)
				if (objectsForSpawn[i].SpawnWeight == currentSpawnWeight)
					indices.Add(i);

			return indices[Random.Range(0, indices.Count)];
		}

		protected IEnumerator Spawn()
		{
			while (_impactPools != null)
			{
				yield return new WaitForSeconds(spawnDelayInSeconds);

				int spawnObjectIndex = GetSpawnObjectIndex();

				GameObjectsPool impact = _impactPools[spawnObjectIndex];
				GameObject objectForSpawn = objectsForSpawn[spawnObjectIndex].gameObject;

				if (impact.TryGetFreeElement(out objectForSpawn, false))
				{
					objectForSpawn.transform.position = transform.position;
					objectForSpawn.transform.rotation = transform.rotation;
				}
			}
		}

		private void OnTriggerEnter(Collider collider) {
            
            if (collider.tag == blockSpawnTag)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }
        private void OnTriggerExit(Collider collider) {

            if (collider.tag == blockSpawnTag)
            {
                StartCoroutine(_spawnCoroutine);
            }
        }

		#endregion
	}

}
