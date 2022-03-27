using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.Inventory
{
	public class GetInventoryInfoForCanvas : MonoBehaviour
	{
		#region Fields
		private Text canvasText;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			canvasText = GetComponent<Text>();
		}
		private void Update()
		{
			canvasText.text = $"Магазинов: {InventoryInfo.ammoMagazineCount}\nНож: {InventoryInfo.hasKnife}";
		}

		#endregion
	}
}
