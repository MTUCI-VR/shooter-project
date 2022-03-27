using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{	
	public class InventoryInfo : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private GameObject inventory;
		
		#endregion

		#region Properties
		
		public static int ammoMagazineCount { get; private set; }

		public static bool hasKnife { get; private set; }

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
			XRSocketInteractor[] snapZones = inventory.GetComponentsInChildren<XRSocketInteractor>();

			ammoMagazineCount = 0;

			foreach (var snapZone in snapZones)
			{
				if (snapZone.tag == "AmmoMagazineSnapZone" && snapZone.hasSelection)
					ammoMagazineCount++;
				
				hasKnife = snapZone.tag == "KnifeSnapZone" && snapZone.hasSelection;
			}
		}

		#endregion
	}
}