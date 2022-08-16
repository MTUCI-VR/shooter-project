using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Weapons.Reloading;

namespace ShooterProject.Scripts.Inventory
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class AmmoMagazineInventorySlot : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private int maxCount;

		private List<AmmoMagazine> _magazines = new List<AmmoMagazine>();

		private XRSocketInteractor _socketInteractor;

		#endregion

		#region Properties

		public bool CanPutInInventory => _magazines.Count < maxCount;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			_socketInteractor.hoverEntered.AddListener(OnHoverEnter);
			_socketInteractor.selectEntered.AddListener(OnSelectEntered);
			_socketInteractor.selectExited.AddListener(OnSelectExit);
		}
		private void OnDisable()
		{
			_socketInteractor.hoverEntered.RemoveListener(OnHoverEnter);
			_socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
			_socketInteractor.selectExited.RemoveListener(OnSelectExit);
		}

		#endregion

		#region Private Methods

		private void OnHoverEnter(HoverEnterEventArgs hoverEnterEventArgs)
		{
			if (hoverEnterEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine) && _socketInteractor.hasSelection)
			{
				magazine.GetComponent<XRGrabInteractable>().selectExited.AddListener(OnMagazineGrabSelectExit);
			}
		}

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			if (selectEnterEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				if (!_magazines.Contains(magazine))
				{
					_magazines.Add(magazine);
					Counting();
				}
			}
		}

		private void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
		{
			if (selectExitEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				TakeFromInventorySlot(magazine);
			}
		}

		private void OnMagazineGrabSelectExit(SelectExitEventArgs selectExitEventArgs)
		{
			selectExitEventArgs.interactableObject.selectExited.RemoveListener(OnMagazineGrabSelectExit);

			if (_socketInteractor.IsHovering(selectExitEventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>()))
				PutInInventorySlot(selectExitEventArgs.interactableObject.transform.GetComponent<AmmoMagazine>());
		}

		private void PutInInventorySlot(AmmoMagazine magazine)
		{
			if (!CanPutInInventory) return;

			if (_socketInteractor.hasSelection)
			{
				magazine.gameObject.SetActive(false);

				if (!_magazines.Contains(magazine))
				{
					_magazines.Add(magazine);
					Counting();
				}
			}
		}

		private void TakeFromInventorySlot(AmmoMagazine magazine)
		{
			_magazines.Remove(magazine);


			if (_magazines.Count > 0)
			{
				GameObject newMagazine = _magazines[_magazines.Count - 1].gameObject;

				newMagazine.transform.position = _socketInteractor.attachTransform.position;
				newMagazine.transform.rotation = _socketInteractor.attachTransform.rotation;

				newMagazine.gameObject.SetActive(true);
			}

			Counting();
		}

		private void Counting()
		{
			if (_magazines.Count == 0)
			{
				InventoryInfo.PistolAmmoCount = 0;
				InventoryInfo.RifleAmmoCount = 0;

				return;
			}

			var pistolAmmoCount = 0;
			var rifleAmmoCount = 0;

			foreach (AmmoMagazine magazine in _magazines)
			{
				switch (magazine.AmmoType)
				{
					case AmmoType.PistolAmmo:
						pistolAmmoCount += magazine.AmmoCount;
						InventoryInfo.PistolAmmoCount = pistolAmmoCount;
						break;
					case AmmoType.RifleAmmo:
						rifleAmmoCount += magazine.AmmoCount;
						InventoryInfo.RifleAmmoCount = rifleAmmoCount;
						break;
				}
			}
		}

		#endregion
	}
}

