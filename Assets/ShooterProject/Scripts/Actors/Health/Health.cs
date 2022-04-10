using UnityEngine;
using System;

namespace ShooterProject.Scripts.Actors
{
	public class Health : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private float maxHealth;

		private float _health;

		#endregion

		#region Properties

		public float CurrentHealth => _health;

		#endregion

		#region Events

		public event Action OnHpZeroed;
		public event Action OnHit;

		#endregion

		#region LifeCycle Methods

		private void Start()
		{
			_health = maxHealth;
			OnHit?.Invoke();
		}

		#endregion

		#region Public Methods

		public void TakeHit(float damage)
		{
			if (_health > damage)
			{
				_health -= damage;
				OnHit?.Invoke();
			}
			else
			{
				_health = 0;
				OnHpZeroed?.Invoke();
			}
			
		}

		#endregion
	}
}
