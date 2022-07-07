using System;
using System.Collections.Generic;
using UnityEngine;
using ShooterProject.Scripts.Actors.Health;

namespace ShooterProject.Scripts.Waves
{
	public class WaveEnemiesObserver
	{
		#region Fields

		private List<Health> _enemies = new List<Health>();
		private int _waveEnemiesCount;
		private int _deadEnemyCount;

		#endregion

		#region Properties

		public bool WaveKilled { get; private set; }

		public int TotalDeadEnemyCount { get; private set; }

		#endregion

		#region Events

		public event Action OnEnemiesDied;

		#endregion

		#region Public Methods

		public void AddTarget(GameObject enemyGameObject)
		{
			if (enemyGameObject.TryGetComponent<Health>(out var enemyHealth))
			{
				_enemies.Add(enemyHealth);
				enemyHealth.OnDied += OnEnemyDied;
			}
		}
		public void Setup(int waveEnemiesCount)
		{
			_waveEnemiesCount = waveEnemiesCount;
			_deadEnemyCount = 0;
			WaveKilled = false;
			_enemies.Clear();
		}

		#endregion

		#region Private Methods

		private void OnEnemyDied(Health enemyHealth)
		{
			_enemies.Remove(enemyHealth);
			enemyHealth.OnDied -= OnEnemyDied;

			_deadEnemyCount++;
			TotalDeadEnemyCount++;

			if (_deadEnemyCount == _waveEnemiesCount)
			{
				OnEnemiesDied?.Invoke();
				WaveKilled = true;
			}
		}

		#endregion
	}
}
