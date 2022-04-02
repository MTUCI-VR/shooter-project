using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Weapons
{
	[Serializable]
	public struct WeaponParts
	{
		public Transform BulletSpawnPoint;
		public AudioSource WeaponAudioSource;
	}
}
