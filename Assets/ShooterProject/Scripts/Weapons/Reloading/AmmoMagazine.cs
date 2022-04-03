using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Weapons.Reloading
{
	[RequireComponent(typeof(Rigidbody),typeof(XRGrabInteractable))]
	public class AmmoMagazine : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private int _ammoSize;

		[SerializeField]
		private int _ammoCount;

		#endregion

		#region Properties

		public int AmmoCount => _ammoCount;

		#endregion

		#region Properties

		public bool HasAmmo => _ammoCount > 0;

		#endregion

		#region Public methods

		/// <summary> 
		/// Уменьшает кол-во патронов в текущем магазине.
		/// </summary>
		public void DecreaseAmmoCount()
		{
			if(_ammoCount > 0)
			_ammoCount--;
		}

		#endregion

	}
}
