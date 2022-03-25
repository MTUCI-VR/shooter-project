using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Items.Weapons
{
	[RequireComponent(typeof(Rigidbody),typeof(XRGrabInteractable))]
	public class AmmoMagazine : MonoBehaviour
	{
		#region PrivateFields

		[SerializeField]
		private int _ammoSize;

		[SerializeField]
		private int _ammoCount;

		[SerializeField]
		private int _secondsToDestroyAfterUse;

		private Rigidbody _rigidbody;
		private XRGrabInteractable _xrGrabInteractable;
		#endregion

		#region Properties
		public bool HasAmmo => _ammoCount > 0;
		public Rigidbody Rigidbody => _rigidbody;
		public XRGrabInteractable XRGrabInteractable => _xrGrabInteractable;
		#endregion

		#region LifeCycle Methods
		private void Start()
		{
			_xrGrabInteractable = GetComponent<XRGrabInteractable>();
			_rigidbody = GetComponent<Rigidbody>();
		}
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

		/// <summary> 
		/// Возвращает кол-во патронов в текущем магазине.
		/// </summary>
		public int GetAmmoCount()
		{
			return _ammoCount;
		}

		#endregion

	}
}
