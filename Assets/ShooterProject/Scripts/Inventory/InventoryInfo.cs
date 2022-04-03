using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryInfo : MonoBehaviour
	{
		#region  Constant Fields

		private const int AMMO_MAGAZINE_INTERACTION_LAYER = 8;
		private const int KNIFE_INTERACTION_LAYER = 16;

		#endregion

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
		private void OnEnable()
		{
			foreach (var snapZone in _snapZones)
			{
				snapZone.selectEntered.AddListener(Counting);
				snapZone.selectExited.AddListener(Counting);
			}
		}
		private void OnDisable()
		{
			foreach (var snapZone in _snapZones)
			{
				snapZone.selectEntered.RemoveAllListeners();
				snapZone.selectExited.RemoveAllListeners();
			}
		}

		#endregion

		#region  Private Methods
		private void Counting(SelectEnterEventArgs arg0)
		{
			if (arg0.interactableObject.interactionLayers.value == AMMO_MAGAZINE_INTERACTION_LAYER)
				AmmoMagazineCount++;
			else
				HasKnife = arg0.interactableObject.interactionLayers.value == KNIFE_INTERACTION_LAYER;
		}
		private void Counting(SelectExitEventArgs arg0)
		{
			if (arg0.interactableObject.interactionLayers.value == AMMO_MAGAZINE_INTERACTION_LAYER)
				AmmoMagazineCount--;
			else
				HasKnife = !(arg0.interactableObject.interactionLayers.value == KNIFE_INTERACTION_LAYER);
		}

		#endregion
	}
}
