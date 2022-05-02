using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShooterProject.Scripts.Spawner;

namespace ShooterProject.Scripts.Waves
{
	public class WavesProvider : MonoBehaviour, General.IObserver<EnemySpawnerActivationData>
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

		#endregion

		#region Properties

		public int CurrentWave { get; private set; }

		#endregion

		#region LifeCycle

		void Awake()
		{
			SingletonInitialization();
		}
		private void Start()
		{
			StartCoroutine(WorkingCoroutine());
		}
		private void OnEnable()
		{
			foreach (var spawner in spawners)
				spawner.Subscribe(this);
		}
		private void OnDisable()
		{
			foreach (var spawner in spawners)
				spawner.Unsubscribe(this);
			StopCoroutine(WorkingCoroutine());
		}

		#endregion

		#region Private Methods

		private IEnumerator WorkingCoroutine()
		{
			while(CurrentWave < waves.Count)
			{

			}
			yield break;
		}
		private void SingletonInitialization()
		{
			if (!Instance)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
#if DEBUG
				Debug.LogWarning($"Error: Object of type {nameof(WavesProvider)} already exists");
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

		public void Notify(EnemySpawnerActivationData data)
		{
			if(data.CanSpawn)
			{
				spawners.Add(data.EnemySpawner);
			}
			else
			{
				spawners.Remove(data.EnemySpawner);
			}
		}

		#endregion

	}
}
