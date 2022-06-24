using UnityEngine;
using ShooterProject.Scripts.Waves;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.GameStatistics
{
	public class GameStatsCounter : MonoBehaviour
	{
		#region Static Fields

		public static GameStatsCounter instance;

        #endregion

		#region Life Cycle

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnEnable()
		{
			AddListeners();
		}
		private void OnDisable()
		{
			RemoveListeners();
		}

		#endregion

		#region Private Methods

		private void AddListeners()
		{
			//WavesProvider.Instance.OnWavesEnded += OnWavesEnded;
			//Player.instance.PlayerHealth.OnDied += OnPlayerDied;
		}

		private void RemoveListeners()
		{
			//WavesProvider.Instance.OnWavesEnded -= OnWavesEnded;
			//Player.instance.PlayerHealth.OnDied -= OnPlayerDied;
		}

        private void OnWavesEnded()
        {
            Counting();
			//GameStatsSceneLoading
        }

		private void OnPlayerDied(Health playerHealth)
		{
			Counting();
			//GameStatsSceneLoading
		}

		private void Counting()
		{
			Debug.Log($"{WavesProvider.Instance.CurrentWave} Волн пройдено");
			// GameStats.completeWaveCount = WavesProvider.Instance.CurrentWave;
			// GameStats.deadEnemyCount += WavesProvider.Instance.waveEnemiesObserver.DeadEnemyCount;
		}

		#endregion
	}
}
