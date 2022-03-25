using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.Items
{
	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private float _startImpulse;
		private float _damage;

		#endregion

		#region LifeCycle Methods
		void Start()
		{
			var rigidBodyComponent = GetComponent<Rigidbody>();
			rigidBodyComponent.AddForce(transform.forward * _startImpulse, ForceMode.Impulse);
			Destroy(gameObject, 5);
		}
		#endregion

		#region Public Methods
		public void SetDamage(float damage)
		{
			if (damage > 0)
				_damage = damage;
#if UNITY_EDITOR
			else
				Debug.LogError($"Ошибка {gameObject.name} невозможно установить отрицательное или нулевое значение урона {damage}");
		}
#endif
		#endregion

		#region Private Methods

		private void OnCollisionEnter(Collision collision)
		{
			Destroy(gameObject);
		}

		#endregion
	}
}
