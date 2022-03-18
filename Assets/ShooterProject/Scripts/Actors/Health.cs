using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShooterProject.Scripts.Actors
{
	public class Health : MonoBehaviour
	{
		#region Private fields
		[SerializeField]
		private float _maxHealth;

		[SerializeField]
		private float _healthPoints;
		#endregion
		#region Properties
		public float HealthPoints => _healthPoints;
		#endregion

		#region Events
		public event Action<float> OnHPChanged;
		public event Action OnHPZeroing;
		#endregion

		#region Public methods

		/// <summary> 
		/// Добавляет очки здоровья
		/// </summary> 
		/// <param name="hp">Кол-во добавочных очков здоровья</param> 
		public void AddHealth(float hp)
		{
			_healthPoints += hp;

			if (_healthPoints > _maxHealth)
			{
				_healthPoints = _maxHealth;
			}
			OnHPChanged?.Invoke(_healthPoints);
		}

		/// <summary> 
		/// Отнимает очки здоровья
		/// </summary> 
		/// <param name="damage">Кол-во отнимаемых очков здоровья</param> 
		public void TakeHit(float damage)
		{
			_healthPoints -= damage;

			if (_healthPoints <= 0)
			{
				_healthPoints = 0;
				OnHPZeroing?.Invoke();
			}
			OnHPChanged?.Invoke(_healthPoints);
		}
		#endregion

	}
}
