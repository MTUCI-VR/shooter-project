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

		private int AttachedMagazineAmmoCount => _attachedMagazine ? _attachedMagazine.AmmoCount : 0;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			weaponMagazineController.OnAttachedMagazine += GettingAttachedMagazine;
			weaponMagazineController.OnDetachedMagazine += Print;
			InventoryInfo.OnPistolAmmoCountChanged += Print;
			InventoryInfo.OnRifleAmmoCountChanged += Print;
			Print();
		}
		private void OnDisable()
		{
			weaponMagazineController.OnAttachedMagazine -= GettingAttachedMagazine;
			weaponMagazineController.OnDetachedMagazine -= Print;
			InventoryInfo.OnPistolAmmoCountChanged -= Print;
			InventoryInfo.OnRifleAmmoCountChanged -= Print;
		}

		#endregion

		#region Private Methods

		private void GettingAttachedMagazine()
		{
			if (weaponMagazineController.AttachedMagazine != null)
			{
				_attachedMagazine = weaponMagazineController.AttachedMagazine;
				_attachedMagazine.OnAmmoCountChanged += Print;
				Print();
			}
			else
			{
				_attachedMagazine.OnAmmoCountChanged -= Print;
				_attachedMagazine = null;
				Print();
			}
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
				default:
				break;
			}

		}

		#endregion
	}
}
