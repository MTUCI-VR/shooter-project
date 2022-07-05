using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Weapons.Reloading;

namespace ShooterProject.Scripts.Inventory
{
	public class AmmoMagazineInventorySlot : InventorySlot
	{
		#region Protetected Override Methods

		protected override void OnPutInInventory(SelectEnterEventArgs enterEventArgs)
		{
			InventoryInfo.AmmoCount += enterEventArgs.interactableObject.transform.GetComponent<AmmoMagazine>().AmmoCount;
		}
		protected override void OnTakenFromInventory(SelectExitEventArgs exitEventArgs)
		{
			InventoryInfo.AmmoCount -= exitEventArgs.interactableObject.transform.GetComponent<AmmoMagazine>().AmmoCount;
		}

		#endregion
	}
}
