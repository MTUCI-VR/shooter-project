using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class KnifeInventorySlot : InventorySlot
    {
        #region Protetected Override Methods

        protected override void OnPutInInventory(SelectEnterEventArgs enterEventArgs)
        {
            InventoryInfo.HasKnife = true;
        }
        protected override void OnTakenFromInventory(SelectExitEventArgs exitEventArgs)
        {
            InventoryInfo.HasKnife = false;    
        }

        #endregion
    }
}
