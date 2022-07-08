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
			InventoryInfo.OnAmmoCountChanged += Print;
			Print();
		}
		private void OnDisable()
		{
			weaponMagazineController.OnAttachedMagazine -= GettingAttachedMagazine;
			InventoryInfo.OnAmmoCountChanged -= Print;
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
			weaponInterfaceTexts.ForEach(weaponInterface => weaponInterface.text = $"{AttachedMagazineAmmoCount}/{InventoryInfo.AmmoCount}");
		}

		#endregion
	}
}
