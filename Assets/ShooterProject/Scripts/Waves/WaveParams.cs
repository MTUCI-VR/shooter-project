using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.Waves
{
	[Serializable]
	public class WaveParams
	{
		#region Fields

		public List<SpawnObjectParams> AvailableEnemies;
		public int EnemiesCount;
		public float WavePreparationTime;

		#endregion
		public void DeleteInvalidObjects()
		{
			foreach (var spawnObjectParams in AvailableEnemies)
			{
				if(!spawnObjectParams.TryGetComponent<Health>(out var enemyHealth))
				{
					AvailableEnemies.Remove(spawnObjectParams);
#if UNITY_EDITOR
					Debug.Log($"Warning: object {spawnObjectParams.gameObject.name} does not have a health component and will not be used");;
#endif
				}
			}
		}
	}
}
