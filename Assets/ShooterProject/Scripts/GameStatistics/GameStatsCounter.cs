using UnityEngine;
using ShooterProject.Scripts.Waves;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;
using ShooterProject.Scripts.GameManager;

namespace ShooterProject.Scripts.GameStatistics
{
	public class GameStatsCounter : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private string _gameStatsSceneName;

		#endregion

		#region Life Cycles

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
			WavesProvider.Instance.OnWavesEnded += OnWavesEnded;
			Player.Instance.PlayerHealth.OnDied += OnPlayerDied;
		}

		private void RemoveListeners()
		{
			WavesProvider.Instance.OnWavesEnded -= OnWavesEnded;
			Player.Instance.PlayerHealth.OnDied -= OnPlayerDied;
		}

        private void OnWavesEnded()
        {
            Counting();
			StartCoroutine(SceneLoader.LoadScene(_gameStatsSceneName));
        }

		private void OnPlayerDied(Health playerHealth)
		{
			Counting();
			StartCoroutine(SceneLoader.LoadScene(_gameStatsSceneName));
		}

		private void Counting()
		{
			GameStats.CompleteWaveCount = WavesProvider.Instance.CurrentWave;
			GameStats.DeadEnemyCount = WavesProvider.Instance.waveEnemiesObserver.TotalDeadEnemyCount;
		}

		#endregion
	}
}
