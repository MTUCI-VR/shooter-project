using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShooterProject.Scripts.Spawner;
using System;

namespace ShooterProject.Scripts.Waves
{
	public class WavesProvider : MonoBehaviour
	{
		#region Static Fields

		public static WavesProvider Instance;

		#endregion

		#region Fields

		[SerializeField]
		private List<EnemySpawner> spawners;

		[SerializeField]
		private List<WaveParams> waves;

		private List<EnemySpawner> _activeSpawners = new List<EnemySpawner>();
		private WaveEnemiesObserver _waveEnemiesObserver = new WaveEnemiesObserver();

		#endregion

		#region Events

		/// <summary>
		/// Вызывается при начале волны, передает номер новой волны
		/// </summary>
		public event Action<int> OnWaveStarted;
		/// <summary>
		/// Выщывается при окончании волны, передает кол-во секунд до начала новой волны
		/// </summary>
		public event Action<float> OnWavePreparationStarted;

		public event Action OnWavesEnded;

		#endregion

		#region Properties

		public int CurrentWave { get; private set; }
		private WaveParams currentWaveParams => waves[CurrentWave];

		#endregion

		#region LifeCycle

		void Awake()
		{
			SingletonInitialization();
			spawners.ForEach(e => _activeSpawners.Add(e));
		}
		private void OnEnable()
		{
			foreach (var spawner in spawners)
				spawner.OnActivationChanged += OnSpawnerActivationChanged;
		}
		private void Start()
		{
			if (spawners.Count == 0)
			{
#if UNITY_EDITOR
				Debug.LogError($"Warning: At least one added {nameof(EnemySpawner)} is required");
#endif
			}
			else
			{
				StartCoroutine(WaveCoroutine());
			}
		}
		private void OnDisable()
		{
			foreach (var spawner in spawners)
				spawner.OnActivationChanged -= OnSpawnerActivationChanged;
			StopCoroutine(WaveCoroutine());
		}

		#endregion

		#region Private Methods

		private IEnumerator WaveCoroutine()
		{
			while(CurrentWave < waves.Count)
			{
				OnWavePreparationStarted?.Invoke(currentWaveParams.WavePreparationTime);
				yield return new WaitForSeconds(currentWaveParams.WavePreparationTime);
				OnWaveStarted?.Invoke(CurrentWave + 1); //Добавляем 1, так как передаем НОМЕР волны

				_waveEnemiesObserver.Setup(currentWaveParams.EnemiesCount);
				SetupSpawners();

				var spawnedEnemies = 0;

				while(spawnedEnemies < currentWaveParams.EnemiesCount)
				{
					if(_activeSpawners.Count > 0)
					{
						var newEnemy = _activeSpawners[0].SpawnEnemy();
						_waveEnemiesObserver.AddTarget(newEnemy);

						spawnedEnemies++;
					}
					yield return new WaitForEndOfFrame();
				}

				while (!_waveEnemiesObserver.WaweKilled)
					yield return new WaitForEndOfFrame();

				CurrentWave++;
			}
			OnWavesEnded?.Invoke();
		}
		private void SingletonInitialization()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
#if UNITY_EDITOR
				Debug.LogWarning($"Warning: Object of type {nameof(WavesProvider)} already exists");
#endif
			}
		}
		private void OnSpawnerActivationChanged(EnemySpawner spawner, bool activationState)
		{
			if (activationState)
				_activeSpawners.Add(spawner);
			else
				_activeSpawners.Remove(spawner);
		}
		private void SetupSpawners()
		{
			foreach (var spawner in spawners)
			{
				spawner.ChangeObjects(currentWaveParams.AvailableEnemies.ToArray());
			}
		}

		#endregion

	}
}
