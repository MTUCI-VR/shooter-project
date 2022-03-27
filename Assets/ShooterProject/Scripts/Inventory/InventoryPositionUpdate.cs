using UnityEngine;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryPositionUpdate : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private Transform cameraPosition;

		[SerializeField]
		private Vector3 positionOffset;

		#endregion

		#region LifeCycle

		private void FixedUpdate()
		{
			transform.position = cameraPosition.position + positionOffset;
			transform.eulerAngles = new Vector3(0, cameraPosition.eulerAngles.y, 0);
		}

		#endregion
	}
}

