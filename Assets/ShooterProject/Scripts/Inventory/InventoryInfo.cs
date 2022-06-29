using System;

namespace ShooterProject.Scripts.Inventory
{
	public static class InventoryInfo
	{
		#region Fields

		private static int _ammoCount;

		private static bool _hasKnife;

		#endregion

		#region Events

		public static event Action OnAmmoCountChanged;

		public static event Action OnHasKnifeChanged;

		#endregion

		#region Properties

		public static int AmmoCount
		{
			get
			{
				return _ammoCount;
			}
			set
			{
				_ammoCount = value;
				OnAmmoCountChanged?.Invoke();
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
