using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Items.Weapons.Reloading
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class WeaponReloadController : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private Weapon _attachedWeapon;

		private AmmoMagazine _attachedMagazine;

		#endregion

		#region Public Methods

		/// <summary>
		/// Обработка события присоединения магазина
		/// </summary>
		/// <param name="arg0">Данные события</param>
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
#endif
		}

		/// <summary>
		/// Обработка события отсоединения магазина
		/// </summary>
		/// <param name="arg0">Данные события</param>
		public void MagazineDetached(SelectExitEventArgs arg0)
		{
			if(_attachedMagazine != null)
			{
				_attachedWeapon.ChangeAmmoMagazine(null);
			}
		}
		#endregion

	}
}
