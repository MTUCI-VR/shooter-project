using System;

namespace ShooterProject.Scripts.Inventory
{
	public static class InventoryInfo
	{
		#region Fields

		private static int _pistolAmmoCount;
		private static int _rifleAmmoCount;

		#endregion

		#region Events

		public static event Action OnPistolAmmoCountChanged;
		public static event Action OnRifleAmmoCountChanged;

		#endregion

		#region Properties

		public static int PistolAmmoCount
		{
			get
			{
				return _pistolAmmoCount;
			}
			set
			{
				_pistolAmmoCount = value;
				OnPistolAmmoCountChanged?.Invoke();
			}
		}
		public static int RifleAmmoCount
		{
			get
			{
				return _rifleAmmoCount;
			}
			set
			{
				_rifleAmmoCount = value;
				OnRifleAmmoCountChanged?.Invoke();
			}
		}

		#endregion
	}
}
