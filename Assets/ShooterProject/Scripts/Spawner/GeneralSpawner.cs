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
		private int timeToNextSpawn;

		protected GameObjectsPool[] _impactPools;

		private List<int> _spawnWeights;

		private int _spawnWeightsSum = 0;

		#endregion

		#region LifeCycle

		private void Awake()
		{
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

		#endregion

		#region Private Methods

		private void SpawnWeightsSort()
		{
			_spawnWeights = new List<int>();

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

		#endregion

		#region Protected Methods
		protected IEnumerator Spawn()
		{
			while (_impactPools != null)
			{
				yield return new WaitForSeconds(timeToNextSpawn);

				int spawnObjectIndex = GetSpawnObjectIndex();

				Debug.Log(spawnObjectIndex);

				GameObjectsPool impact = _impactPools[spawnObjectIndex];
				GameObject objectForSpawn = objectsForSpawn[spawnObjectIndex].gameObject;

				if (impact.TryGetFreeElement(out objectForSpawn, true))
				{
					objectForSpawn.transform.position = transform.position;
					objectForSpawn.transform.rotation = transform.rotation;
				}
			}
		}

		#endregion
	}

}
