using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryInfo : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject inventory;
    
    #endregion

    #region Properties
    
    public static int ammoCount { get; set; }

    public static bool hasKnife { get; set; }

    #endregion

    #region LifeCycle

    private void Update()
	{
		Counting();
	}

	#endregion

	#region  Private Methods
	private void Counting()
	{
		XRSocketInteractor[] socketZones = inventory.GetComponentsInChildren<XRSocketInteractor>();

		ammoCount = 0;

		foreach (var socketZone in socketZones)
		{
			if (socketZone.name.Contains("Ammo") && socketZone.hasSelection)
			{
				ammoCount++;
			}
			
			hasKnife = socketZone.name.Contains("Knife") && socketZone.hasSelection;
		}
	}

	#endregion
}
