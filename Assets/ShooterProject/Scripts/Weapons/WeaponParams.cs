using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace ShooterProject.Scripts.Weapons
{
	[Serializable]
	public struct WeaponParams
	{

		[SerializeField]
		public InputActionProperty ActivateAction;

		[Header("Shooting Params")]
		[SerializeField]
		public float ShootingDelaySeconds;

		[SerializeField]
		public float ShootingDistance;

		[SerializeField]
		public float Damage;

		[SerializeField]
		public bool CanFireBursts;

	}
}
