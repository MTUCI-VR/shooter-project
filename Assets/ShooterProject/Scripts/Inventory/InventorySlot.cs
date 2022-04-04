using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
	public abstract class InventorySlot : MonoBehaviour
	{
		#region Fiedls

		private XRSocketInteractor _socketInterector;

		#endregion

		#region  LifeCycle

		private void Awake()
		{
			_socketInterector = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			AddListeners();
		}
		private void OnDisable()
		{
			RemoveListeners();
		}

		#endregion

		#region Private Methods
		private void AddListeners()
		{
			_socketInterector.selectEntered.AddListener(OnPutInInventory);
			_socketInterector.selectExited.AddListener(OnTakenFromInventory);
		}
		private void RemoveListeners()
		{
			_socketInterector.selectEntered.RemoveListener(OnPutInInventory);
			_socketInterector.selectExited.RemoveListener(OnTakenFromInventory);
		}

		#endregion

		#region Protected Abstract Methods

		protected abstract void OnPutInInventory(SelectEnterEventArgs enterEventArgs);

		protected abstract void OnTakenFromInventory(SelectExitEventArgs exitEventArgs);

		#endregion
	}
}
