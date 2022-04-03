using System.Collections.Generic;
using UnityEngine;
namespace ShooterProject.Scripts.Weapons
{
	[System.Serializable]
	public struct WeaponShootingEffects
	{
		public AudioClip Sound;
		public AudioClip NoAmmoSound;
		public ParticleSystem Particles;

		[Header("Impacts")]
		public GameObject ImpactPrefab;
		public int MaxImpacts;
		public List<string> ImpactIgnoreTags;
	}
}
