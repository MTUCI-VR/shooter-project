using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class AmmoMagazineInventorySlot : InventorySlot
    {
        #region Protetected Override Methods

        protected override void OnPutInInventory(SelectEnterEventArgs enterEventArgs)
        {
            InventoryInfo.AmmoMagazineCount++;
        }
        protected override void OnTakenFromInventory(SelectExitEventArgs exitEventArgs)
        {
            InventoryInfo.AmmoMagazineCount--;    
        }

        #endregion
    }
}
