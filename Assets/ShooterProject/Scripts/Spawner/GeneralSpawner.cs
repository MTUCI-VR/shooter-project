using UnityEngine;
using System.Collections.Generic;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.Spawner
{
	public class GeneralSpawner : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private SpawnObjectParams[] objectsForSpawn;

		[SerializeField]
		private bool poolAutoExpand;

		private GameObjectsPool[] _objectsForSpawnPools;

		private List<int> _spawnWeights = new List<int>();

		private int _spawnWeightsSum = 0;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_objectsForSpawnPools = new GameObjectsPool[objectsForSpawn.Length];

			for (int i = 0; i < objectsForSpawn.Length; i++)
			{
				_objectsForSpawnPools[i] = new GameObjectsPool(objectsForSpawn[i].maxCount,
					poolAutoExpand,
					false,
					objectsForSpawn[i].gameObject,
					null);
			}

			SortSpawnWeights();
		}

		#endregion

		#region Public Methods

		public void ChangeObjects(SpawnObjectParams[] objectsForSpawn)
		{
			this.objectsForSpawn = objectsForSpawn;
		}

		#endregion

		#region Private Methods

		private void SortSpawnWeights()
		{
			foreach (var objectForSpawn in objectsForSpawn)
			{
				if (!_spawnWeights.Contains(objectForSpawn.SpawnWeight))
				{
					_spawnWeightsSum += objectForSpawn.SpawnWeight;
					_spawnWeights.Add(objectForSpawn.SpawnWeight);
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
				{
					random -= _spawnWeights[i];
				}
			}

			List<int> indices = new List<int>();

			for (int i = 0; i < objectsForSpawn.Length; i++)
			{
				if (objectsForSpawn[i].SpawnWeight == currentSpawnWeight)
				{
					indices.Add(i);
				}
			}

			return indices[Random.Range(0, indices.Count)];
		}

		#endregion

		#region Protected Methods 

		protected GameObject Spawn()
		{
			int spawnObjectIndex = GetSpawnObjectIndex();

			GameObjectsPool objectForSpawnPool = _objectsForSpawnPools[spawnObjectIndex];
			GameObject objectForSpawn = objectsForSpawn[spawnObjectIndex].gameObject;

			if (objectForSpawnPool.TryGetFreeElement(out objectForSpawn, false))
			{
				objectForSpawn.transform.position = transform.position;
				objectForSpawn.transform.rotation = transform.rotation;
				return objectForSpawn;
			}
			return null;
		}

		#endregion
	}

}
