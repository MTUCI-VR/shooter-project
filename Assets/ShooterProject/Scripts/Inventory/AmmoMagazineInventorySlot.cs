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

        private List<AmmoMagazine> _magazines = new List<AmmoMagazine>();

        private XRSocketInteractor _socketInteractor;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _socketInteractor = GetComponent<XRSocketInteractor>();
        }

        private void OnEnable()
        {
            _socketInteractor.hoverEntered.AddListener(OnHoverEnter);
            _socketInteractor.hoverExited.AddListener(OnHoverExit);
            _socketInteractor.selectEntered.AddListener(OnSelectEntered);
            _socketInteractor.selectExited.AddListener(OnSelectExit);
        }
        private void OnDisable()
        {
            _socketInteractor.hoverEntered.RemoveListener(OnHoverEnter);
            _socketInteractor.hoverExited.RemoveListener(OnHoverExit);
            _socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
            _socketInteractor.selectExited.RemoveListener(OnSelectExit);
        }

        #endregion

        #region Private Methods

        private void OnHoverEnter(HoverEnterEventArgs hoverEnterEventArgs)
        {
            if (hoverEnterEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine) && !magazine.GetComponent<XRGrabInteractable>().isSelected)
            {
                PutInInventorySlot(magazine);
            }
        }

        private void OnHoverExit(HoverExitEventArgs hoverExitEventArgs)
        {
            if (hoverExitEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine) && !magazine.GetComponent<XRGrabInteractable>().isSelected)
            {
                PutInInventorySlot(magazine);
            }
        }

        private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (selectEnterEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
            {
                if (!_magazines.Contains(magazine))
                    _magazines.Add(magazine);

                Counting();
            }
        }

        private void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
        {
            if (selectExitEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
            {
                TakeFromInventorySlot(magazine);
            }
        }

        private void PutInInventorySlot(AmmoMagazine magazine)
        {
            if (_socketInteractor.hasSelection)
            {
                magazine.gameObject.SetActive(false);
            }

            if (!_magazines.Contains(magazine))
                    _magazines.Add(magazine);

            Counting();
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

            int pistolAmmoCount = 0;
            int rifleAmmoCount = 0;

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
					default:
						break;
				}
            }
        }

        #endregion
    }
}

