using UnityEngine;
namespace ShooterProject.Scripts.Items.Weapons
{
	[System.Serializable]
	public struct WeaponShootingEffects
	{
		[SerializeField]
		public AudioClip ShootignSound;

		[SerializeField]
		public ParticleSystem Particles;
	}
}
