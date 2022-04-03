using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Weapons.Reloading
{
	[RequireComponent(typeof(XRSocketInteractor),typeof(Weapon))]
	public class WeaponReloadController : MonoBehaviour
	{
		#region Fields

		private Weapon _attachedWeapon;
		private AmmoMagazine _attachedMagazine;
		private XRSocketInteractor _socketInteractor;

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			_attachedWeapon = GetComponent<Weapon>();
			_socketInteractor = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			_socketInteractor.selectEntered.AddListener(ObjectAttached);
			_socketInteractor.selectExited.AddListener(ObjectDetached);
		}

		private void OnDisable()
		{
			_socketInteractor.selectEntered.RemoveListener(ObjectAttached);
			_socketInteractor.selectExited.RemoveListener(ObjectDetached);
		}

		#endregion

		#region Private Methods

		private void ObjectAttached(SelectEnterEventArgs arg0)
		{
			var interactable = arg0.interactable.gameObject;
			if (interactable.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				_attachedMagazine = magazine;
				_attachedWeapon.ChangeAmmoMagazine(magazine);
			}
		}

		private void ObjectDetached(SelectExitEventArgs arg0)
		{
			if(_attachedMagazine != null)
			{
				_attachedWeapon.ChangeAmmoMagazine(null);
			}
		}

		#endregion

	}
}
