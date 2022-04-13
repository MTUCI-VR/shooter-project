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
		public event Action OnHpChanged;

		#endregion

		#region LifeCycle Methods

		private void Start()
		{
			_health = maxHealth;
			OnHpChanged?.Invoke();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Наносит урон объекту
		/// </summary>
		/// <param name="damage">Кол-во отнимаемого хп</param>
		public void TakeHit(float damage)
		{
			if (_health > damage)
			{
				_health -= damage;
				OnHpChanged?.Invoke();
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
