using TMPro;
using UnityEngine;
using System.Collections.Generic;
using ShooterProject.Scripts.Weapons.Reloading;
using ShooterProject.Scripts.Inventory;

namespace ShooterProject.Scripts.GameInterface
{
	public class WeaponInterface : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<TextMeshProUGUI> weaponInterfaceTexts;

		[SerializeField]
		private WeaponMagazineController weaponMagazineController;

		private AmmoMagazine _attachedMagazine;

		#endregion

		#region Properties

		private int AttachedMagazineAmmoCount => weaponMagazineController.AttachedMagazine ? weaponMagazineController.AttachedMagazine.AmmoCount : 0;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			weaponMagazineController.OnAttachedMagazine += OnAttachedMagazine;
			weaponMagazineController.OnDetachedMagazine += OnDetachedMagazine;
			InventoryInfo.OnPistolAmmoCountChanged += Print;
			InventoryInfo.OnRifleAmmoCountChanged += Print;

			OnAttachedMagazine();
		}
		private void OnDisable()
		{
			weaponMagazineController.OnAttachedMagazine -= OnAttachedMagazine;
			weaponMagazineController.OnDetachedMagazine -= OnDetachedMagazine;
			InventoryInfo.OnPistolAmmoCountChanged -= Print;
			InventoryInfo.OnRifleAmmoCountChanged -= Print;
		}

		#endregion

		#region Private Methods

		private void OnAttachedMagazine()
		{
			if (weaponMagazineController.AttachedMagazine != null)
			{
				_attachedMagazine = weaponMagazineController.AttachedMagazine;
				_attachedMagazine.OnAmmoCountChanged += Print;
			}

			Print();
		}
		private void OnDetachedMagazine()
		{
			_attachedMagazine.OnAmmoCountChanged -= Print;

			Print();
		}

		private void Print()
		{
			switch (weaponMagazineController.AmmoType)
			{
				case AmmoType.PistolAmmo:
					weaponInterfaceTexts.ForEach(weaponInterface => weaponInterface.text = $"{AttachedMagazineAmmoCount}/{InventoryInfo.PistolAmmoCount}");
					break;
				case AmmoType.RifleAmmo:
					weaponInterfaceTexts.ForEach(weaponInterface => weaponInterface.text = $"{AttachedMagazineAmmoCount}/{InventoryInfo.RifleAmmoCount}");
					break;
			}

		}

		#endregion
	}
}
