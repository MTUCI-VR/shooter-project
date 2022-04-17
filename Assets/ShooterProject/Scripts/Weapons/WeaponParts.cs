using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Weapons.Reloading;

namespace ShooterProject.Scripts.Weapons
{
	[Serializable]
	public struct WeaponParts
	{
		public Transform BulletSpawnPoint;
		public AudioSource WeaponAudioSource;
		public WeaponMagazineController ReloadController;
	}
}
