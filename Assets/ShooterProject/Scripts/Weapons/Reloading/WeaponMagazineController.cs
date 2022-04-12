using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Weapons.Reloading
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class WeaponMagazineController : MonoBehaviour
	{
		#region Fields

		private AmmoMagazine _attachedMagazine;
		private XRSocketInteractor _socketInteractor;

		#endregion

		#region Properties

		public bool MagazineAttached => _attachedMagazine != null;
		public bool HasAmmo => MagazineAttached && _attachedMagazine.HasAmmo;

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			_socketInteractor.selectEntered.AddListener(OnObjectAttached);
			_socketInteractor.selectExited.AddListener(OnObjectDetached);
		}

		private void OnDisable()
		{
			_socketInteractor.selectEntered.RemoveListener(OnObjectAttached);
			_socketInteractor.selectExited.RemoveListener(OnObjectDetached);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Уменьшает кол-во патронов в прикрепленном магазине.
		/// </summary>
		public void DecreaseAmmoCount()
		{
			if (MagazineAttached)
				_attachedMagazine.DecreaseAmmoCount();
		}

		#endregion

		#region Private Methods

		private void OnObjectAttached(SelectEnterEventArgs selectEnterEventArg)
		{
			var interactable = selectEnterEventArg.interactable.gameObject;
			if (interactable.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				_attachedMagazine = magazine;
			}
		}

		private void OnObjectDetached(SelectExitEventArgs selectExitEventArg)
		{
			if (_attachedMagazine != null)
			{
				_attachedMagazine = null;
			}
		}

		#endregion

	}
}
