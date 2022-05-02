using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShooterProject.Scripts.PlayerScripts;
namespace ShooterProject.Scripts.Spawner
{
	public class EnemySpawner : GeneralSpawner, General.IObservable<EnemySpawnerActivationData>
	{
		#region Fields

		private bool _canSpawn = true;

		private List<General.IObserver<EnemySpawnerActivationData>> _observers = new List<General.IObserver<EnemySpawnerActivationData>>();
		#endregion

		#region Properties

		public bool CanSpawn
		{
			get
			{
				return _canSpawn;
			}
			private set
			{
				Notify(new EnemySpawnerActivationData(this,value));
				_canSpawn = value;
			}
		}

		#endregion

		#region Public Methods

		public void Spawn(float spawnDelayInSeconds)
		{
			if (_canSpawn)
				StartCoroutine(SpawnCoroutine(spawnDelayInSeconds));
		}

		#endregion

		#region Private Methods

		private IEnumerator SpawnCoroutine(float spawnDelayInSeconds)
		{
			Spawn();
			CanSpawn = false;
			yield return new WaitForSeconds(spawnDelayInSeconds);
			CanSpawn = true;
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				CanSpawn = false;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.TryGetComponent<Player>(out Player player))
			{
				CanSpawn = true;
			}
		}

		public void Subscribe(General.IObserver<EnemySpawnerActivationData> observer)
		{
			_observers.Add(observer);
		}

		public void Unsubscribe(General.IObserver<EnemySpawnerActivationData> observer)
		{
			if(_observers.Contains(observer))
			_observers.Remove(observer);
		}

		public void Notify(EnemySpawnerActivationData data)
		{
			foreach (var observer in _observers)
				observer.Notify(data);
		}

		#endregion
	}
}
