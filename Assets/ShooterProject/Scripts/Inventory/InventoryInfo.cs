using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryInfo : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private GameObject inventory;

		private XRSocketInteractor[] _snapZones;

		private static int _ammoMagazineCount;

		private static bool _hasKnife;

		#endregion

		#region Events

		public delegate void OnChange();

		public static event OnChange OnAmmoMagazineCountChanged;

		public static event OnChange OnHasKnifeChanged;

		#endregion

		#region Properties

		public static int AmmoMagazineCount
		{
			get
			{
				return _ammoMagazineCount;
			}
			private set
			{
				_ammoMagazineCount = value;
				OnAmmoMagazineCountChanged?.Invoke();
			}
		}

		public static bool HasKnife
		{
			get
			{
				return _hasKnife;
			}
			private set
			{
				_hasKnife = value;
				OnHasKnifeChanged?.Invoke();
			}
		}

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_snapZones = inventory.GetComponentsInChildren<XRSocketInteractor>();
		}
		private void Update()
		{
			Counting();
		}

		#endregion

		#region  Private Methods

		private void Counting()
		{
			AmmoMagazineCount = 0;

			foreach (var snapZone in _snapZones)
			{
				if (snapZone.tag == "AmmoMagazineSnapZone" && snapZone.hasSelection)
					AmmoMagazineCount++;

				HasKnife = snapZone.tag == "KnifeSnapZone" && snapZone.hasSelection;
			}
		}

		#endregion
	}
}
