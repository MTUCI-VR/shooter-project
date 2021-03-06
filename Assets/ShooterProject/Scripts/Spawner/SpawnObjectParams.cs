using UnityEngine;

namespace ShooterProject.Scripts.Spawner
{
	public class SpawnObjectParams : MonoBehaviour
	{
		[Header("Spawn Object Params")]

		#region Fields

		[SerializeField]
		private int spawnWeight;

		[SerializeField]
		public int maxCount;

		#endregion

		#region Properties

		public int SpawnWeight => spawnWeight;

		#endregion
	}
}
