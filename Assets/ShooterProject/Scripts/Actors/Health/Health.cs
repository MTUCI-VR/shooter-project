using UnityEngine;
using System;

namespace ShooterProject.Scripts.Actors.Health
{
	public class Health : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		[Min(0)]
		private float maxHealth;

		private float _health;

		#endregion

		#region Properties

		public float MaxHealth
		{
			get
			{
				return maxHealth;
			}
			private set
			{
				maxHealth = value;
			}
		}

		public float CurrentHealth
		{
			get
			{
				return _health;
			}
			private set
			{
				_health = value;
				if (value == 0)
					OnDied?.Invoke(this);
				else
					OnChanged?.Invoke(this);
			}
		}

		#endregion

		#region Events

		public event Action<Health> OnDied;
		public event Action<Health> OnChanged;

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			CurrentHealth = maxHealth;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Наносит урон объекту
		/// </summary>
		/// <param name="damage">Кол-во отнимаемого хп</param>
		public void TakeHit(float damage)
		{
			CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
		}

		/// <summary>
		/// Пополняет здоровье объекту
		/// </summary>
		/// <param name="healthPoints">Кол-во добоваляемого хп</param>
		public void Heal(float healthPoints)
		{
			CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + healthPoints);
		}

		#endregion
	}
}
