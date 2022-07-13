using System;

namespace ShooterProject.Scripts.Inventory
{
	public static class InventoryInfo
	{
		#region Fields

		private static int _ammoCount;

		#endregion

		#region Events

		public static event Action OnAmmoCountChanged;

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

		#endregion
	}
}
