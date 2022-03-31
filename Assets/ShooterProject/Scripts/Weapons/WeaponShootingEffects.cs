using System.Collections.Generic;
using UnityEngine;
namespace ShooterProject.Scripts.Weapons
{
	[System.Serializable]
	public struct WeaponShootingEffects
	{
		[SerializeField]
		public AudioClip ShootignSound;

		[SerializeField]
		public ParticleSystem Particles;

		[Header("Impacts")]

		[SerializeField]
		public GameObject ImpactPrefab;

		[SerializeField]
		public int MaxImpacts;

		[SerializeField]
		public List<string> ImpactIgnoreTags;
	}
}
