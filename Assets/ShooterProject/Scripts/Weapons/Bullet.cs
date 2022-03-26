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

		#endregion

		#region LifeCycle Methods
		void Start()
		{
			var rigidBodyComponent = GetComponent<Rigidbody>();

			rigidBodyComponent.AddForce(transform.forward * _startImpulse, ForceMode.Impulse);

			Destroy(gameObject, 5);
		}
		#endregion

		#region Private Methods

		private void OnCollisionEnter(Collision collision)
		{
			Destroy(gameObject);
		}

		#endregion
	}
}
