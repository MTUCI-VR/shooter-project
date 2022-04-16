using UnityEngine;

namespace ShooterProject.Scripts.Items
{
	public class Item : MonoBehaviour
	{
		#region Properties

		public int initialLayerValue { get; private set; }

		#endregion

		#region Life Cycle

		private void Awake()
		{
			initialLayerValue = gameObject.layer;
		}

		#endregion
	}
}
