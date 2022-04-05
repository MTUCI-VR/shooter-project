using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShooterProject.Scripts.Inventory
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class InventoryInfoForCanvas : MonoBehaviour
	{
		#region Fields

		private TextMeshProUGUI _canvasText;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_canvasText = GetComponent<TextMeshProUGUI>();
		}

		private void OnEnable()
		{
			InventoryInfo.OnAmmoMagazineCountChanged += Print;
			InventoryInfo.OnHasKnifeChanged += Print;
		}

		private void OnDisable()
		{
			InventoryInfo.OnAmmoMagazineCountChanged -= Print;
			InventoryInfo.OnHasKnifeChanged -= Print;
		}

		#endregion

		#region Private Methods
		private void Print()
		{
			_canvasText.text = $"Магазинов: {InventoryInfo.AmmoMagazineCount}\nНож: {InventoryInfo.HasKnife}";
		}

		#endregion
	}
}
