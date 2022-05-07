using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Weapons.Reloading
{
	[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
	public class AmmoMagazine : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private int _ammoSize;

		[SerializeField]
		private int _ammoCount;

		#endregion

		#region Properties

		public int AmmoCount
		{
			get
			{
				return _ammoCount;
			}
			private set
			{
				_ammoCount = value;
				OnAmmoCountChanged?.Invoke();
			}
		}

		public bool HasAmmo => _ammoCount > 0;

		#endregion

		#region Events

		public event Action OnAmmoCountChanged;

		#endregion

		#region Public methods

		/// <summary> 
		/// Уменьшает кол-во патронов в текущем магазине.
		/// </summary>
		public void DecreaseAmmoCount()
		{
			if (AmmoCount > 0)
				AmmoCount--;
		}

		#endregion
	}
}
