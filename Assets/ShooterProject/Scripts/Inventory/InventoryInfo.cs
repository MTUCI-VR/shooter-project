using System;
using UnityEngine;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryInfo : MonoBehaviour
	{
		#region Fields

		private static int _ammoMagazineCount;

		private static bool _hasKnife;

		#endregion

		#region Events
		
		public static event Action OnAmmoMagazineCountChanged;

		public static event Action OnHasKnifeChanged;

		#endregion

		#region Properties

		public static int AmmoMagazineCount
		{
			get
			{
				return _ammoMagazineCount;
			}
			set
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
			set
			{
				_hasKnife = value;
				OnHasKnifeChanged?.Invoke();
			}
		}

		#endregion		
	}
}
