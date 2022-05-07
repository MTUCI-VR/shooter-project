using TMPro;
using UnityEngine;
using ShooterProject.Scripts.Weapons.Reloading;
using ShooterProject.Scripts.Inventory;

namespace ShooterProject.Scripts.GameInterface
{
	public class WeaponInterface : MonoBehaviour
	{
		#region Fields

		private TextMeshProUGUI _weaponInterfaceText;

		private WeaponMagazineController _weaponMagazineController;

		private AmmoMagazine _attachedMagazine;

		private int _attachedMagazineAmmoCount;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_weaponInterfaceText = GetComponentInChildren<TextMeshProUGUI>();
			_weaponMagazineController = GetComponentInParent<WeaponMagazineController>();
		}

		private void OnEnable()
		{
			_weaponMagazineController.OnAttachedMagazine += GettingAttachedMagazine;
			InventoryInfo.OnAmmoCountChanged += Print;
			Print();
		}
		private void OnDisable()
		{
			_weaponMagazineController.OnAttachedMagazine -= GettingAttachedMagazine;
			InventoryInfo.OnAmmoCountChanged -= Print;
		}

		#endregion

		#region Private Methods

		private void GettingAttachedMagazine()
		{
			if (_weaponMagazineController.AttachedMagazine != null)
			{
				_attachedMagazine = _weaponMagazineController.AttachedMagazine;
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
			if (_attachedMagazine != null)
			{
				_attachedMagazineAmmoCount = _attachedMagazine.AmmoCount;
			}
			else
			{
				_attachedMagazineAmmoCount = 0;
			}

			_weaponInterfaceText.text = $"{_attachedMagazineAmmoCount}/{InventoryInfo.AmmoCount}";
		}

		#endregion
	}
}
