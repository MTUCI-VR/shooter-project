using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Weapons
{
	[Serializable]
	public struct WeaponParts
	{
		[SerializeField]
		public Transform BulletSpawner;

		[SerializeField]
		public XRGrabInteractable InteractableHandle;

		[SerializeField]
		public AudioSource WeaponAudioSource;
	}
}
