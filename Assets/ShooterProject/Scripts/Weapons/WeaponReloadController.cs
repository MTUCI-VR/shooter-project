using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Items.Weapons
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class WeaponReloadController : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private Weapon _attachedWeapon;

		private AmmoMagazine _attachedMagazine;

		#endregion

		#region LifeCycleMethods
		private void Start()
		{
		}
		#endregion

		#region Public Methods

		public void MagazineAttached(SelectEnterEventArgs arg0)
		{
			var interactable = arg0.interactable.gameObject;
			if (interactable.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				_attachedMagazine = magazine;
				_attachedWeapon.ChangeAmmoMagazine(magazine);
			}
#if UNITY_EDITOR
			else
			{
				Debug.LogError($"На объекте {interactable} отсутствует компонент AmmoMagazine");
			}
		}
#endif
		#endregion

	}
}
