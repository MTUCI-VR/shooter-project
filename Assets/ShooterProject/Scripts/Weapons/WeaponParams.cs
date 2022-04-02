using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace ShooterProject.Scripts.Weapons
{
	[Serializable]
	public struct WeaponParams
	{
		[Header("Shooting Params")]
		public float ShootingDelaySeconds;
		public float ShootingDistance;
		public float Damage;
		public bool CanFireBursts;
	}
}
