using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace ShooterProject.Scripts.Items.Weapons
{
	[Serializable]
	public struct WeaponParams
	{
		[SerializeField]
		public GameObject BulletPrefab;

		[SerializeField]
		public InputActionProperty ActivateAction;

		[SerializeField]
		public float ShootingDelaySeconds;

		[SerializeField]
		public float Damage;

		[SerializeField]
		public bool CanFireBursts;
	}
}
